using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace UDMT;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        DispatcherUnhandledException += (_, args) =>
        {
            LogError(args.Exception);
            MessageBox.Show(args.Exception.ToString(), "UDMT - Hata");
            args.Handled = true;
        };
        AppDomain.CurrentDomain.UnhandledException += (_, args) =>
            LogError(args.ExceptionObject as Exception);

        // Ensure ToolVersion is set before any settings save (a module may save
        // before the Wwise editor — which originally set it — is ever opened).
        var ver = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "0.0";
        FusionTools.Utils.DataStorage.ToolVersion = ver.Length > 2 ? ver.Remove(ver.Length - 2) : ver;

        var shell = new UDMT.Shell.ShellWindow();
        MainWindow = shell;
        shell.Show();
    }

    public static System.Windows.Style? Res(string key) => Current.TryFindResource(key) as System.Windows.Style;

    private static void LogError(Exception? ex)
    {
        if (ex == null) return;
        try { File.WriteAllText(Path.Combine(AppContext.BaseDirectory, "udmt_error.log"), ex.ToString()); }
        catch { }
    }
}
