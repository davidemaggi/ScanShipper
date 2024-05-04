namespace ScanShipper.Models
{
    public class ScanShipperPayload
    {
        public int sizeInBytes { get; set; }
        public int chunck{get; set;}
        public int totalChuncks { get; set; }

        public string fileName { get; set; }
        public string data { get; set; }


        public ScanShipperPayload() { 
        



        
        }


    }
}
