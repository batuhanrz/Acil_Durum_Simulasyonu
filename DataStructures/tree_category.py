class TreeNode:
    def __init__(self, name, color=None):
        self.name = name
        self.color = color  # Kategoriye özel renk (GUI'de kullanılabilir)
        self.children = []

    def add_child(self, child_node):
        """Alt kategori ekler"""
        self.children.append(child_node)

    def find(self, name):
        """İsme göre node bulur"""
        if self.name == name:
            return self
        for child in self.children:
            result = child.find(name)
            if result:
                return result
        return None

    def __str__(self, level=0):
        ret = "  " * level + f"{self.name} ({self.color})\n"
        for child in self.children:
            ret += child.__str__(level + 1)
        return ret
