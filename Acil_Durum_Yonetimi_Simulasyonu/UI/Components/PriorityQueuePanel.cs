using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Acil_Durum_Yonetimi_Simulasyonu.DataStructures;
using Acil_Durum_Yonetimi_Simulasyonu.Services;
using PriorityEntry = Acil_Durum_Yonetimi_Simulasyonu.DataStructures.PriorityEntry;

namespace Acil_Durum_Yonetimi_Simulasyonu.UI.Components
{
    public partial class PriorityQueuePanel : UserControl
    {
        private Button btnEnqueue;
        private Button btnDequeue;
        private Button btnHeapify;
        private ListBox lstHeapItems;
        private TextBox txtPriority;
        private TextBox txtDescription;
        private RichTextBox rtbAnalysis;
        private Heap<PriorityEntry> priorityQueue;
        public event Action DataUpdated;

        public PriorityQueuePanel()
        {
            InitializeComponent();
            priorityQueue = new Heap<PriorityEntry>(new PriorityComparer());
            priorityQueue.HeapChanged += OnHeapChanged;
        }

        private void InitializeComponent()
        {
            this.Size = new System.Drawing.Size(350, 550);

            txtPriority = new TextBox
            {
                Size = new System.Drawing.Size(100, 30),
                Location = new System.Drawing.Point(20, 20),
                ForeColor = Color.Gray,
                Text = "Priority"
            };
            txtPriority.GotFocus += (s, e) => RemovePlaceholder(txtPriority, "Priority");
            txtPriority.LostFocus += (s, e) => SetPlaceholder(txtPriority, "Priority");
            this.Controls.Add(txtPriority);

            txtDescription = new TextBox
            {
                Size = new System.Drawing.Size(150, 30),
                Location = new System.Drawing.Point(130, 20),
                ForeColor = Color.Gray,
                Text = "Description"
            };
            txtDescription.GotFocus += (s, e) => RemovePlaceholder(txtDescription, "Description");
            txtDescription.LostFocus += (s, e) => SetPlaceholder(txtDescription, "Description");
            this.Controls.Add(txtDescription);

            btnEnqueue = new Button
            {
                Text = "Enqueue",
                Size = new System.Drawing.Size(100, 30),
                Location = new System.Drawing.Point(20, 60)
            };
            btnEnqueue.Click += BtnEnqueue_Click;
            this.Controls.Add(btnEnqueue);

            btnDequeue = new Button
            {
                Text = "Dequeue",
                Size = new System.Drawing.Size(100, 30),
                Location = new System.Drawing.Point(130, 60)
            };
            btnDequeue.Click += BtnDequeue_Click;
            this.Controls.Add(btnDequeue);

            btnHeapify = new Button
            {
                Text = "Heapify",
                Size = new System.Drawing.Size(100, 30),
                Location = new System.Drawing.Point(240, 60)
            };
            btnHeapify.Click += BtnHeapify_Click;
            this.Controls.Add(btnHeapify);

            lstHeapItems = new ListBox
            {
                Size = new System.Drawing.Size(300, 200),
                Location = new System.Drawing.Point(20, 100)
            };
            this.Controls.Add(lstHeapItems);

            rtbAnalysis = new RichTextBox
            {
                Size = new System.Drawing.Size(300, 100),
                Location = new System.Drawing.Point(20, 320),
                ReadOnly = true
            };
            this.Controls.Add(rtbAnalysis);
        }

        private void BtnEnqueue_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtPriority.Text, out int priority) && !string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                var entry = new PriorityEntry(priority, txtDescription.Text);
                string result = OperationTimer.MeasureTimeAndMemory(() => priorityQueue.Enqueue(entry));

                txtPriority.Clear();
                txtDescription.Clear();
                SetPlaceholder(txtPriority, "Priority");
                SetPlaceholder(txtDescription, "Description");

                AppendAnalysis($"Enqueue (Priority: {entry.Priority}, Description: {entry.Description}): {result}");
                DataUpdated?.Invoke();
            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir öncelik ve açıklama girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDequeue_Click(object sender, EventArgs e)
        {
            try
            {
                string result = OperationTimer.MeasureTimeAndMemory(() =>
                {
                    var removedEntry = priorityQueue.Dequeue();
                    MessageBox.Show($"Dequeue: {removedEntry.Priority} - {removedEntry.Description}", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                });

                AppendAnalysis($"Dequeue: {result}");
                DataUpdated?.Invoke();
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Heap boş!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnHeapify_Click(object sender, EventArgs e)
        {
            string result = OperationTimer.MeasureTimeAndMemory(() => priorityQueue.Heapify());
            AppendAnalysis($"Heapify: {result}");
            UpdateListBox();
            MessageBox.Show("Heap yeniden düzenlendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void OnHeapChanged()
        {
            UpdateListBox();
        }

        private void UpdateListBox()
        {
            lstHeapItems.Items.Clear();

            var sortedEntries = priorityQueue.ToList();
            sortedEntries.Sort((x, y) => x.Priority.CompareTo(y.Priority));

            foreach (var entry in sortedEntries)
            {
                lstHeapItems.Items.Add($"Priority: {entry.Priority}, Desc: {entry.Description}");
            }
        }

        private void AppendAnalysis(string message)
        {
            rtbAnalysis.AppendText($"{message}\n");
        }

        private void SetPlaceholder(TextBox textBox, string placeholder)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.ForeColor = Color.Gray;
                textBox.Text = placeholder;
            }
        }

        private void RemovePlaceholder(TextBox textBox, string placeholder)
        {
            if (textBox.Text == placeholder)
            {
                textBox.Clear();
                textBox.ForeColor = Color.Black;
            }
        }

        public List<string> GetAllData()
        {
            var dataList = new List<string>();

            foreach (var entry in priorityQueue)
            {
                dataList.Add($"Priority: {entry.Priority}, Desc: {entry.Description}");
            }

            return dataList;
        }

    }
}
