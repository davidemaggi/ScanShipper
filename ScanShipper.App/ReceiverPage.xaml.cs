using ScanShipper.Models;
using System.Text.Json;
using ZXing.Net.Maui;

namespace ScanShipper.App;

public partial class ReceiverPage : ContentPage
{
	public ReceiverPage()
    {
        InitializeComponent();

        barcodeReader.Options = new BarcodeReaderOptions()
        {
            Formats = BarcodeFormat.QrCode,
            AutoRotate = true,
            Multiple = true

        };

    }
    List<ScanShipperPayload> res = new List<ScanShipperPayload>();
    int expected = 0;

    private void barcodeReader_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
    {
        var first = e.Results?.FirstOrDefault();
        Console.WriteLine(first);

        if (first is null)
        {
            return;
        }


        try {

            var tmp = JsonSerializer.Deserialize<ScanShipperPayload>(first.Value);
            if (tmp !=null) {

            var exists = res.Any(x=>x.chunck==tmp.chunck);

            if (!exists) {

                res.Add(tmp);
            
            }
            }

            if (res.Count() == expected) {

                Dispatcher.DispatchAsync(async () => {
                    await DisplayAlert(@"File Received!", "bla bla", "OK");
                });


            }


        }
        catch(Exception ex) { }
        



        
    }

}