import pytest

def init_tests():
    """Tüm testleri çalıştırır"""
    print("[MAIN] Test sistemi başlatılıyor...")
    pytest.main(["Tests"])  # Tests klasöründeki tüm testleri çalıştırır

if __name__ == "__main__":
    init_tests()   #
