using System.Diagnostics;
using virginactive.club.access.core;
using virginactive.club.access.services;
using ZXing.Net.Maui;

namespace virginactive.club.access;

public partial class MainPage : ContentPage
{
    private readonly IMemberService _memberService;
    private readonly IAccessLogService _accessLogService;

    public MainPage()
    {
        InitializeComponent();

        _memberService = serviceExtensions.GetService<IMemberService>();

        _accessLogService = serviceExtensions.GetService<IAccessLogService>();
    }

    private async void CheckInClicked(object sender, EventArgs e)
    {
        try
        {
            await Shell.Current.GoToAsync("///MainPage");
        }
        catch (Exception ex)
        {
            // ToDO: replace with logging
            System.Diagnostics.Debug.WriteLine($"Navigation error: {ex.Message}");
        }
    }

    private async void CheckOutClicked(object sender, EventArgs e)
    {
        try
        {
            await Shell.Current.GoToAsync("///CheckOutPage");
        }
        catch (Exception ex)
        {
            // ToDO: replace with logging
            System.Diagnostics.Debug.WriteLine($"Navigation error: {ex.Message}");
        }
    }

    private void BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            var barcodeReaderView = sender as ZXing.Net.Maui.Controls.CameraBarcodeReaderView;
            if (barcodeReaderView == null)
            {
                Debug.WriteLine("Sender is not a CameraBarcodeReaderView");
                return;
            }

            barcodeReaderView.IsDetecting = false;

            var barcodeValue = e.Results.FirstOrDefault()?.Value;
            if (!string.IsNullOrEmpty(barcodeValue))
            {
                Debug.WriteLine("No QR Code detected or QR Code is empty."); // ToDO: replace with logging
            }

            try
            {
                var member = await _memberService.GetMemberAsync(barcodeValue);
                await _accessLogService.RecordAccessAsync(member.MemberId, accessType.CheckIn);

                await notificationHelper.ShowTemporaryPopup(
                    $"Welcome {member.Name} {member.Surname} :) "
                );
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"error while checking in member : {ex}.");
            }

            barcodeReaderView.IsDetecting = true;
        });
    }
}
