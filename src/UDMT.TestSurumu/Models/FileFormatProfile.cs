using System.Collections.Generic;

namespace UDMT.Models;

public sealed record FormatStep(string Title, string Description, string Status);

public sealed record FileFormatProfile(
    string Id,
    string Name,
    string ShortName,
    string Extensions,
    string Description,
    string KnownGames,
    string DetectionMethod,
    IReadOnlyList<FormatStep> Steps);

public static class FileFormatProfiles
{
    public static IReadOnlyList<FileFormatProfile> All { get; } = new[]
    {
        new FileFormatProfile(
            "wem", "Wwise Encoded Media", "WEM", ".wem",
            "Wwise ses verisini taşıyan temel medya biçimi. Codec, Wwise sürümü, örnekleme hızı, kanal yapısı ve CUE bilgileri korunarak ele alınmalıdır.",
            "Outlast, Outlast: Whistleblower, DMC: Devil May Cry, F1 25 ve diğer Wwise kullanan oyunlar",
            "RIFF/RIFX-WAVE yapısı, fmt/data bölümleri ve Wwise'a özgü alanlar birlikte kontrol edilir.",
            new[]
            {
                new FormatStep("Tanı", "Dosyanın RIFF/RIFX yapısını, codec türünü, Wwise sürümünü, örnekleme hızını ve kanal sayısını belirle.", "Tanım hazır"),
                new FormatStep("Aç ve incele", "Ses verisini çöz, süreyi ve varsa CUE/adtl/labl etiketlerini göster.", "Çekirdek bağlanacak"),
                new FormatStep("Değiştir", "Yeni kaydı özgün dosyanın teknik özelliklerine dönüştür ve zamanlama verilerini koru.", "Çekirdek bağlanacak"),
                new FormatStep("Geri yaz", "Yeni WEM'i üret, yapısal doğrulamadan geçir ve tekrar açarak sonucu kontrol et.", "Doğrulama eklenecek")
            }),

        new FileFormatProfile(
            "bnk", "Wwise SoundBank", "BNK", ".bnk",
            "Wwise ses bankası biçimi. UDMT'deki mevcut BNK çalışması öncelikle Outlast ve Outlast: Whistleblower dosyalarına göre geliştirilecektir.",
            "Outlast ve Outlast: Whistleblower",
            "BKHD, DIDX, DATA ve HIRC bölüm kimlikleri; bölüm boyutları ve WEM indeksleri doğrulanır.",
            new[]
            {
                new FormatStep("Tanı", "BNK başlığını, Wwise sürümünü ve mevcut bölümleri belirle.", "Tanım hazır"),
                new FormatStep("Aç", "DIDX ve DATA tablolarından gömülü WEM kayıtlarını çıkar; HIRC nesnelerini ayrı göster.", "Outlast odaklı çekirdek bağlanacak"),
                new FormatStep("Değiştir", "Seçilen WEM'i aynı kimlik ve uyumlu teknik özelliklerle değiştir.", "Çekirdek bağlanacak"),
                new FormatStep("Geri paketle", "Ofsetleri, uzunlukları ve bölüm boyutlarını yeniden hesapla; BNK'yi tekrar açarak doğrula.", "Yazma doğrulaması eklenecek")
            }),

        new FileFormatProfile(
            "dmc-apk", "DMC APK Arşivi", "APK", ".apk",
            "DMC: Devil May Cry tarafından kullanılan özel arşiv biçimi. Android APK biçimiyle ilişkili değildir.",
            "DMC: Devil May Cry",
            "DMC'ye özgü başlık, dosya indeksi, giriş ofsetleri ve uzunlukları kontrol edilir.",
            new[]
            {
                new FormatStep("Tanı", "Arşiv başlığını ve DMC APK varyantını doğrula.", "Araştırma tanımı hazırlanıyor"),
                new FormatStep("Aç", "Dosya indeksini oku ve arşiv girdilerini güvenli biçimde listele.", "Mevcut çekirdek bağlanacak"),
                new FormatStep("İçeriği değiştir", "Yeni ses girdisini doğru kayıtla eşleştir; boyut ve hizalama sınırlarını kontrol et.", "Çekirdek bağlanacak"),
                new FormatStep("Geri paketle", "İndeksi ve ofsetleri yeniden oluştur; çıkan APK'yi yeniden açarak bütünlüğünü doğrula.", "Test örnekleri gerekli")
            }),

        new FileFormatProfile(
            "nefs", "EGO NeFS Arşivi", "NeFS", ".nfs",
            "Codemasters/EGO oyunlarında kullanılan arşiv biçimi. Sürüm ve sıkıştırma yöntemi oyunlara göre değişebilir.",
            "F1 25 ve uyumlu Codemasters/EGO oyunları",
            "NeFS imzası, sürüm, bölüm tabloları, dosya indeksi ve sıkıştırma bilgileri birlikte doğrulanır.",
            new[]
            {
                new FormatStep("Tanı", "NeFS sürümünü, başlık yapısını ve kullanılan sıkıştırma yöntemlerini belirle.", "NefsLib üzerinden bağlanacak"),
                new FormatStep("Aç", "Dosya ağacını, sıkıştırılmış ve gerçek boyutları listele; seçilen girdiyi dışarı çıkar.", "Çekirdek bağlanacak"),
                new FormatStep("Değiştir", "Kaynak dosyayı hedef girdiye eşleştir ve gerekiyorsa WEM özelliklerini dönüştür.", "Güvenli eşleme eklenecek"),
                new FormatStep("Geri paketle", "Arşivi yeni dosyaya yaz, yedek oluştur ve yeniden açma doğrulaması yap.", "Veri güvenliği zorunlu")
            }),

        new FileFormatProfile(
            "pck", "Wwise Package", "PCK", ".pck",
            "Wwise paket biçimi. Birden fazla ses bankası ve medya girdisini tek paket içinde taşıyabilir.",
            "Wwise paketleri kullanan oyunlar",
            "Paket başlığı, dil/banka tabloları, veri blokları ve giriş ofsetleri incelenir.",
            new[]
            {
                new FormatStep("Tanı", "PCK sürümünü ve giriş tablolarını belirle.", "Şema araştırması"),
                new FormatStep("Aç", "Bankaları ve medya girdilerini salt okunur biçimde listele.", "Planlandı"),
                new FormatStep("Değiştir", "Seçilen girdi için boyut, hizalama ve kimlik doğrulaması uygula.", "Planlandı"),
                new FormatStep("Geri paketle", "Tabloları ve ofsetleri yeniden üret; paket bütünlüğünü doğrula.", "Planlandı")
            }),

        new FileFormatProfile(
            "aesp", "Audio Event System Package", "AESP", ".aesp",
            "Ses olayları ve ilişkili verileri taşıyan paket biçimi. Destek kapsamı örnek dosyalarla doğrulanacaktır.",
            "AESP kullanan oyunlar",
            "Başlık, bölüm tablosu, olay verisi ve gömülü medya yapıları incelenir.",
            new[]
            {
                new FormatStep("Tanı", "AESP varyantını ve bölüm yapısını belirle.", "Şema araştırması"),
                new FormatStep("Aç", "Olayları ve medya bağlantılarını salt okunur biçimde göster.", "Planlandı"),
                new FormatStep("Değiştir", "Değiştirilecek nesnenin bağımlılıklarını ve kimliklerini koru.", "Planlandı"),
                new FormatStep("Geri paketle", "Bölüm boyutlarını ve bağlantıları yeniden yaz; tekrar açarak doğrula.", "Planlandı")
            })
    };
}
