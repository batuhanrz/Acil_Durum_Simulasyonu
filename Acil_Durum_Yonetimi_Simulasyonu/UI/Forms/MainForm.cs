using Acil_Durum_Yonetimi_Simulasyonu.DataStructures;
using Acil_Durum_Yonetimi_Simulasyonu.Services;
using Acil_Durum_Yonetimi_Simulasyonu.UI.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Acil_Durum_Yonetimi_Simulasyonu.UI.Forms
{
    public partial class MainForm : Form
    {
        private Button btnStartSimulation;
        private Button btnAddStatus;
        private TextBox txtStatusName;
        private ListBox lstStatusList;
        private DataAssignmentPanel dataAssignmentPanel;
        private ComboBox cmbSimulationMode;
        private Label lblSimulationMode;

        private SimulationService simulationService;
        private PriorityQueuePanel priorityQueuePanel;
        private LinkedListPanel linkedListPanel;
        private HashMapPanel hashMapPanel;
        private GraphPanel graphPanel;

        private List<StatusItem> statusItems;
        private List<string> allDataItems;
        private List<string> statusNames;

        public MainForm()
        {
            InitializeComponent();
            simulationService = new SimulationService();
            simulationService.SimulationStep += OnSimulationStep;

            statusItems = new List<StatusItem>();
            allDataItems = new List<string>();
            statusNames = new List<string>();

            // Data Assignment Panel
            Label lblDataAssignment = new Label
            {
                Text = "Veri Atama Paneli",
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.FromArgb(50, 50, 50),
                Location = new System.Drawing.Point(500, 400),
                AutoSize = true
            };
            this.Controls.Add(lblDataAssignment);

            dataAssignmentPanel = new DataAssignmentPanel
            {
                Location = new System.Drawing.Point(500, 400),
                Size = new System.Drawing.Size(400, 200),
                BackColor = System.Drawing.Color.FromArgb(240, 240, 240)
            };
            dataAssignmentPanel.DataAssigned += OnDataAssigned;
            this.Controls.Add(dataAssignmentPanel);

            // Graph Panel
            Label lblGraphPanel = new Label
            {
                Text = "Graph Panel",
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.FromArgb(50, 50, 50),
                Location = new System.Drawing.Point(1000, 100),
                AutoSize = true
            };
            this.Controls.Add(lblGraphPanel);

            graphPanel = new GraphPanel
            {
                Location = new System.Drawing.Point(1000, 130),
                Size = new System.Drawing.Size(550, 250),
                BackColor = System.Drawing.Color.FromArgb(240, 240, 240)
            };
            this.Controls.Add(graphPanel);

            // Priority Queue Panel
            Label lblPriorityQueuePanel = new Label
            {
                Text = "Öncelik Kuyruğu Paneli",
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.FromArgb(50, 50, 50),
                Location = new System.Drawing.Point(0, 600),
                AutoSize = true
            };
            this.Controls.Add(lblPriorityQueuePanel);

            priorityQueuePanel = new PriorityQueuePanel
            {
                Location = new System.Drawing.Point(0, 630),
                Size = new System.Drawing.Size(340, 200),
                BackColor = System.Drawing.Color.FromArgb(240, 240, 240)
            };
            this.Controls.Add(priorityQueuePanel);

            // LinkedList Panel
            Label lblLinkedListPanel = new Label
            {
                Text = "Linked List Paneli",
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.FromArgb(50, 50, 50),
                Location = new System.Drawing.Point(500, 600),
                AutoSize = true
            };
            this.Controls.Add(lblLinkedListPanel);

            linkedListPanel = new LinkedListPanel
            {
                Location = new System.Drawing.Point(500, 630),
                Size = new System.Drawing.Size(350, 200),
                BackColor = System.Drawing.Color.FromArgb(240, 240, 240)
            };
            this.Controls.Add(linkedListPanel);

            // HashMap Panel
            Label lblHashMapPanel = new Label
            {
                Text = "HashMap Paneli",
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.FromArgb(50, 50, 50),
                Location = new System.Drawing.Point(1000, 600),
                AutoSize = true
            };
            this.Controls.Add(lblHashMapPanel);

            hashMapPanel = new HashMapPanel
            {
                Location = new System.Drawing.Point(1000, 610),
                Size = new System.Drawing.Size(340, 220),
                BackColor = System.Drawing.Color.FromArgb(240, 240, 240)
            };
            this.Controls.Add(hashMapPanel);

            SubscribeToDataUpdates();
        }

        private void InitializeComponent()
        {
            this.Text = "Acil Durum Yönetimi Simülasyonu";
            this.Size = new System.Drawing.Size(1400, 900);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);

            // Başlık
            Label lblTitle = new Label
            {
                Text = "Acil Durum Yönetimi Simülasyonu",
                Font = new System.Drawing.Font("Segoe UI", 18, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.FromArgb(50, 50, 50),
                Location = new System.Drawing.Point(50, 20),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            // Simülasyonu Başlat Butonu
            btnStartSimulation = new Button
            {
                Text = "Simülasyonu Başlat",
                Size = new System.Drawing.Size(180, 45),
                Location = new System.Drawing.Point(50, 80),
                BackColor = System.Drawing.Color.FromArgb(66, 133, 244),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold),
                FlatAppearance = { BorderSize = 0 }
            };
            btnStartSimulation.Click += BtnStartSimulation_Click;
            this.Controls.Add(btnStartSimulation);

            // Simülasyon Modu
            lblSimulationMode = new Label
            {
                Text = "Simülasyon Modu:",
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold),
                Location = new System.Drawing.Point(250, 80),
                AutoSize = true
            };
            this.Controls.Add(lblSimulationMode);

            cmbSimulationMode = new ComboBox
            {
                Size = new System.Drawing.Size(160, 35),
                Location = new System.Drawing.Point(400, 80),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new System.Drawing.Font("Segoe UI", 10),
                BackColor = System.Drawing.Color.White
            };
            cmbSimulationMode.Items.Add("DS_BASED");
            cmbSimulationMode.Items.Add("REGULAR");
            cmbSimulationMode.SelectedIndex = 0;
            this.Controls.Add(cmbSimulationMode);

            // Durum Ekleme
            txtStatusName = new TextBox
            {
                Size = new System.Drawing.Size(200, 35),
                Location = new System.Drawing.Point(580, 80),
                Font = new System.Drawing.Font("Segoe UI", 10),
                BackColor = System.Drawing.Color.White,
                ForeColor = System.Drawing.Color.FromArgb(50, 50, 50),
                Text = "Durum"
            };
            txtStatusName.GotFocus += (s, e) => RemovePlaceholder(txtStatusName, "Durum");
            txtStatusName.LostFocus += (s, e) => SetPlaceholder(txtStatusName, "Durum");
            this.Controls.Add(txtStatusName);

            btnAddStatus = new Button
            {
                Text = "Durum Ekle",
                Size = new System.Drawing.Size(150, 35),
                Location = new System.Drawing.Point(800, 80),
                BackColor = System.Drawing.Color.FromArgb(66, 133, 244),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
            };
            btnAddStatus.Click += BtnAddStatus_Click;
            this.Controls.Add(btnAddStatus);

            // Durum Listesi
            Label lblStatusList = new Label
            {
                Text = "Durum Listesi",
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold),
                Location = new System.Drawing.Point(50, 150),
                AutoSize = true
            };
            this.Controls.Add(lblStatusList);

            lstStatusList = new ListBox
            {
                Size = new System.Drawing.Size(800, 200),
                Location = new System.Drawing.Point(50, 180),
                BackColor = System.Drawing.Color.White
            };
            this.Controls.Add(lstStatusList);
        }



        private void BtnStartSimulation_Click(object sender, EventArgs e)
        {
            lstStatusList.Items.Clear();

            string selectedMode = cmbSimulationMode.SelectedItem.ToString();
            simulationService.Mode = selectedMode == "DS_BASED" ? SimulationMode.DS_BASED : SimulationMode.REGULAR;

            try
            {
                simulationService.LoadDataFromJson();
                simulationService.StartSimulation();
                MessageBox.Show("Simülasyon tamamlandı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAddStatus_Click(object sender, EventArgs e)
        {
            string statusName = txtStatusName.Text.Trim();

            if (!string.IsNullOrEmpty(statusName))
            {
                StatusItem newStatus = new StatusItem(statusName);
                statusItems.Add(newStatus);

                lstStatusList.Items.Add($"Durum: {statusName}");
                txtStatusName.Clear();

                UpdateDataAssignmentPanel();
            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir durum adı girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void OnDataAssigned(string status, string data)
        {
            lstStatusList.Items.Add($"Veri Atandı: {data} → {status}");
        }

        private void UpdateDataAssignmentPanel()
        {
            statusNames = statusItems.Select(s => s.Name).ToList();
            allDataItems.Clear();

            allDataItems.AddRange(priorityQueuePanel.GetAllData());
            allDataItems.AddRange(linkedListPanel.GetAllData());
            allDataItems.AddRange(hashMapPanel.GetAllData());
            allDataItems.AddRange(graphPanel.GetAllData());

            dataAssignmentPanel.UpdateLists(statusNames, allDataItems);
        }

        private void OnSimulationStep(string message)
        {
            lstStatusList.Items.Add(message);
        }

        private void SubscribeToDataUpdates()
        {
            priorityQueuePanel.DataUpdated += UpdateDataAssignmentPanel;
            linkedListPanel.DataUpdated += UpdateDataAssignmentPanel;
            hashMapPanel.DataUpdated += UpdateDataAssignmentPanel;
            graphPanel.DataUpdated += UpdateDataAssignmentPanel;
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
    }
}
