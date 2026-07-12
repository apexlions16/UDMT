# UDMT Geliştirme Yol Haritası

## Yeni yön: önce dosya formatı, sonra oyun desteği

UDMT'nin geliştirme yönü değiştirilmiştir. Öncelik artık doğrudan “her oyun için çalışan evrensel bir modlama aracı” üretmek değildir. Önce desteklenecek dosya formatları tek tek tanımlanacak, güvenli biçimde açılacak, incelenecek, değiştirilecek ve yeniden paketlenecektir.

Temel ilke:

> Bir oyun profili eklenmeden önce o oyunun kullandığı her dosya formatı bağımsız olarak tanınmalı, okunmalı, yazılmalı ve yeniden açılarak doğrulanmalıdır.

Bu yaklaşım sayesinde aynı formatı kullanan yeni bir oyun eklenirken sıfırdan araç yazmak yerine doğrulanmış format çekirdeği yeniden kullanılabilecektir.

## Format yaşam döngüsü

Her format desteği aşağıdaki kapılardan geçecektir:

1. **Tanıma** — İmza, başlık, sürüm, bölüm tablosu ve varyant tespiti.
2. **Salt okunur açma** — Dosyayı değiştirmeden yapısını ve girdilerini listeleme.
3. **Dışarı çıkarma** — Seçili veya toplu içerik çıkarma ve çıktı özetlerini doğrulama.
4. **İçerik doğrulama** — Codec, kimlik, boyut, hizalama, sıkıştırma ve bağımlılık kontrolleri.
5. **Değiştirme** — Uyumlu yeni içeriği hedef girdiye güvenli biçimde yerleştirme.
6. **Geri paketleme** — İndeksleri, ofsetleri, bölüm boyutlarını ve sıkıştırmayı yeniden oluşturma.
7. **Yeniden açma testi** — Üretilen dosyayı aynı ayrıştırıcıyla tekrar açıp bütünlüğünü doğrulama.
8. **Oyun testi** — Önceki adımlar geçildikten sonra gerçek oyunda test etme.

Bir format, ilk yedi adım için otomatik testlere sahip olmadan “kararlı” kabul edilmeyecektir.

## Aşama 0 — Depo ve araştırma temeli

- [ ] Eksik kaynak ve ikili bağımlılıkları özgün yollarıyla depoya aktarmak.
- [ ] `bin`, `obj`, geçici dosya ve decompile araştırma çıktılarını üretim kodundan ayırmak.
- [ ] Her üçüncü taraf bileşenin kaynak kökenini, lisansını ve dağıtım iznini belgelemek.
- [ ] Format araştırması için paylaşımı izinli küçük test örnekleri hazırlamak.
- [ ] Test örneklerinin SHA-256 özetlerini kaydetmek.
- [ ] Temiz Windows ortamında tekrarlanabilir Release derlemesi sağlamak.

## Aşama 1 — Format kayıt sistemi

- [x] Format seçim ekranını oluşturmak.
- [x] WEM, BNK, DMC APK, NeFS, PCK ve AESP için ilk format tanımlarını eklemek.
- [x] Her format için tanıma, açma, değiştirme ve geri paketleme adımlarını uygulama içinde göstermek.
- [ ] `IFileFormatHandler` arabirimini oluşturmak.
- [ ] Format kimliği, uzantılar, imzalar, sürümler ve yetenekleri taşıyan merkezi kayıt sistemi eklemek.
- [ ] Bir dosyanın uzantısına güvenmeden içerikten format tespiti yapmak.
- [ ] Bilinmeyen veya desteklenmeyen varyantlarda güvenli biçimde salt okunur moda geçmek.

Önerilen temel arabirim:

```csharp
public interface IFileFormatHandler
{
    string Id { get; }
    FormatProbeResult Probe(Stream stream);
    FormatDocument OpenRead(Stream stream);
    ValidationResult Validate(FormatDocument document);
    void Save(FormatDocument document, Stream destination);
}
```

## Aşama 2 — WEM çekirdeği

- [ ] RIFF/RIFX ve Wwise alanlarını güvenilir biçimde tespit etmek.
- [ ] Codec, Wwise sürümü, örnekleme hızı, kanal sayısı ve süre bilgisini göstermek.
- [ ] CUE, `adtl` ve `labl` verilerini okuyup kayıpsız geri yazmak.
- [ ] FFmpeg ve vgmstream ile çözme/önizleme yolunu ayırmak.
- [ ] Desteklenen Wwise sürümleri için WEM kodlama profillerini tanımlamak.
- [ ] Yeni sesin özgün WEM özellikleriyle uyumunu zorunlu doğrulamak.
- [ ] Encode → yeniden aç → metadata karşılaştırması testi eklemek.

**Bitti ölçütü:** Aynı WEM dosyası değişiklik yapılmadan kaydedildiğinde yapı korunmalı; değiştirilmiş dosya yeniden açılmalı ve hedef teknik özellikleri doğrulamalıdır.

## Aşama 3 — Outlast / Whistleblower BNK çekirdeği

- [ ] BKHD, DIDX, DATA ve HIRC bölümlerini ayrı ayrıştırmak.
- [ ] Gömülü WEM kimliklerini, ofsetlerini ve uzunluklarını listelemek.
- [ ] Tekli ve toplu WEM dışa aktarma eklemek.
- [ ] Yeni WEM için kimlik, codec, sürüm ve boyut doğrulaması eklemek.
- [ ] DIDX/DATA ofsetlerini ve bölüm boyutlarını yeniden hesaplamak.
- [ ] HIRC nesnelerini salt okunur inceleme ekranına bağlamak.
- [ ] `.int` altyazı anahtarlarını ilgili olay/WEM kayıtlarıyla ilişkilendirmek.
- [ ] Outlast ve Whistleblower örnekleri için ayrı uyumluluk testleri yazmak.

**Bitti ölçütü:** BNK içindeki bir WEM değiştirildikten sonra banka tekrar açılmalı, tüm indeksler doğrulanmalı ve oyun kopyası üzerinde çalışmalıdır.

## Aşama 4 — DMC: Devil May Cry APK çekirdeği

- [ ] DMC APK başlık ve varyantlarını belgelemek.
- [ ] Android APK ile karışmayı önleyen içerik tabanlı tanıma eklemek.
- [ ] Dosya indeksi, ofset, uzunluk ve hizalama kurallarını çıkarmak.
- [ ] Salt okunur dosya ağacı ve dışa aktarma eklemek.
- [ ] Girdi değiştirme öncesinde boyut ve bağımlılık doğrulaması yapmak.
- [ ] İndeksi ve veri bloklarını deterministik biçimde yeniden yazmak.
- [ ] Çıkan arşivi yeniden açıp her girdinin özetini karşılaştırmak.
- [ ] DMC üzerinde gerçek oyun testi yapmak.

**Bitti ölçütü:** Değişiklik yapılmadan aç/kaydet işlemi yapısal olarak eşdeğer arşiv üretmeli; değiştirilen ses doğru girdide çalışmalıdır.

## Aşama 5 — EGO NeFS çekirdeği

- [ ] NeFS sürüm ve varyant tespiti eklemek.
- [ ] NefsLib kullanımını ince bir adaptör katmanına almak.
- [ ] Dosya ağacını ve sıkıştırma bilgilerini salt okunur göstermek.
- [ ] Tekli/toplu dışa aktarma ve özet doğrulaması eklemek.
- [ ] Kaynak-hedef eşleşmesini yalnızca dosya adına bırakmamak; tam yol ve kullanıcı onayı kullanmak.
- [ ] WEM dönüşümü başarısızsa işlemi durdurmak; “olduğu gibi ekle” davranışını kapatmak.
- [ ] Her kayıttan önce zaman damgalı yedek oluşturmak.
- [ ] Yazılan arşivi yeniden açarak indeks ve veri bütünlüğünü kontrol etmek.
- [ ] F1 25 ile ilk uyumluluk profilini doğrulamak.

**Bitti ölçütü:** Hatalı veya uyumsuz girdi arşive yazılmamalı; başarılı kayıt sonrasında arşiv tekrar açılmalı ve bütün girdiler okunabilmelidir.

## Aşama 6 — PCK ve AESP araştırması

- [ ] PCK başlık, dil tablosu, banka tablosu ve medya bloklarını belgelemek.
- [ ] PCK için salt okunur listeleme ve dışa aktarma prototipi hazırlamak.
- [ ] AESP varyantlarını ve bölüm yapılarını örnek dosyalarla karşılaştırmak.
- [ ] AESP olayları ile medya bağlantılarını salt okunur göstermek.
- [ ] Yazma desteğini ancak yeterli örnek ve test sağlandıktan sonra açmak.

## Aşama 7 — Ortak güvenlik ve test altyapısı

- [ ] Her format için `Probe`, `Open`, `Extract`, `Replace`, `Save`, `Reopen` testleri yazmak.
- [ ] Bozuk, kesilmiş, yanlış uzantılı ve aşırı büyük dosya testleri eklemek.
- [ ] Fuzz testleri ve sınır değer kontrolleri eklemek.
- [ ] Tüm yazma işlemlerini önce geçici dosyaya yapmak.
- [ ] Atomik değiştirme ve otomatik yedekleme sağlamak.
- [ ] Kaydetme öncesi değişiklik özeti ve kullanıcı onayı göstermek.
- [ ] Geri alma/geri yükleme mekanizması eklemek.
- [ ] GitHub Actions üzerinde Windows derleme ve test hattı kurmak.

## Aşama 8 — Arayüz ve tema sistemi

- [x] Koyu modu varsayılan tema yapmak.
- [x] Açık/koyu tema geçişi eklemek.
- [x] Tema seçimini kullanıcı profilinde saklamak.
- [ ] Tüm sabit renkleri tema kaynaklarına taşımak.
- [ ] Türkçe kullanıcı metinlerini merkezi kaynak dosyalarına taşımak.
- [ ] Format ayrıntı ekranına gerçek dosya açma ve hex/metadata görünümü eklemek.
- [ ] İşlem günlüğü, ilerleme göstergesi ve iptal desteği eklemek.
- [ ] Erişilebilirlik, klavye gezintisi ve yüksek kontrast kontrolleri yapmak.

## Aşama 9 — Oyun profilleri

Oyun profilleri ana mimarinin başlangıç noktası olmayacaktır. Format çekirdekleri kararlı olduktan sonra profiller yalnızca hazır çekirdekleri yapılandıracaktır.

- [ ] `IGameProfile` arabirimini eklemek.
- [ ] Outlast profilini BNK + WEM + `.int` yapılandırması olarak tanımlamak.
- [ ] Whistleblower profilini ayrı yollar ve altyazı kaynaklarıyla tanımlamak.
- [ ] DMC profilini APK + WEM yapılandırması olarak tanımlamak.
- [ ] F1 25 profilini NeFS + WEM yapılandırması olarak tanımlamak.
- [ ] Oyun klasörü tespiti, yedekleme konumu ve varsayılan ayarları profile taşımak.
- [ ] Profilin desteklediği format varyantlarını açıkça sınırlamak.

## Aşama 10 — Kullanılabilir sürüm

- [ ] Kurulum veya taşınabilir ZIP paketi hazırlamak.
- [ ] Lisans, NOTICE, katkı rehberi ve güvenlik politikası eklemek.
- [ ] Desteklenen format/sürüm/oyun uyumluluk matrisi yayımlamak.
- [ ] Örnek dosya paylaşmadan yeniden üretilebilir test talimatları yazmak.
- [ ] Hata raporuna format tespiti ve anonim teknik özet eklemek.
- [ ] İlk kararlı sürümü ancak veri kaybı testleri geçildikten sonra yayımlamak.

## Yakın dönem öncelik sırası

1. Depo ve lisans temizliği.
2. `IFileFormatHandler` ve içerik tabanlı format tespiti.
3. WEM salt okunur açma ve doğrulama.
4. Outlast/Whistleblower BNK açma ve yeniden paketleme.
5. DMC APK açma ve yeniden paketleme.
6. NeFS güvenli yazma akışı.
7. Ortak test, yedekleme ve yeniden açma doğrulaması.
8. Oyun profilleri ve oyun klasörü otomasyonu.

## Karar kayıtları

- Oyun seçimi ilk ekran olmaktan çıkarılmıştır; ilk ekran dosya formatı seçimidir.
- Format desteği uzantıya göre değil, içerik imzası ve yapı doğrulamasına göre belirlenecektir.
- Bir format için salt okunur destek tamamlanmadan yazma desteği açılmayacaktır.
- Geri paketlenen her dosya otomatik olarak yeniden açılmadan başarı mesajı gösterilmeyecektir.
- Oyun profilleri format kodunu kopyalamayacak; yalnızca doğrulanmış format işleyicilerini yapılandıracaktır.
