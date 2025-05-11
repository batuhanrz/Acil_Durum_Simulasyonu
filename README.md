# Acil Durum Yönetimi Simülasyonu

## **Proje Hakkında:**

Acil Durum Yönetimi Simülasyonu projesi, acil durum senaryolarında **veri yapıları kullanarak veri işleme, önceliklendirme ve analiz yapmayı** amaçlayan bir yazılımdır. Bu proje, acil durum durumlarını simüle ederek veri akışını, bellek tüketimini ve işlem sürelerini ölçer, **manuel veri girişine de olanak tanır.**

Proje, yüksek boyutlu veriler üzerinde veri yönetimi ve analizini daha etkili hale getirmek amacıyla esnek ve veri odaklı olarak tasarlanmıştır. Panellere bir hastanenin acil durumlarıyla alakalı bilgiler girilirse o yönde çalıştırılabilir, başka bir durum için o yönde çalıştırılabilir. Simulasyon aşamasındaki veri seti, hastane durum verilerine uygun olarak seçilmiştir.

---

##  **Mimarisi ve Yapısı:**

### **1. Veri Yapıları ve Kullanım Alanları:**

* **Heap (Öncelik Kuyruğu):** Acil durum verilerinin önceliklendirilmesinde kullanılır. Acil duruma göre en yüksek öncelikli veriler en üstte yer alır ve işlem sırası buna göre belirlenir.
* **LinkedList (Bağlı Liste):** Hasta verilerini sırayla saklamak ve gerektiğinde sıralı erişim sağlamak için kullanılır. Ekleme ve silme işlemleri **O(1)** zaman karmaşıklığına sahiptir.
* **Graph (Graf Yapısı):** Acil durum alanlarının ve bu alanlar arasındaki bağlantıların yönetiminde kullanılır. **Oda geçişleri ve yönlendirmeler** bu yapıyla modellenmiştir.
* **HashMap (Hash Tablosu):** Hastalar ve acil durumları arasında ilişki kurarak, hızlı erişim sağlar. HashMap ile **O(1)** karmaşıklığında veri araması yapılır.

---

### **2. Kullanıcı Arayüzü (UI) Bileşenleri:**

* **DataAssignmentPanel:** Veri atama işlemlerinin yapıldığı paneldir. Kullanıcı, verileri seçip belirli durumlara atayabilir. Öncesinde durum eklemelidir.
* **GraphPanel:** Düğümler ve bağlantılar oluşturulur. Kullanıcı, yeni düğümler ekleyebilir ve düğümler arasında bağlantı kurabilir.
* **PriorityQueuePanel:** Öncelik kuyruğu ile veri ekleme ve çıkarma işlemleri yapılır. Veri ekleme sırasında kullanıcıdan öncelik ve açıklama bilgisi alınır.
* **LinkedListPanel:** Hasta verilerinin eklendiği, çıkarıldığı ve güncellendiği paneldir. Panel üzerine Hasta ID'si, ismi ve durum bilgisi yazılabilir.
* **HashMapPanel:** Hastaların acil durum durumlarının hızlıca sorgulanabileceği ve güncellenebileceği paneldir. Key olarak Hasta ID'si, value olarak durum bilgisi yazılabilir.

---

### **3. SimulationService.cs:**

Simülasyon işlemleri bu dosyada yürütülmektedir. İki farklı mod vardır:

* **DS\_BASED:** Veri yapıları kullanılarak işlemler yapılır ve analiz edilir.
* **REGULAR:** Veri yapıları kullanılmadan düz işlemler (foreach) yapılır ve analiz edilir.

 **Simulasyon sonucu TestLogs adlı klasöre bellek kullanımı, simulasyon süresi gibi bilgiler ve simulasyon sonucu oluşan veriler loglanır**

---

### **4. JSONMaker.cs:**

Simülasyon verilerinin oluşturulması ve kaydedilmesi bu dosya üzerinden yapılır. Kullanıcı, belirli büyüklükte veri setleri oluşturabilir ve bu veriler üzerinde simülasyon çalıştırılabilir.

---

### MainForm.cs

MainForm, kullanıcı arayüzünün ana yönetim merkezidir ve simülasyon işlemlerini başlatmak, veri eklemek ve verileri yönetmek için gerekli bileşenleri içerir. Kullanıcılar burada veri yapıları panelleri (Heap, LinkedList, Graph, HashMap) ile etkileşime geçip mod bazlı simülasyonları çalıştırabilir. 

---

### OperationTimer

OperationTimer, bellek kullanımı ve işlem süresi ölçümü için kullanılır. Verilerin işlenme süresi ve bellek tüketimi analiz edilip, kullanıcıya raporlanır. Bu sayede, veri yapılarının performans etkisi gözlemlenebilir ve optimizasyon yapılabilir.

---

## **Test ve Performans Ölçümleri:**

Bu bölümde, **100x1000x4** ve **250x1000x4** veri setleri üzerinde gerçekleştirilen **DS\_BASED** (Veri Yapıları Tabanlı) ve **REGULAR** (Normal arama tabanlı) modlarının performans analizleri sunulmaktadır. Analizler, **süre (ms)** ve **bellek kullanımı (KB)** üzerinden gerçekleştirilmiştir.

###  **Süre (DurationMilliseconds) Karşılaştırması:**

| Veri Seti  | DS\_BASED Süre (ms) | REGULAR Süre (ms) | Fark (ms) | Fark (Kat) | Artış (%) |
| ---------- | ------------------- | ----------------- | --------- | ---------- | --------- |
| 100x1000x4 | 146632              | 186111            | 39479     | 1.27x      | 27%       |
| 250x1000x4 | 431598              | 692945            | 261347    | 1.61x      | 61%       |

**Açıklama:**

* **100x1000x4** veri setinde, REGULAR mod, DS\_BASED moda kıyasla **%27 daha uzun sürede tamamlanmıştır.**
* **250x1000x4** veri setinde ise, REGULAR mod DS\_BASED moda kıyasla **%61 daha uzun sürede tamamlanmıştır.**
* Veri seti boyutu arttıkça, REGULAR modun süresi, DS\_BASED moda göre daha fazla artış göstermektedir.

---

###  **Bellek Kullanımı (TotalMemoryUsageKB) Karşılaştırması:**

| Veri Seti  | DS\_BASED Bellek (KB) | REGULAR Bellek (KB) | Fark (KB)       | Fark (Kat) | Artış (%) |
| ---------- | --------------------- | ------------------- | --------------- | ---------- | --------- |
| 100x1000x4 | 57,039,178,866        | 137,255,526,889     | 80,216,348,023  | 2.40x      | 140%      |
| 250x1000x4 | 364,223,918,389       | 630,224,455,822     | 266,000,537,433 | 1.73x      | 73%       |

**Açıklama:**

* **100x1000x4** veri setinde, REGULAR modun bellek kullanımı DS\_BASED moda göre **%140 daha fazladır.**
* **250x1000x4** veri setinde ise, REGULAR modun bellek kullanımı DS\_BASED moda göre **%73 daha fazladır.**
* DS\_BASED mod, bellek kullanımında genel olarak **daha verimli çalışmaktadır.**

---

### ** Genel Değerlendirme:**

* Veri seti boyutu arttıkça, REGULAR modun bellek kullanımı ve süre artışı daha yüksek oranlarda gerçekleşmiştir.
* **DS\_BASED mod**, özellikle bellek kullanımında daha verimli çalışırken, büyük veri setlerinde işlem süresi açısından avantajını korumaktadır.
* **250x1000x4** veri setinde, REGULAR modun bellek kullanımı DS\_BASED modun yaklaşık **1.73 katına çıkmış**, işlem süresi ise **1.61 kat artmıştır.**

Bu analizler, veri yapılarının etkinliğini ve büyük veri setleri üzerindeki performanslarını net bir şekilde ortaya koymaktadır.

---

