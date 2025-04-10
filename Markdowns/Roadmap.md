"Öncelik Kuyruğu / Acil Durum Simülasyonu" Projesi - Yol Haritası
 Genel Özellikler:

    Dil: Python

    Arayüz: CustomTkinter (modern, sade arayüz için)

    Veri yapıları: Heap, Linked List, HashMap, Tree

    Veri Saklama: Küçük bir local DB (JSON veya SQLite)

    Sunum: Demo + PDF rapor + README.md + opsiyonel PowerPoint

 3 Günlük Plan
 GÜN 1: Temel Yapının Kurulumu ve Veri Yapıları
 Hedef: Çalışan mantık, arayüz iskeleti ve veri modelleri

heapq ile min-heap tabanlı öncelik kuyruğu yapısını oluştur

Linked List ile hastaların geliş sırası tutulsun

HashMap (dict) ile hasta ID → detay bilgisi eşlemesi yapılsın

Tree ile acil durum kategorileri (kırmızı, sarı, yeşil) bir hiyerarşide gösterilsin

Küçük bir JSON dosyası ile veri kalıcılığı test et (SQLite'a karar verilebilir)

CustomTkinter ile pencere iskeleti: başlık, ekle/görüntüle/sıraya al butonları

    Örnek 2-3 hastayla sistemin işleyişi test edilir (manuel verilerle)

==========================================================================================
 GÜN 2: Arayüz + Etkileşim + Demo Fonksiyonları
 Hedef: Kullanılabilir simülasyon ve görsellik

Hasta ekleme formu (isim, yaş, durum, öncelik skoru)

Simülasyon başlat: hasta listesi sıraya girsin (heap işlesin)

İşlenmiş hastaları linked list + tree yapısında göster (GUI içinde liste/etiket)

JSON veya SQLite'a her adımda veri yazılsın

Hasta detaylarını hash ile getir, arayüzde göster

Görsel: Öncelik kuyruğu sırası → basit renkli kutularla tkinter’da çizim

    Demo butonu: otomatik senaryo simülasyonu başlat (her 3 sn'de 1 hasta işlenir)

 GÜN 3: Dokümantasyon + Teslim Dosyaları + Sunum
Hedef: Tüm çıktılar hazır, proje tamamlanmış

README.md → proje açıklaması, kurulum, veri yapıları açıklaması

report.pdf → tek sayfalık çıktı (amaç, kapsam, grup üyeleri)

.zip dosyası: grupno_projeadi.zip

Github’a her üyenin katkısı olan commit'ler (README, kod, açıklamalar)

Opsiyonel: PowerPoint (.pptx) sunumu (8-10 slayt)

    Proje amacı, veri yapıları, arayüz ekran görüntüleri, sonuçlar

Son test: Proje tek dosyada çalışıyor mu? Başka bilgisayarda test edilir.

    Gerekirse YouTube demo videosu

 Kullanılacak Yapılar Özet:
Veri Yapısı	Kullanım Amacı
Heap	Hastaları önceliklerine göre sıraya dizme
Linked List	Geliş sırasına göre hastaları gösterme
HashMap (dict)	Hasta ID ile detaylı bilgi eşlemesi
Tree	Durum öncelik kategorileri (kırmızı/sarı/yeşil gibi)
