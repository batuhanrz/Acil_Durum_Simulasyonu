class Node:
    def __init__(self, name):
        self.name = name
        self.next = None

class LinkedList:
    def __init__(self):
        self.head = None

    def append(self, name):
        """Liste sonuna yeni bir hasta ekler"""
        new_node = Node(name)
        if not self.head:
            self.head = new_node
        else:
            current = self.head
            while current.next:
                current = current.next
            current.next = new_node

    def is_empty(self):
        """Liste boş mu"""
        return self.head is None

    def __str__(self):
        """Listedeki tüm hastaları sırayla döndürür"""
        patients = []
        current = self.head
        while current:
            patients.append(current.name)
            current = current.next
        return " → ".join(patients) if patients else "Liste boş"
