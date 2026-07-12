using System;
using System.IO;
using System.Windows;
using UDMT.Models;
using UDMT.Services;

namespace UDMT;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        ThemeManager.KayitliTemayiUygula();

        DispatcherUnhandledException += (_, args) =>
        {
            HataKaydet(args.Exception);
            MessageBox.Show(
                "Beklenmeyen bir hata oluştu. Ayrıntılar udmt_error.log dosyasına yazıldı.\n\n" + args.Exception.Message,
                "UDMT — Hata",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            args.Handled = true;
        };

        var secim = new FormatSelectionWindow();
        if (secim.ShowDialog() != true || secim.SelectedFormat is null)
        {
            Shutdown();
            return;
        }

        AnaPencereyiAc(secim.SelectedFormat);
    }

    public static void AnaPencereyiAc(FileFormatProfile format, Window? eskiPencere = null)
    {
        var pencere = new MainWindow(format);
        Current.MainWindow = pencere;
        Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
        pencere.Show();
        eskiPencere?.Close();
    }

    private static void HataKaydet(Exception hata)
    {
        try
        {
            File.WriteAllText(Path.Combine(AppContext.BaseDirectory, "udmt_error.log"), hata.ToString());
        }
        catch
        {
            // Günlük yazılamasa bile uygulamanın hata gösterimini engelleme.
        }
    }
}
