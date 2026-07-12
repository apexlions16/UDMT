using System.Linq;
using System.Windows;
using System.Windows.Input;
using UDMT.Models;

namespace UDMT;

public partial class GameSelectionWindow : Window
{
    private readonly string? _seciliOyunKimligi;

    public GameSelectionWindow(string? seciliOyunKimligi = null)
    {
        _seciliOyunKimligi = seciliOyunKimligi;
        InitializeComponent();
        DataContext = GameProfiles.All;
        Loaded += Pencere_Yuklendi;
    }

    public GameProfile? SelectedProfile { get; private set; }

    private void Pencere_Yuklendi(object sender, RoutedEventArgs e)
    {
        OyunListesi.SelectedItem = GameProfiles.All.FirstOrDefault(x => x.Id == _seciliOyunKimligi)
                                   ?? GameProfiles.All.FirstOrDefault();
        OyunListesi.Focus();
    }

    private void Devam_Click(object sender, RoutedEventArgs e) => SecimiTamamla();

    private void OyunListesi_CiftTik(object sender, MouseButtonEventArgs e) => SecimiTamamla();

    private void SecimiTamamla()
    {
        if (OyunListesi.SelectedItem is not GameProfile profile)
        {
            MessageBox.Show("Devam etmek için bir oyun seçin.", "UDMT — Oyun seçimi",
                MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        SelectedProfile = profile;
        DialogResult = true;
    }

    private void Cikis_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }
}
