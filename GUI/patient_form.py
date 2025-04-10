import customtkinter as ctk
from DataStructures.tree_category import TreeNode
from tkcalendar import DateEntry
from datetime import date


class PatientForm(ctk.CTkToplevel):
    def __init__(self, master, heap, linked_list, hash_map, tree_root):
        super().__init__(master)

        # Veri yapıları
        self.heap = heap
        self.linked_list = linked_list
        self.hash_map = hash_map
        self.tree_root = tree_root

        self.title("Hasta Ekle")
        self.geometry("400x400")

        # Giriş bileşenleri
        ctk.CTkLabel(self, text="Ad:").pack(pady=5)
        self.name_entry = ctk.CTkEntry(self, placeholder_text="Adı giriniz")
        self.name_entry.pack(pady=5)

        ctk.CTkLabel(self, text="Doğum Tarihi:").pack(pady=5)
        self.birth_entry = DateEntry(self, width=20, background='darkblue',
                                     foreground='white', borderwidth=2, year=2000, locale='tr_TR')
        self.birth_entry.pack(pady=5)

        ctk.CTkLabel(self, text="Durum:").pack(pady=5)
        self.condition_combo = ctk.CTkComboBox(self, values=["Kalp Krizi", "Kırık", "Soğuk Algınlığı", "Yüksek Ateş"])
        self.condition_combo.set("Kalp Krizi")
        self.condition_combo.pack(pady=5)

        ctk.CTkLabel(self, text="Öncelik (1=Acil, 3=Düşük):").pack(pady=5)
        self.priority_combo = ctk.CTkComboBox(self, values=["1", "2", "3"])
        self.priority_combo.set("1")
        self.priority_combo.pack(pady=5)

        submit_button = ctk.CTkButton(self, text="Ekle", command=self.submit)
        submit_button.pack(pady=20)

    def submit(self):
        name = self.name_entry.get().strip()
        condition = self.condition_combo.get()
        priority = int(self.priority_combo.get())

        if not name:
            print("[HATA] Ad alanı boş bırakılamaz.")
            return

        try:
            birth_date = self.birth_entry.get_date()
        except Exception:
            print("[HATA] Geçersiz doğum tarihi.")
            return

        today = date.today()
        age = today.year - birth_date.year - ((today.month, today.day) < (birth_date.month, birth_date.day))
        patient_id = f"P{len(self.hash_map.data)+1:03}"

        # 1. Heap'e ekle
        self.heap.add_patient(name, priority)

        # 2. Linked List'e ekle
        self.linked_list.append(name)

        # 3. Hash Map'e ekle
        self.hash_map.add_patient(patient_id, {
            "name": name,
            "birthdate": birth_date.strftime("%Y-%m-%d"),
            "age": age,
            "condition": condition,
            "priority": priority
        })

        # 4. Tree sınıflandırması
        if priority == 1:
            self.tree_root.find("Kırmızı").add_child(TreeNode(name, "red"))
        elif priority == 2:
            self.tree_root.find("Sarı").add_child(TreeNode(name, "yellow"))
        elif priority == 3:
            self.tree_root.find("Yeşil").add_child(TreeNode(name, "green"))

        print(f"[EKLE] Hasta {name} ({patient_id}) başarıyla eklendi. Yaş: {age}")
        self.destroy()
