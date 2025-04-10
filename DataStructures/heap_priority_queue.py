import heapq

class PriorityQueue:
    def __init__(self):
        # Heap yapısı: (öncelik, hasta_sayacı, hasta_adı)
        self.heap = []
        self.counter = 0  # Aynı önceliğe sahip hastalar arasında FIFO sağlar

    def add_patient(self, name, priority):
        """Bir hastayı önceliği ile birlikte kuyruğa ekle"""
        heapq.heappush(self.heap, (priority, self.counter, name))
        self.counter += 1

    def get_next_patient(self):
        """Önceliği en yüksek (sayısal olarak en düşük) hastayı çıkarır"""
        if not self.is_empty():
            return heapq.heappop(self.heap)[2]  # sadece hasta adını döndür
        return None

    def is_empty(self):
        """Kuyruk boş mu?"""
        return len(self.heap) == 0

    def __str__(self):
        """Debug için kuyruktaki hastaları sırayla göster"""
        sorted_heap = sorted(self.heap)
        return '\n'.join([f"{item[2]} (öncelik: {item[0]})" for item in sorted_heap])
