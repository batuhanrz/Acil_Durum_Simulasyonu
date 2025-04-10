class HashMap:
    def __init__(self):
        self.data = {}

    def add_patient(self, patient_id, patient_info):
        """
        Hasta bilgilerini ID ile eşleştirerek ekler.
        patient_info → dict: {'name': ..., 'age': ..., 'priority': ...}
        """
        self.data[patient_id] = patient_info

    def get_patient(self, patient_id):
        """Belirtilen ID'ye sahip hastanın bilgilerini döndürür"""
        return self.data.get(patient_id, None)

    def remove_patient(self, patient_id):
        """Belirtilen ID'deki hastayı siler (varsa)"""
        if patient_id in self.data:
            del self.data[patient_id]

    def __str__(self):
        result = []
        for pid, info in self.data.items():
            result.append(f"{pid}: {info}")
        return "\n".join(result) if result else "Kayıtlı hasta yok."
