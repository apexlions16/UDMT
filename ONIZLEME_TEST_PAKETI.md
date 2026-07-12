# UDMT 0.1.1 — Oyun Seçimli Önizleme Test Paketi

Bu geçici test paketi, teslim edilen mevcut `0.1.0` Debug ikili dosyalarını Türkçe bir oyun seçici başlatıcıyla birlikte sunar.

## Desteklenen seçimler

| Oyun | Açılan çalışma alanı |
|---|---|
| Outlast | BNK / WEM editörü |
| Outlast: Whistleblower | BNK / WEM editörü |
| DMC: Devil May Cry | APK arşiv editörü |
| F1 25 | NeFS arşiv editörü |

## Kullanım

1. ZIP dosyasını tamamen bir klasöre çıkarın.
2. `UDMT-Baslat.cmd` dosyasını çalıştırın.
3. Türkçe seçim ekranından oyunu seçin.
4. Başlatıcı mevcut UDMT uygulamasını açar ve uygun çalışma alanını otomatik seçmeye çalışır.

## Gereksinim

Windows x64 üzerinde Microsoft .NET 9 Desktop Runtime gereklidir.

## Bilinen sınırlama

Bu paket kaynak koddan yeniden derlenmiş kalıcı sürüm değildir. Uygulamanın eski ikili dosyaları kullanıldığı için ana uygulamada bazı eski modül adları görülebilir. Kaynakta hazırlanan yerleşik oyun profili ve filtreleme mimarisi, tam kaynak aktarımı ve Windows Release derlemesi tamamlandığında doğrudan uygulamaya alınacaktır.

Oyun arşivlerinde işlem yapmadan önce mutlaka yedek alın ve ilk denemeleri kopya dosyalar üzerinde gerçekleştirin.
