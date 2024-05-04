

using ScanShipper.Models;
using System.IO.Compression;
using System.Text.Json;

namespace ScanShipper.App;

public partial class SenderPage : ContentPage
{
	public SenderPage()
	{
		InitializeComponent();


		
    }

   



    private bool sending;
    

    private async void pickButton_Clicked(object sender, EventArgs e)
    {

        if (!sending)
        {
            sending = true;
            var options = PickOptions.Default;

            var _c = new Compressor();

            try
            {
                var result = await FilePicker.Default.PickAsync(options);
                if (result != null)
                {
                    var tmp = _c.CompressFile(result.FullPath, result.FileName);

                   
                    
                        await ShowQR(tmp);
                    
                }

                //return result;
            }
            catch (Exception ex)
            {
                // The user canceled or something went wrong
                sending = false;
            }

        }
    }


    public async Task ShowQR(List<ScanShipperPayload> tmp) {


        

      

        while (sending)
        {
            foreach (var item in tmp)
            {
                string jsonString = JsonSerializer.Serialize(item);
                barcodeGenerator.Value = jsonString;
                await Task.Delay(1000); // Wait for 1 second before processing the next item
            }
        }





    }

    private void stopButton_Clicked(object sender, EventArgs e)
    {
        sending=false;
    }
}