using System;
using System.Collections.Generic;
using System.Text;

namespace Acil_Durum_Yonetimi_Simulasyonu.DataStructures
{
    public class StatusItem
    {
        public string Name { get; set; }
        public List<string> DataItems { get; set; }

        public StatusItem(string name)
        {
            Name = name;
            DataItems = new List<string>();
        }
        public void AddData(string data)
        {
            DataItems.Add(data);
        }
        public string GetFormattedData()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"--- {Name} ---");
            foreach (string data in DataItems)
            {
                sb.AppendLine(data);
            }
            return sb.ToString();
        }
    }
}
