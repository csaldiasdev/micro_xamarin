namespace MicroAppPOC.Domain.Predictor
{
    public class Prediction
    {
        public string Service { get; set; }
        public string Plate { get; set; }
        public bool InTransit { get; set; }
        public bool Error { get; set; }
        public string Message { get; set; }
        public string ArrivalTimeMessage { get; set; }
        public int Distance { get; set; } 
    }
}