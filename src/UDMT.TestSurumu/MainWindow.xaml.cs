using System.Windows;
using UDMT.Models;
using UDMT.Services;

namespace UDMT;

public partial class MainWindow : Window
{
    private readonly FileFormatProfile _format;

    public MainWindow(FileFormatProfile format)
    {
        _format = format;
        InitializeComponent();

        Title = $"UDMT — {format.Name}";
        SeciliFormatBaslik.Text = $"Seçili format: {format.Name}";
        FormatAdi.Text = format.Name;
        FormatAciklamasi.Text = format.Description;
        Uzantilar.Text = format.Extensions;
        BilinenOyunlar.Text = format.KnownGames;
        TespitYontemi.Text = format.DetectionMethod;
        AdimListesi.ItemsSource = format.Steps;
        TemaDugmesiniGuncelle();
    }

    private void FormatiDegistir_Click(object sender, RoutedEventArgs e)
    {
        var secim = new FormatSelectionWindow(_format.Id) { Owner = this };
        if (secim.ShowDialog() != true || secim.SelectedFormat is null || secim.SelectedFormat.Id == _format.Id)
            return;

        App.AnaPencereyiAc(secim.SelectedFormat, this);
    }

    private void TemaDugmesi_Click(object sender, RoutedEventArgs e)
    {
        ThemeManager.TemayiDegistir();
        TemaDugmesiniGuncelle();
    }

    private void TemaDugmesiniGuncelle() => TemaDugmesi.Content = ThemeManager.DugmeMetni;
}
