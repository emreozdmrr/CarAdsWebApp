
# <p align="center"> PROJE HAKKINDA </p>
<br>
<p>Merhaba, proje kullanıcıların araba ilanı yayınladığı bir Web uygulamasıdır.</p>
<p>Uygulamada kullanıcılar hesap oluşturup araba ilanı yayınlayabilir ve ilgilendikleri ilanlar için ilgili kullanıcılara uygulama üzerinden mesaj gönderebilirler.</p>
<p>Proje .Net 5 ile geliştirilmiştir.</p>

## <p align="center">KULLANILAN KÜTÜPHANELER VE TASARIM KALIPLARI</p>
<p>Projede katmanlı mimari tasarım deseni kullanılmıştır. Kullanılan katmanlar şunlardır:

- Business
- Common
- DataAccess
- Dtos
- Entities
- UI (ASP.NET Web App)
</p>

<p>
    Projenin arayüz tasarımları için <strong>Bootstrap'ten</strong> faydalanılmıştır.
</p>
<p>Sayfalama için <b>PagedList</b> kütüphanesinden yararlanılmıştır.</p>
<p>Oluşturduğumuz Dto'ların validasyon kontrolleri için <b>FluentValidation</b> kütüphanesi kullanılmıştır.</p>
<p>Entity-Dto dönüşümleri için <b>AutoMapper</b> kütüphanesinden faydalanılmıştır.</p>
<p>Projede veritabanı işlemlerinin yönetimi için <b>UnitOfWork</b> tasarım deseni kullanılmıştır.</p>
<p>Tarih ve Marka bazlı yeni ilan sayıları ile grafik oluşturmak için <b>Chart.js</b> kütüphanesi kullanılmıştır.</p>
<p>Projede veritabanı olarak <b>MsSQL</b> ve veritabanı işlemleri için <b>EntityFramework Core</b> kullanılmıştır.</p>
<p>Bazı arayüzlerde fiyat veri girişinin kullanıcı dostu olması için <b>AutoNumeric</b> kütüphanesinden faydalanılmıştır.</p>
<p>Projede birbiri ile ilişkili select list verilerinin   sayfayı yeniden yüklemeden güncellenmesi için <b>JQuery'den</b> yararlanılmıştır.</p>

<p>Uygulama içinden görseller ile uygulamayı detaylı olarak inceleyebiliriz.</p>

## <p align="center"> UYGULAMA DETAYLARI </p>


- <p>Kullanıcıların uygulamaya giriş yaptığı ve yeni hesap oluşturduğu arayüzler</p>


![image alt](readmefiles/readme-1.png)
![image alt](readmefiles/readme-3.png)

- Validasyon kontrolleri

<br>

![image alt](readmefiles/readme-2.png)
![image alt](readmefiles/readme-4.png)

- Member rolüne sahip kullanıcıların uygulamada İlanlarını, Mesajlarını ve Bilgilerini kontrol ettiği kullanıcı paneli
<br>

![image alt](readmefiles/readme-5.png)

- Kullanıcıların yeni ilan oluşturduğu arayüz
<br>

![image alt](readmefiles/readme-6.png)
![image alt](readmefiles/readme-7.png)

- Kullanıcıların diğer ilanlar için gönderdiği ve yayınladıkları ilanlar için aldıkları mesajları kontrol ettiği arayüzler
<br>

![image alt](readmefiles/readme-8.png)
![image alt](readmefiles/readme-9.png)

- İlan ve Kullanıcı bazlı mesaj detayı
<br>

![image alt](readmefiles/readme-10.png)

- Kullanıcı bilgilerinin güncellendiği arayüz
<br>

![image alt](readmefiles/readme-11.png)

- Tüm ilanların listelendiği Anasayfa arayüzü
<br>

![image alt](readmefiles/readme-16.png)

- Kullanıcılar üst menüde bulunan Grid ve Liste seçenekleri ile ilanların nasıl listeleneceğini seçebilmektedirler
<br>

![image alt](readmefiles/readme-17.png)

- Yine aynı menüde Fiyata göre ve Tarihe göre Artan ve Azalan olacak şekilde sıralama seçenekleri ile ilanları listeleyebilmektedirler
<br>

![image alt](readmefiles/readme-18.png)

- Marka ve Modellerin listelendiği kısımda Marka ve Model seçimi yaparak detaylı listeleme yapabilmektedirler
<br>

![image alt](readmefiles/readme-19.png)

- Fiyat Adres Yıl gibi bilgileri seçerek detaylı arama yapabilmektedirler
<br>

![image alt](readmefiles/readme-20.png)

- Listelenen bir ilanın detay arayüzü
<br>

![image alt](readmefiles/readme-21.png)

- Admin rolüne sahip bir kullanıcının Marka, Model, Kasa Tipi ve Vites Tipi gibi uygulamada kullanılan dinamik verileri güncelleyebildiği yönetici paneli
<br>

![image alt](readmefiles/readme-12.png)
![image alt](readmefiles/readme-13.png)

- Yönetici panelinden ulaşılan Tarihe ve Markaya göre yeni ilan sayıları ile oluşturulmuş grafikler
<br>

![image alt](readmefiles/readme-14.png)
![image alt](readmefiles/readme-15.png)

<br>

<p>Proje için eğitimlerinden faydalandığım Eğitmen: Yavuz Selim KAHRAMAN - Udemy</p>








