using System;
using System.Collections.Generic;

namespace Acil_Durum_Yonetimi_Simulasyonu.DataStructures
{
    public class SimulationData
    {
        public List<StatusData> Statuses { get; set; }
    }
    public class StatusData
    {
        public string StatusName { get; set; }
        public List<PriorityEntry> PriorityQueue { get; set; }
        public List<PatientEntry> LinkedList { get; set; }
        public List<GraphNodeData> Graph { get; set; }
        public List<HashMapEntry> HashMap { get; set; }
    }
    public class PatientEntry
    {
        public string PatientId { get; set; }
        public string Name { get; set; }
        public string Condition { get; set; }
    }
    public class GraphNodeData
    {
        public string Node { get; set; }
        public List<string> Connections { get; set; }
    }
    public class HashMapEntry
    {
        public string PatientId { get; set; }
        public string Status { get; set; }
    }
}
