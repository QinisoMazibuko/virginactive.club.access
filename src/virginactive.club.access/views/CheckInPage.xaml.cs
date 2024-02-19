using System.Diagnostics;
using CommunityToolkit.Maui.Alerts;
using virginactive.club.access.services;
using ZXing.Net.Maui;

namespace virginactive.club.access;

public partial class CheckInPage : ContentPage
{
    public CheckInPage()
    {
        InitializeComponent();
    }

    private void CheckInClicked(object sender, EventArgs e)
    {
        // Show the check-in barcode reader and hide the check-out barcode reader

        checkinbarcodeReaderView.IsVisible = true;
    }

    private async void BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        // Determine the source CameraBarcodeReaderView
        var barcodeReaderView = sender as ZXing.Net.Maui.Controls.CameraBarcodeReaderView;
        if (barcodeReaderView == null)
        {
            Debug.WriteLine("Sender is not a CameraBarcodeReaderView");
            return;
        }

        Debug.WriteLine(barcodeReaderView.Id);
        Debug.WriteLine(checkinbarcodeReaderView.Id);
        // Use the Name property to distinguish between views
        string logtype = barcodeReaderView == checkinbarcodeReaderView ? "Check-In" : "Check-Out";

        // Adjust visibility or other properties based on the view
        barcodeReaderView.IsVisible = false; // Example of adjusting the triggering view directly

        // Continue with barcode processing...
        var barcodeValue = e.Results.FirstOrDefault()?.Value;
        if (!string.IsNullOrEmpty(barcodeValue))
        {
            var memberService = serviceExtensions.GetService<IMemberService>();
            var member = await memberService.GetMemberAsync(barcodeValue);

            Debug.WriteLine($"Scanned QR Code: {barcodeValue}");
            Debug.WriteLine($"Scanned QR Code for member : {member.Name} {member.Surname}");

            if (logtype == "Check-In")
            {
                await ShowTemporaryPopup($"Welcome {member.Name} {member.Surname}");
            }
            else
            {
                await ShowTemporaryPopup($"you Crushed it {member.Name} {member.Surname}");
            }
        }
        else
        {
            Debug.WriteLine("No QR Code detected or QR Code is empty.");
        }
    }

    private async Task ShowTemporaryPopup(string message)
    {
        try
        {
            await Dispatcher.DispatchAsync(async () =>
            {
                var toast = Toast.Make(message, CommunityToolkit.Maui.Core.ToastDuration.Long, 50);
                await toast.Show();
            });
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error showing toast: {ex}");
        }
    }
}
