using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using Acil_Durum_Yonetimi_Simulasyonu.DataStructures;
using Acil_Durum_Yonetimi_Simulasyonu.Services;

namespace Acil_Durum_Yonetimi_Simulasyonu.Services
{
    public enum SimulationMode
    {
        DS_BASED,
        REGULAR
    }

    public class SimulationService
    {
        public event Action<string> SimulationStep;
        public SimulationMode Mode { get; set; } = SimulationMode.DS_BASED;
        private List<StatusData> _statuses;
        private List<LogEntry> _logEntries;
        private readonly string logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "TestLogs");
        private long _totalMemoryUsage;

        public SimulationService()
        {
            _statuses = new List<StatusData>();
            _logEntries = new List<LogEntry>();
            _totalMemoryUsage = 0;

            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }
        }
        public void LoadDataFromJson()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "Data", "simulation_data.json");

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"JSON dosyası bulunamadı: {filePath}");

            string jsonData = File.ReadAllText(filePath);
            var data = JsonConvert.DeserializeObject<SimulationData>(jsonData);

            if (data == null || data.Statuses == null) return;

            _statuses.Clear();
            _statuses.AddRange(data.Statuses);
        }
        public void StartSimulation()
        {
            _logEntries.Clear();
            _totalMemoryUsage = 0;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            SimulationStep?.Invoke($"--- Simülasyon Başladı - Mod: {Mode} ---");

            if (_statuses.Count == 0)
            {
                SimulationStep?.Invoke("Simülasyon verisi bulunamadı.");
                return;
            }

            foreach (var status in _statuses)
            {
                if (Mode == SimulationMode.DS_BASED)
                {
                    ProcessStatusData_DS_BASED(status);
                }
                else if (Mode == SimulationMode.REGULAR)
                {
                    ProcessStatusData_REGULAR(status);
                }
            }

            stopwatch.Stop();
            long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

            SimulationStep?.Invoke($"--- Simülasyon Tamamlandı - Süre: {elapsedMilliseconds} ms, Toplam Bellek: {_totalMemoryUsage / 1024} KB ---");

            SaveLogToFile(elapsedMilliseconds);
        }

        private void ProcessStatusData_DS_BASED(StatusData status)
        {
            SimulationStep?.Invoke($"--- Durum: {status.StatusName} (DS_BASED) ---");

            ProcessPriorityQueue(status.PriorityQueue);
            ProcessLinkedList(status.LinkedList);
            ProcessGraph(status.Graph);
            ProcessHashMap(status.HashMap);
        }

        private void ProcessStatusData_REGULAR(StatusData status)
        {
            SimulationStep?.Invoke($"--- Durum: {status.StatusName} (REGULAR) ---");

            foreach (var entry in status.PriorityQueue)
                LogData("PriorityQueue", entry.Priority.ToString(), entry.Description);

            foreach (var patient in status.LinkedList)
                LogData("LinkedList", patient.PatientId, $"{patient.Name} - {patient.Condition}");

            foreach (var node in status.Graph)
                LogData("Graph", node.Node, string.Join(", ", node.Connections));

            foreach (var entry in status.HashMap)
                LogData("HashMap", entry.PatientId, entry.Status);
        }

        private void ProcessPriorityQueue(List<PriorityEntry> priorityQueue)
        {
            SimulationStep?.Invoke("---- Priority Queue İşlemleri ----");

            foreach (var entry in priorityQueue)
                LogData("PriorityQueue", entry.Priority.ToString(), entry.Description);
        }

        private void ProcessLinkedList(List<PatientEntry> linkedList)
        {
            SimulationStep?.Invoke("---- Linked List İşlemleri ----");

            foreach (var patient in linkedList)
                LogData("LinkedList", patient.PatientId, $"{patient.Name} - {patient.Condition}");
        }

        private void ProcessGraph(List<GraphNodeData> graph)
        {
            SimulationStep?.Invoke("---- Graph İşlemleri ----");

            foreach (var node in graph)
                LogData("Graph", node.Node, string.Join(", ", node.Connections));
        }

        private void ProcessHashMap(List<HashMapEntry> hashMap)
        {
            SimulationStep?.Invoke("---- HashMap İşlemleri ----");

            foreach (var entry in hashMap)
                LogData("HashMap", entry.PatientId, entry.Status);
        }
        private void LogData(string dataType, string identifier, string data)
        {
            long memoryUsage = GC.GetTotalMemory(false);
            _totalMemoryUsage += memoryUsage;

            string logMessage = $"{dataType} - {identifier}: {data}";
            SimulationStep?.Invoke(logMessage);

            _logEntries.Add(new LogEntry
            {
                Timestamp = DateTime.Now,
                DataType = dataType,
                Identifier = identifier,
                Data = data,
                MemoryUsageKB = memoryUsage / 1024,
                Mode = Mode.ToString()
            });
        }
        private void SaveLogToFile(long elapsedMilliseconds)
        {
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string fileName = $"SimulationLog_{Mode}_{timestamp}.json";
            string filePath = Path.Combine(logDirectory, fileName);

            var logData = new LogFile
            {
                Mode = Mode.ToString(),
                DurationMilliseconds = elapsedMilliseconds,
                TotalMemoryUsageKB = _totalMemoryUsage / 1024,
                Entries = _logEntries
            };

            string json = JsonConvert.SerializeObject(logData, Formatting.Indented);
            File.WriteAllText(filePath, json);

            SimulationStep?.Invoke($"Log dosyası oluşturuldu: {filePath}");
        }
    }

    public class LogEntry
    {
        public DateTime Timestamp { get; set; }
        public string DataType { get; set; }
        public string Identifier { get; set; }
        public string Data { get; set; }
        public long MemoryUsageKB { get; set; }
        public string Mode { get; set; }
    }

    public class LogFile
    {
        public string Mode { get; set; }
        public long DurationMilliseconds { get; set; }
        public long TotalMemoryUsageKB { get; set; }
        public List<LogEntry> Entries { get; set; }
    }
}
