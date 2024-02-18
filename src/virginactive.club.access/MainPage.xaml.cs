namespace virginactive.club.access;

using System.Diagnostics;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Microsoft.Extensions.DependencyInjection;
using virginactive.club.access.services;
using ZXing.Net.Maui;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void CheckInClicked(object sender, EventArgs e)
    {
        // Start scanning


        barcodeReaderView.IsVisible = true;
        // barcodeReaderView.IsScanning = true;
    }

    private void CheckOutClicked(object sender, EventArgs e)
    {
        // Start scanning
        barcodeReaderView.IsVisible = true;
        // barcodeReaderView.IsScanning = true;
    }

    private async void BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        // Stop scanning
        // barcodeReaderView.IsScanning = false;
        barcodeReaderView.IsVisible = false;

        // Accessing the first detected barcode value
        var barcodeValue = e.Results.FirstOrDefault()?.Value;
        if (!string.IsNullOrEmpty(barcodeValue))
        {
            Debug.WriteLine($"Scanned QR Code: {barcodeValue}");

            await ShowTemporaryPopup("Scan Result", $"Welcome: {barcodeValue}", 5000);
            // TODO: Call your service to log access with the barcode value
            // Determine if it's a check-in or check-out based on your app logic
        }
        else
        {
            Debug.WriteLine("No QR Code detected or QR Code is empty.");
        }
    }

    private async Task ShowTemporaryPopup(string title, string message, int millisecondsDelay)
    {
        // Show the alert
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        string text = $"Welcome: {message}";
        ToastDuration duration = ToastDuration.Long;
        double fontSize = 30;

        var toast = Toast.Make(text, duration, fontSize);

        await toast.Show(cancellationTokenSource.Token);

        Debug.WriteLine($"showing toast");
    }
}
