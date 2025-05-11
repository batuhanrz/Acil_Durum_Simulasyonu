using System;
using System.Collections.Generic;

namespace Acil_Durum_Yonetimi_Simulasyonu.DataStructures
{
    public class PriorityEntry
    {
        public int Priority { get; set; }
        public string Description { get; set; }

        public PriorityEntry(int priority, string description)
        {
            this.Priority = priority; 
            this.Description = description;
        }

        public override string ToString()
        {
            return $"Priority: {this.Priority}, Description: {this.Description}"; 
        }
    }

    public class PriorityComparer : IComparer<PriorityEntry>
    {
        public int Compare(PriorityEntry x, PriorityEntry y)
        {
            if (x == null || y == null)
                throw new ArgumentException("Karşılaştırılacak nesneler null olamaz.");

            return x.Priority.CompareTo(y.Priority); 
        }
    }
}
