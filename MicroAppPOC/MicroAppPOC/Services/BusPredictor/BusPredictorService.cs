using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using MicroAppPOC.Domain.Predictor;

namespace MicroAppPOC.Services.BusPredictor
{
    public class BusPredictorService : IBusPredictorService
    {
        private const string SearchUrl = "http://web.smsbus.cl/web/buscarAction.do";
        
        public BusPredictorService() => ServicePointManager.Expect100Continue = false;

        public async Task<IEnumerable<Prediction>> GetStopPredictions(string StopId)
        {
            var bytesBodyContent = Encoding.UTF8.GetBytes($"d=busquedaParadero&ingresar_paradero={StopId}");
            var result = new List<Prediction>();
            
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(SearchUrl);
                
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = bytesBodyContent.Length;
                
                await using (var stream = request.GetRequestStream()) 
                    await stream.WriteAsync(bytesBodyContent, 0, bytesBodyContent.Length);

                using var response = (HttpWebResponse)request.GetResponse();

                var cookie = response.Headers.GetValues("Set-Cookie")?[0].Split(";")[0];

                var requestTwo = (HttpWebRequest)WebRequest.Create(SearchUrl);

                requestTwo.Method = "POST";
                requestTwo.ContentType = "application/x-www-form-urlencoded";
                requestTwo.ContentLength = bytesBodyContent.Length;
            
                requestTwo.Headers.Add("Cookie", cookie);
                
                await using (var stream = requestTwo.GetRequestStream()) 
                    await stream.WriteAsync(bytesBodyContent, 0, bytesBodyContent.Length);

                using var responseTwo = (HttpWebResponse)requestTwo.GetResponse();
                
                var resultText = await new StreamReader(responseTwo.GetResponseStream()).ReadToEndAsync();
                
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(resultText);

                var inTransitServices = htmlDoc
                    .DocumentNode
                    .Descendants("div")
                    .Where(x => x.Id.Equals("siguiente_respuesta") || x.Id.Equals("proximo_solo_paradero"))
                    .ToList();

                foreach (var service in inTransitServices)
                {
                    var serviceCode = service.Descendants("div")
                        .Where(x => x.Id.Equals("servicio_respuesta_solo_paradero"))
                        .Select(x => x.InnerHtml)
                        .FirstOrDefault();
                    
                    var plate = service.Descendants("div")
                        .Where(x => x.Id.Equals("bus_respuesta_solo_paradero"))
                        .Select(x => x.InnerHtml)
                        .FirstOrDefault();
                    
                    var arrivalMessage = service.Descendants("div")
                        .Where(x => x.Id.Equals("tiempo_respuesta_solo_paradero"))
                        .Select(x => x.InnerHtml)
                        .FirstOrDefault();
                    
                    var distance = service.Descendants("div")
                        .Where(x => x.Id.Equals("distancia_respuesta_solo_paradero"))
                        .Select(x => x.InnerHtml)
                        .FirstOrDefault();

                    int.TryParse(distance, out var numericDistance);
                    
                    result.Add(new Prediction
                    {
                        InTransit = true,
                        Error = false,
                        Service = serviceCode,
                        Plate = plate,
                        ArrivalTimeMessage = arrivalMessage.Trim(),
                        Distance = numericDistance
                    });
                }
                
                var notTransitServices = htmlDoc
                    .DocumentNode
                    .Descendants("div")
                    .Where(x => x.Id.Equals("servicio_error_solo_paradero") || x.Id.Equals("respuesta_error_solo_paradero"))
                    .Select(x => x.InnerHtml)
                    .ToList();

                for (var i = 0; i < notTransitServices.Count / 2; i += 2)
                {
                    result.Add(new Prediction
                    {
                        InTransit = false,
                        Error = true,
                        Service = notTransitServices[i],
                        Message = notTransitServices[i + 1],
                        Distance = 0
                    });
                }
            }
            catch (Exception e)
            {
                return result;
            }
            
            return result;
        }
    }
}