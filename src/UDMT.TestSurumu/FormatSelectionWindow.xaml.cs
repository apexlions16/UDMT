using System.Linq;
using System.Windows;
using System.Windows.Input;
using UDMT.Models;
using UDMT.Services;

namespace UDMT;

public partial class FormatSelectionWindow : Window
{
    private readonly string? _seciliFormatKimligi;

    public FormatSelectionWindow(string? seciliFormatKimligi = null)
    {
        _seciliFormatKimligi = seciliFormatKimligi;
        InitializeComponent();
        DataContext = FileFormatProfiles.All;
        Loaded += Pencere_Yuklendi;
        TemaDugmesiniGuncelle();
    }

    public FileFormatProfile? SelectedFormat { get; private set; }

    private void Pencere_Yuklendi(object sender, RoutedEventArgs e)
    {
        FormatListesi.SelectedItem = FileFormatProfiles.All.FirstOrDefault(x => x.Id == _seciliFormatKimligi)
                                     ?? FileFormatProfiles.All.FirstOrDefault();
        FormatListesi.Focus();
    }

    private void Devam_Click(object sender, RoutedEventArgs e) => SecimiTamamla();

    private void FormatListesi_CiftTik(object sender, MouseButtonEventArgs e) => SecimiTamamla();

    private void SecimiTamamla()
    {
        if (FormatListesi.SelectedItem is not FileFormatProfile format)
        {
            MessageBox.Show("Devam etmek için bir dosya formatı seçin.", "UDMT — Format seçimi",
                MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        SelectedFormat = format;
        DialogResult = true;
    }

    private void TemaDugmesi_Click(object sender, RoutedEventArgs e)
    {
        ThemeManager.TemayiDegistir();
        TemaDugmesiniGuncelle();
    }

    private void TemaDugmesiniGuncelle() => TemaDugmesi.Content = ThemeManager.DugmeMetni;

    private void Cikis_Click(object sender, RoutedEventArgs e) => DialogResult = false;
}
