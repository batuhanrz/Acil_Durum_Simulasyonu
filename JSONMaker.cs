using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Acil_Durum_Yonetimi_Simulasyonu.Services
{
    public class JSONMaker
    {
        private const int NumberOfStatuses = 250;
        private const int DataPerStatus = 1000;

        public void GenerateLargeSimulationData()
        {
            List<StatusData> statuses = new List<StatusData>();
            int patientIdCounter = 1000;

            for (int i = 0; i < NumberOfStatuses; i++)
            {
                char statusChar = (char)('A' + i);
                string statusName = $"Durum {statusChar}";

                List<PriorityEntry> priorityQueue = new List<PriorityEntry>();
                List<PatientEntry> linkedList = new List<PatientEntry>();
                List<GraphNodeData> graph = new List<GraphNodeData>();
                List<HashMapEntry> hashMap = new List<HashMapEntry>();

                for (int j = 0; j < DataPerStatus; j++)
                {
                    int priority = (j % 5) + 1;
                    string description = $"Açıklama {j + 1} - Durum {statusChar}";
                    priorityQueue.Add(new PriorityEntry { Priority = priority, Description = description });
                }

                for (int j = 0; j < DataPerStatus; j++)
                {
                    string patientId = (patientIdCounter++).ToString();
                    string patientName = $"Hasta {patientId}";
                    string condition = $"Durum {statusChar} - Hasta {j + 1}";

                    linkedList.Add(new PatientEntry { PatientId = patientId, Name = patientName, Condition = condition });
                    hashMap.Add(new HashMapEntry { PatientId = patientId, Status = $"Durum {statusChar}" });
                }

                for (int j = 1; j <= DataPerStatus; j++)
                {
                    string nodeName = $"Oda {statusChar}{j}";
                    List<string> connections = new List<string>();

                    if (j > 1) connections.Add($"Oda {statusChar}{j - 1}");
                    if (j < DataPerStatus) connections.Add($"Oda {statusChar}{j + 1}");

                    graph.Add(new GraphNodeData { Node = nodeName, Connections = connections });
                }

                statuses.Add(new StatusData
                {
                    StatusName = statusName,
                    PriorityQueue = priorityQueue,
                    LinkedList = linkedList,
                    Graph = graph,
                    HashMap = hashMap
                });
            }

            SimulationData simulationData = new SimulationData { Statuses = statuses };
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "Data", "simulation_data_generated.json");

            try
            {
                string json = JsonConvert.SerializeObject(simulationData, Formatting.Indented);
                File.WriteAllText(filePath, json);
                Console.WriteLine($"Data successfully generated at {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating data: {ex.Message}");
            }
        }
    }

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

    public class PriorityEntry
    {
        public int Priority { get; set; }
        public string Description { get; set; }
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
