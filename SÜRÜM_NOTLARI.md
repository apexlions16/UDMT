# UDMT v0.2.0-beta.1 — Oyun seçimi test sürümü

Bu sürüm, UDMT'nin oyun odaklı çalışma düzenini kullanıcıların deneyebilmesi için hazırlanmış ilk indirilebilir Windows test paketidir.

## Bu sürümde test edilebilenler

- Uygulama açılır açılmaz oyun seçim ekranının gösterilmesi.
- **Outlast** ve **Outlast: Whistleblower** profillerinin ayrı seçilebilmesi.
- Outlast profillerinde **BNK / WEM** çalışma alanının gösterilmesi.
- **DMC: Devil May Cry** profilinde **APK** çalışma alanının gösterilmesi.
- **F1 25** profilinde **NeFS** çalışma alanının gösterilmesi.
- Ana ekranın seçilen oyuna göre değişmesi.
- Uygulama kapatılmadan “Oyunu Değiştir” seçeneğiyle başka profile geçilebilmesi.
- Uygulama ve sürüm metinlerinin Türkçe olması.

## Bu sürümün sınırı

Bu paket bir **arayüz ve oyun yönlendirmesi beta sürümüdür**. Arşiv okuma, WEM dönüştürme, BNK/APK/NeFS dosyası değiştirme ve yeniden paketleme çekirdeği henüz bu indirilebilir pakete bağlanmamıştır. Ekranlarda bu durum açıkça gösterilir.

Eksik kaynak ve ikili bağımlılıklar güvenli biçimde depoya aktarıldıktan sonra mevcut arşiv düzenleme çekirdeği bu oyun profillerine bağlanacaktır.

## Kurulum

1. `UDMT-v0.2.0-beta.1-win-x64.zip` dosyasını indirin.
2. ZIP dosyasını boş bir klasöre tamamen çıkarın.
3. `UDMT.exe` dosyasını çalıştırın.
4. Açılış ekranından oyununuzu seçin.

Paket Windows x64 içindir ve **.NET 9 Desktop Runtime** gerektirir.

## Geri bildirim

Bir sorun bildirirken şunları yazın:

- seçtiğiniz oyun,
- beklediğiniz davranış,
- gerçekleşen davranış,
- varsa ekran görüntüsü,
- uygulama klasöründe oluştuysa `udmt_error.log` içeriği.

Issue, pull request ve inceleme açıklamalarının tamamı Türkçe hazırlanmalıdır.
