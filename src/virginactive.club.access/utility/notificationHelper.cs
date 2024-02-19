namespace virginactive.club.access;

using System.Diagnostics;
using CommunityToolkit.Maui.Alerts;

public static class notificationHelper
{
    public static async Task ShowTemporaryPopup(string message)
    {
        try
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                var toast = Toast.Make(message, CommunityToolkit.Maui.Core.ToastDuration.Long, 50);
                await toast.Show();
            });
        }
        catch (Exception ex)
        {
            // ToDO: replace with logging
            Debug.WriteLine($"Error showing toast: {ex}");
        }
    }
}
