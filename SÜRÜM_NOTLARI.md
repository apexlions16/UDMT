# UDMT v0.3.0-beta.1 — Format Laboratuvarı ve Koyu Mod

Bu sürüm, projenin geliştirme yönünü oyun merkezli yaklaşımdan dosya formatı merkezli yaklaşıma geçirir.

## Yeni geliştirme yaklaşımı

- Açılışta oyun yerine dosya formatı seçilir.
- WEM, BNK, DMC APK, NeFS, PCK ve AESP için ayrı format kartları eklenmiştir.
- Her format için nasıl tanındığı, hangi oyunlarda görüldüğü, nasıl açılacağı, içeriğin nasıl değiştirileceği ve nasıl geri paketleneceği gösterilir.
- Oyun profilleri, format çekirdekleri kararlı olduktan sonra eklenecek yapılandırma katmanı olarak yeniden konumlandırılmıştır.
- Ayrıntılı geliştirme planı `YOL_HARITASI.md` dosyasına eklenmiştir.

## Tema sistemi

- Koyu mod varsayılan tema hâline getirilmiştir.
- Açık ve koyu mod arasında geçiş düğmesi eklenmiştir.
- Seçilen tema `%LocalAppData%\UDMT\tema.txt` dosyasında saklanır ve sonraki açılışta geri yüklenir.
- Pencere, panel, metin, çizgi, vurgu ve uyarı renkleri dinamik tema kaynaklarına taşınmıştır.

## Bu pakette test edilebilenler

- Format seçim ekranı.
- Formatlar arasında geçiş.
- Format tanıma ve açma/geri paketleme adımlarının görüntülenmesi.
- Açık/koyu tema geçişi.
- Tema seçiminin uygulama yeniden açıldığında korunması.

## Henüz bulunmayanlar

Bu beta sürümü gerçek dosya açma veya yeniden paketleme çekirdeğini içermemektedir. WEM, BNK, APK, NeFS, PCK ve AESP işleyicileri yol haritasındaki sırayla uygulamaya bağlanacaktır.

## Kurulum

1. `UDMT-v0.3.0-beta.1-win-x64.zip` dosyasını indirin.
2. ZIP dosyasını boş bir klasöre tamamen çıkarın.
3. `UDMT.exe` dosyasını çalıştırın.
4. Açılış ekranından incelemek istediğiniz dosya formatını seçin.

Paket Windows x64 içindir ve self-contained olarak yayımlanır.

## Bilinen sınırlar

- Ana kaynak arşivinin ve üçüncü taraf bağımlılıkların eksiksiz aktarımı tamamlanmamıştır.
- Format açıklamaları mevcut araştırma ve kod yapısına dayanır; gerçek örnek dosyalarla uyumluluk testleri gereklidir.
- PCK ve AESP desteği araştırma aşamasındadır.

## Geri bildirim

Bir sorun bildirirken seçilen formatı, beklenen davranışı, gerçekleşen davranışı, tema seçimini ve varsa `udmt_error.log` içeriğini ekleyin.
