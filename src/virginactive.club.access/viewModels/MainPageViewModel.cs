using System.Diagnostics;
using System.Windows.Input;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Microsoft.Extensions.DependencyInjection;
using virginactive.club.access.services;
using ZXing.Net.Maui;

namespace virginactive.club.access;

public class MainPageViewModel
{
    private readonly IMemberService _memberService;

    public ICommand CheckInCommand { get; }
    public ICommand CheckOutCommand { get; }
    public ICommand BarcodesDetectedCommand { get; }

    public MainPageViewModel(IMemberService memberService)
    {
        // Fetch the IMemberService instance
        _memberService =
            memberService
            ?? throw new InvalidOperationException("IMemberService is not registered.");

        CheckInCommand = new Command(OnCheckIn);
        CheckOutCommand = new Command(OnCheckOut);
        BarcodesDetectedCommand = new Command<BarcodeDetectionEventArgs>(OnBarcodesDetected);
    }

    private void OnCheckIn() { }

    private void OnCheckOut() { }

    private async void OnBarcodesDetected(BarcodeDetectionEventArgs e)
    {
        var barcodeValue = e.Results.FirstOrDefault()?.Value;
        if (!string.IsNullOrEmpty(barcodeValue))
        {
            Debug.WriteLine($"Scanned QR Code: {barcodeValue}");

            var member = await _memberService.GetMemberAsync(barcodeValue);
            Debug.WriteLine($"Scanned QR Code for Member: {member}");

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            string text = $"Welcome: {member} ";
            ToastDuration duration = ToastDuration.Long;
            double fontSize = 30;

            var toast = Toast.Make(text, duration, fontSize);

            await toast.Show(cancellationTokenSource.Token);

            Debug.WriteLine($"showing toast");
        }
        else
        {
            Debug.WriteLine("No QR Code detected or QR Code is empty.");
        }
    }

    // Add ShowTemporaryPopup method here
}
