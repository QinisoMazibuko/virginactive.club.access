using virginactive.club.access.services;

namespace virginactive.club.access;

public partial class App : Application
{
    private readonly IDataSyncService _dataSyncService;
    private Timer _syncTimer;

    public App(IDataSyncService dataSyncService)
    {
        InitializeComponent();
        _dataSyncService =
            dataSyncService ?? throw new ArgumentNullException(nameof(dataSyncService));

        MainPage = new AppShell();
        StartSyncService();
    }

    private void StartSyncService()
    {
        _syncTimer = new Timer(
            async _ => await _dataSyncService.SyncDataAsync(),
            null,
            TimeSpan.Zero,
            TimeSpan.FromMinutes(1)
        );
    }
}
