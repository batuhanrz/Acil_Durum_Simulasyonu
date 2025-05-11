using Acil_Durum_Yonetimi_Simulasyonu.Services;
using Acil_Durum_Yonetimi_Simulasyonu.UI.Components;
using Acil_Durum_Yonetimi_Simulasyonu.UI.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Acil_Durum_Yonetimi_Simulasyonu
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new MainForm());

            //JSONMaker jsonMaker = new JSONMaker();
            //jsonMaker.GenerateLargeSimulationData();

            //Console.WriteLine("JSON veri oluşturuldu!");
            //Console.ReadLine();
            // -----------------------------------------------------------------
            // PRIORITY QUEUE PANEL TEST
            // -----------------------------------------------------------------
            //Form mainForm = new Form
            //{
            //    Text = "Priority Queue Panel Test",
            //    Size = new System.Drawing.Size(400, 450),
            //    StartPosition = FormStartPosition.CenterScreen
            //};

            //PriorityQueuePanel priorityQueuePanel = new PriorityQueuePanel
            //{
            //    Location = new System.Drawing.Point(30, 30)
            //};
            //mainForm.Controls.Add(priorityQueuePanel);

            //Application.Run(mainForm);

            // -----------------------------------------------------------------
            // LINKED LIST PANEL TEST
            // -----------------------------------------------------------------
            //Form mainForm = new Form
            //{
            //    Text = "Linked List Panel Test",
            //    Size = new System.Drawing.Size(400, 450),
            //    StartPosition = FormStartPosition.CenterScreen
            //};

            //LinkedListPanel linkedListPanel = new LinkedListPanel
            //{
            //    Location = new System.Drawing.Point(30, 30)
            //};
            //mainForm.Controls.Add(linkedListPanel);

            //Application.Run(mainForm);

            // -----------------------------------------------------------------
            // GRAPH PANEL TEST
            // -----------------------------------------------------------------
            //Form mainForm = new Form
            //{
            //    Text = "Graph Panel Test",
            //    Size = new System.Drawing.Size(400, 550),
            //    StartPosition = FormStartPosition.CenterScreen
            //};

            //GraphPanel graphPanel = new GraphPanel
            //{
            //    Location = new System.Drawing.Point(30, 30)
            //};
            //mainForm.Controls.Add(graphPanel);

            //Application.Run(mainForm);

            // -----------------------------------------------------------------
            //  HASHMAP PANEL TEST
            // -----------------------------------------------------------------
            //Form mainForm = new Form
            //{
            //    Text = "HashMap Panel Test",
            //    Size = new System.Drawing.Size(400, 550),
            //    StartPosition = FormStartPosition.CenterScreen
            //};

            //HashMapPanel hashMapPanel = new HashMapPanel();
            //hashMapPanel.Location = new System.Drawing.Point(30, 30);
            //mainForm.Controls.Add(hashMapPanel);

            //Application.Run(mainForm);

            // -----------------------------------------------------------------

            // -----------------------------------------------------------------
        }
    }
}
