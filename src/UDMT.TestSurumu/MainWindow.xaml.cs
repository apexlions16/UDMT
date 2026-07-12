using System.Windows;
using UDMT.Models;

namespace UDMT;

public partial class MainWindow : Window
{
    private readonly GameProfile _profile;

    public MainWindow(GameProfile profile)
    {
        _profile = profile;
        InitializeComponent();

        Title = $"UDMT — {profile.Name}";
        SeciliOyunBaslik.Text = $"Seçili oyun: {profile.Name}";
        OyunAdi.Text = profile.Name;
        OyunAciklamasi.Text = profile.Description;
        ArsivTuru.Text = profile.ArchiveType;
        AracListesi.ItemsSource = profile.Tools;
    }

    private void OyunuDegistir_Click(object sender, RoutedEventArgs e)
    {
        var secim = new GameSelectionWindow(_profile.Id) { Owner = this };
        if (secim.ShowDialog() != true || secim.SelectedProfile is null || secim.SelectedProfile.Id == _profile.Id)
            return;

        App.AnaPencereyiAc(secim.SelectedProfile, this);
    }
}
