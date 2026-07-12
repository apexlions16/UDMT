using System.Collections.Generic;

namespace UDMT.Models;

public sealed record ToolInfo(string Title, string Description, string Status);

public sealed record GameProfile(
    string Id,
    string Name,
    string ShortName,
    string Description,
    string ArchiveType,
    IReadOnlyList<ToolInfo> Tools);

public static class GameProfiles
{
    public static IReadOnlyList<GameProfile> All { get; } = new[]
    {
        new GameProfile(
            "outlast",
            "Outlast",
            "OUTLAST",
            "Outlast ana oyununun BNK/WEM konuşma sesleri için çalışma alanı.",
            "BNK / WEM",
            new[]
            {
                new ToolInfo("BNK / WEM Editörü", "Outlast ses bankalarını açma, WEM seslerini bulma ve değiştirme akışı.", "Çekirdek entegrasyonu hazırlanıyor"),
                new ToolInfo("Ses → WEM", "Yeni dublaj kayıtlarını hedef Wwise biçimine dönüştürme akışı.", "Çekirdek entegrasyonu hazırlanıyor")
            }),

        new GameProfile(
            "outlast-whistleblower",
            "Outlast: Whistleblower",
            "WHISTLEBLOWER",
            "Whistleblower genişleme paketinin BNK/WEM konuşma sesleri için ayrı profil.",
            "BNK / WEM",
            new[]
            {
                new ToolInfo("BNK / WEM Editörü", "Whistleblower ses bankalarını ana oyundan ayrı yollar ve ayarlarla yönetme akışı.", "Çekirdek entegrasyonu hazırlanıyor"),
                new ToolInfo("Ses → WEM", "Yeni dublaj kayıtlarını Whistleblower dosyalarına uygun biçime dönüştürme akışı.", "Çekirdek entegrasyonu hazırlanıyor")
            }),

        new GameProfile(
            "dmc-devil-may-cry",
            "DMC: Devil May Cry",
            "DMC",
            "DMC: Devil May Cry tarafından kullanılan özel APK ses arşivleri için çalışma alanı.",
            "APK",
            new[]
            {
                new ToolInfo("DMC APK Arşiv Editörü", "DMC APK arşivlerini açma, içerik çıkarma, değiştirme ve yeniden paketleme akışı.", "Çekirdek entegrasyonu hazırlanıyor"),
                new ToolInfo("Ses → WEM", "Dublaj kayıtlarını DMC ses girdilerine uygun WEM biçimine dönüştürme akışı.", "Çekirdek entegrasyonu hazırlanıyor")
            }),

        new GameProfile(
            "f1-25",
            "F1 25",
            "F1 25",
            "Codemasters/EGO NeFS konuşma arşivleri için çalışma alanı.",
            "NeFS",
            new[]
            {
                new ToolInfo("F1 25 NeFS Editörü", "NeFS arşivlerini çıkarma, dosya değiştirme ve yeniden paketleme akışı.", "Çekirdek entegrasyonu hazırlanıyor"),
                new ToolInfo("Ses → WEM", "Yeni konuşma kayıtlarını arşivdeki özgün WEM özelliklerine eşleme akışı.", "Çekirdek entegrasyonu hazırlanıyor")
            })
    };
}
