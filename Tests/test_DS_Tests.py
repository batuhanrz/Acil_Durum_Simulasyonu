from DataStructures.heap_priority_queue import PriorityQueue
from DataStructures.linked_list import LinkedList
from DataStructures.hash_map import HashMap
from DataStructures.tree_category import TreeNode

# ===================== HEAP TESTİ =====================
def test_heap_ordering():
    """
    PriorityQueue:
    - Hastalar öncelik değerine göre sıralanır (küçük değer daha yüksek öncelik).
    - Ekleme sırası: A(3), B(1), C(2) → Beklenen çıkarma sırası: B, C, A
    """
    print("\n[TEST] HEAP: Öncelik sırasına göre hasta işleme testi başlatıldı.")
    
    pq = PriorityQueue()
    pq.add_patient("Hasta A", 3)
    pq.add_patient("Hasta B", 1)
    pq.add_patient("Hasta C", 2)

    print("Eklendi: Hasta A (3), Hasta B (1), Hasta C (2)")

    assert pq.get_next_patient() == "Hasta B"
    assert pq.get_next_patient() == "Hasta C"
    assert pq.get_next_patient() == "Hasta A"
    assert pq.is_empty() is True

    print("[OK] Heap testi başarıyla geçti.")

# ===================== LINKED LIST TESTİ =====================
def test_linked_list_order():
    """
    LinkedList:
    - Hastalar geliş sırasına göre listeye eklenir.
    - Ekleme sırası: Ali → Ayşe → Veli
    """
    print("\n[TEST] LINKED LIST: Geliş sırasına göre hasta kaydı testi başlatıldı.")

    ll = LinkedList()
    ll.append("Ali")
    ll.append("Ayşe")
    ll.append("Veli")

    print("Eklendi: Ali → Ayşe → Veli")

    assert str(ll) == "Ali → Ayşe → Veli"
    assert ll.is_empty() is False

    print("[OK] Linked list testi başarıyla geçti.")

# ===================== HASH MAP TESTİ =====================
def test_hash_map_operations():
    """
    HashMap:
    - Hasta ID ile bilgi eşleştirilir.
    - Ekleme, getirme ve silme işlemleri test edilir.
    """
    print("\n[TEST] HASH MAP: Hasta ID bazlı işlem testi başlatıldı.")

    hm = HashMap()
    hm.add_patient("P001", {"name": "Ali", "age": 30})
    hm.add_patient("P002", {"name": "Ayşe", "age": 25})

    print("Eklendi: P001 - Ali, P002 - Ayşe")

    assert hm.get_patient("P001")["name"] == "Ali"
    hm.remove_patient("P001")
    print("Silindi: P001")

    assert hm.get_patient("P001") is None

    print("[OK] HashMap testi başarıyla geçti.")

# ===================== TREE TESTİ =====================
def test_tree_structure():
    """
    TreeNode:
    - Kategori yapısı ağaç şeklinde modellenir.
    - Kök: Acil Durumlar → Alt: Kırmızı, Yeşil
    """
    print("\n[TEST] TREE: Kategori yapısı testi başlatıldı.")

    root = TreeNode("Acil Durumlar", "none")
    kirmizi = TreeNode("Kırmızı", "red")
    yesil = TreeNode("Yeşil", "green")

    root.add_child(kirmizi)
    root.add_child(yesil)

    print("Kategori eklendi: Kırmızı, Yeşil")

    assert root.find("Kırmızı") is not None
    assert root.find("Yeşil").color == "green"
    assert root.find("Mavi") is None

    print("[OK] TreeNode testi başarıyla geçti.")
