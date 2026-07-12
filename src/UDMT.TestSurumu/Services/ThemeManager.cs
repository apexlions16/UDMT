using System;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace UDMT.Services;

public static class ThemeManager
{
    private static readonly string AyarDizini = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "UDMT");

    private static readonly string TemaDosyasi = Path.Combine(AyarDizini, "tema.txt");

    public static bool IsDarkMode { get; private set; } = true;

    public static void KayitliTemayiUygula()
    {
        var koyuMu = true;

        try
        {
            if (File.Exists(TemaDosyasi))
                koyuMu = !string.Equals(File.ReadAllText(TemaDosyasi).Trim(), "acik", StringComparison.OrdinalIgnoreCase);
        }
        catch
        {
            koyuMu = true;
        }

        TemayiUygula(koyuMu, kaydet: false);
    }

    public static void TemayiDegistir() => TemayiUygula(!IsDarkMode, kaydet: true);

    public static string DugmeMetni => IsDarkMode ? "Açık Moda Geç" : "Koyu Moda Geç";

    private static void TemayiUygula(bool koyuMu, bool kaydet)
    {
        IsDarkMode = koyuMu;
        var kaynaklar = Application.Current.Resources;

        if (koyuMu)
        {
            FircayiAyarla(kaynaklar, "ArkaPlan", "#111318");
            FircayiAyarla(kaynaklar, "Panel", "#1A1D24");
            FircayiAyarla(kaynaklar, "PanelVurgu", "#242936");
            FircayiAyarla(kaynaklar, "Cizgi", "#343A48");
            FircayiAyarla(kaynaklar, "Metin", "#F4F6FA");
            FircayiAyarla(kaynaklar, "IkincilMetin", "#A7ADBA");
            FircayiAyarla(kaynaklar, "Vurgu", "#7C8CFF");
            FircayiAyarla(kaynaklar, "UyariArkaPlan", "#27231B");
            FircayiAyarla(kaynaklar, "UyariCizgi", "#6C592E");
            FircayiAyarla(kaynaklar, "UyariBaslik", "#FFD77A");
            FircayiAyarla(kaynaklar, "UyariMetin", "#E6D5AA");
        }
        else
        {
            FircayiAyarla(kaynaklar, "ArkaPlan", "#F4F6FA");
            FircayiAyarla(kaynaklar, "Panel", "#FFFFFF");
            FircayiAyarla(kaynaklar, "PanelVurgu", "#E9EDF5");
            FircayiAyarla(kaynaklar, "Cizgi", "#CDD3DE");
            FircayiAyarla(kaynaklar, "Metin", "#1C2430");
            FircayiAyarla(kaynaklar, "IkincilMetin", "#5D6675");
            FircayiAyarla(kaynaklar, "Vurgu", "#5267E8");
            FircayiAyarla(kaynaklar, "UyariArkaPlan", "#FFF7E2");
            FircayiAyarla(kaynaklar, "UyariCizgi", "#D7B95A");
            FircayiAyarla(kaynaklar, "UyariBaslik", "#785B00");
            FircayiAyarla(kaynaklar, "UyariMetin", "#5F4B12");
        }

        if (!kaydet)
            return;

        try
        {
            Directory.CreateDirectory(AyarDizini);
            File.WriteAllText(TemaDosyasi, koyuMu ? "koyu" : "acik");
        }
        catch
        {
            // Tema değişikliği uygulanır; ayar kaydedilemezse uygulama çalışmaya devam eder.
        }
    }

    private static void FircayiAyarla(ResourceDictionary kaynaklar, string anahtar, string renk)
    {
        kaynaklar[anahtar] = new SolidColorBrush((Color)ColorConverter.ConvertFromString(renk));
    }
}
