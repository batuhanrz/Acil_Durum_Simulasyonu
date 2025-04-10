import customtkinter as ctk
from GUI.patient_form import PatientForm
from DataStructures.heap_priority_queue import PriorityQueue
from DataStructures.linked_list import LinkedList
from DataStructures.hash_map import HashMap
from DataStructures.tree_category import TreeNode

class MainWindow(ctk.CTk):
    def __init__(self):
        super().__init__()

        self.title("Acil Durum Yönetimi Simülasyonu")
        self.geometry("800x600")
        ctk.set_appearance_mode("dark")         
        ctk.set_default_color_theme("blue")    

        self.header = ctk.CTkLabel(self, text="Acil Durum Yönetimi", font=("Arial", 28, "bold"))
        self.header.pack(pady=30)

        self.add_patient_btn = ctk.CTkButton(self, text="Hasta Ekle", command=self.add_patient)
        self.add_patient_btn.pack(pady=10)

        self.show_data_btn = ctk.CTkButton(self, text="Verileri Göster", command=self.show_data)
        self.show_data_btn.pack(pady=10)

        self.simulate_btn = ctk.CTkButton(self, text="Simülasyonu Başlat", command=self.start_simulation)
        self.simulate_btn.pack(pady=10)

        #DS tanımları
        self.heap = PriorityQueue()
        self.linked_list = LinkedList()
        self.hash_map = HashMap()

        # Tree yapısı
        self.tree_root = TreeNode("Acil Durumlar", "none")
        self.red_node = TreeNode("Kırmızı", "red")
        self.yellow_node = TreeNode("Sarı", "yellow")
        self.green_node = TreeNode("Yeşil", "green")
        self.tree_root.add_child(self.red_node)
        self.tree_root.add_child(self.yellow_node)
        self.tree_root.add_child(self.green_node)

    def add_patient(self):
        print("[UI] Hasta ekleme arayüzü açılıyor...")
        PatientForm(self, self.heap, self.linked_list, self.hash_map, self.tree_root)

    def show_data(self):
        print("[UI] Hasta verileri gösterilecek.")

    def start_simulation(self):
        print("[UI] Simülasyon başlatılacak.")
