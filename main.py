import pytest
from GUI.main_window import MainWindow

def init_tests():
    """Tüm testleri çalıştırır"""
    print("[MAIN] Test sistemi başlatılıyor...")
    # pytest.main(["Tests"])  # Tests klasöründeki tüm testleri çalıştırır

def init_gui():
    """CustomTkinter GUI'yi başlatır"""
    print("[MAIN] GUI başlatılıyor...")
    app = MainWindow()
    app.mainloop()

if __name__ == "__main__":
    # init_tests()   # Testleri çalıştırmak istersen yorumdan çıkar
    init_gui()       # GUI'yi başlat
