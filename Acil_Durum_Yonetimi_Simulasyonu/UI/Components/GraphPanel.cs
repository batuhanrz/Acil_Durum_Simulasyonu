using System;
using System.Windows.Forms;
using Acil_Durum_Yonetimi_Simulasyonu.DataStructures;
using Acil_Durum_Yonetimi_Simulasyonu.Services;
using System.Drawing;
using System.Collections.Generic;

namespace Acil_Durum_Yonetimi_Simulasyonu.UI.Components
{
    public partial class GraphPanel : UserControl
    {
        private Button btnAddNode;
        private Button btnAddEdge;
        private TextBox txtNodeInput;
        private TextBox txtEdgeSource;
        private TextBox txtEdgeDestination;
        private CheckBox chkBidirectional;
        private ListBox lstGraphItems;
        private RichTextBox rtbAnalysis;
        private Graph<string> graph;
        public event Action DataUpdated;


        public GraphPanel()
        {
            InitializeComponent();
            graph = new Graph<string>();
            graph.GraphChanged += OnGraphChanged;
        }

        private void InitializeComponent()
        {
            this.Size = new System.Drawing.Size(350, 600);

            txtNodeInput = new TextBox
            {
                Size = new System.Drawing.Size(120, 30),
                Location = new System.Drawing.Point(20, 20),
                ForeColor = Color.Gray,
                Text = "Node"
            };
            txtNodeInput.GotFocus += (s, e) => RemovePlaceholder(txtNodeInput, "Node");
            txtNodeInput.LostFocus += (s, e) => SetPlaceholder(txtNodeInput, "Node");
            this.Controls.Add(txtNodeInput);

            btnAddNode = new Button
            {
                Text = "Add Node",
                Size = new System.Drawing.Size(100, 30),
                Location = new System.Drawing.Point(160, 20)
            };
            btnAddNode.Click += BtnAddNode_Click;
            this.Controls.Add(btnAddNode);

            txtEdgeSource = new TextBox
            {
                Size = new System.Drawing.Size(120, 30),
                Location = new System.Drawing.Point(20, 70),
                ForeColor = Color.Gray,
                Text = "Source"
            };
            txtEdgeSource.GotFocus += (s, e) => RemovePlaceholder(txtEdgeSource, "Source");
            txtEdgeSource.LostFocus += (s, e) => SetPlaceholder(txtEdgeSource, "Source");
            this.Controls.Add(txtEdgeSource);

            txtEdgeDestination = new TextBox
            {
                Size = new System.Drawing.Size(120, 30),
                Location = new System.Drawing.Point(160, 70),
                ForeColor = Color.Gray,
                Text = "Destination"
            };
            txtEdgeDestination.GotFocus += (s, e) => RemovePlaceholder(txtEdgeDestination, "Destination");
            txtEdgeDestination.LostFocus += (s, e) => SetPlaceholder(txtEdgeDestination, "Destination");
            this.Controls.Add(txtEdgeDestination);

            chkBidirectional = new CheckBox
            {
                Text = "Bidirectional",
                Location = new System.Drawing.Point(20, 110),
                AutoSize = true
            };
            this.Controls.Add(chkBidirectional);

            btnAddEdge = new Button
            {
                Text = "Add Edge",
                Size = new System.Drawing.Size(100, 30),
                Location = new System.Drawing.Point(160, 110)
            };
            btnAddEdge.Click += BtnAddEdge_Click;
            this.Controls.Add(btnAddEdge);

            lstGraphItems = new ListBox
            {
                Size = new System.Drawing.Size(300, 250),
                Location = new System.Drawing.Point(20, 160)
            };
            this.Controls.Add(lstGraphItems);

            rtbAnalysis = new RichTextBox
            {
                Size = new System.Drawing.Size(300, 100),
                Location = new System.Drawing.Point(20, 430),
                ReadOnly = true
            };
            this.Controls.Add(rtbAnalysis);
        }
        private void BtnAddNode_Click(object sender, EventArgs e)
        {
            string nodeData = txtNodeInput.Text.Trim();

            if (!string.IsNullOrEmpty(nodeData))
            {
                string result = OperationTimer.MeasureTimeAndMemory(() =>
                {
                    graph.AddNode(nodeData);
                });

                AppendAnalysis($"Add Node ({nodeData}): {result}");
                DataUpdated?.Invoke();
                txtNodeInput.Clear();
            }
            else
            {
                MessageBox.Show("Lütfen bir düğüm girin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void BtnAddEdge_Click(object sender, EventArgs e)
        {
            string source = txtEdgeSource.Text.Trim();
            string destination = txtEdgeDestination.Text.Trim();
            bool isBidirectional = chkBidirectional.Checked;

            if (!string.IsNullOrEmpty(source) && !string.IsNullOrEmpty(destination))
            {
                if (graph.Contains(source) && graph.Contains(destination))
                {
                    string result = OperationTimer.MeasureTimeAndMemory(() =>
                    {
                        graph.AddEdge(source, destination, isBidirectional);
                    });

                    AppendAnalysis($"Add Edge ({source} → {destination}): {result}");
                    DataUpdated?.Invoke();
                    txtEdgeSource.Clear();
                    txtEdgeDestination.Clear();
                }
                else
                {
                    MessageBox.Show("Kaynak veya hedef düğüm mevcut değil!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Lütfen geçerli düğümler girin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void OnGraphChanged()
        {
            UpdateGraphDisplay();
        }
        private void UpdateGraphDisplay()
        {
            lstGraphItems.Items.Clear();

            foreach (var node in graph.GetAllNodes())
            {
                string nodeInfo = $"{node.Data}: ";
                foreach (var neighbor in node.Neighbors)
                {
                    nodeInfo += $"{neighbor.Data} ";
                }

                lstGraphItems.Items.Add(nodeInfo.TrimEnd(' '));
            }
        }
        private void AppendAnalysis(string message)
        {
            rtbAnalysis.AppendText($"{message}\n");
        }

        public List<string> GetAllData()
        {
            var dataList = new List<string>();

            foreach (var node in graph.GetAllNodes())
            {
                string nodeData = $"{node.Data}: ";

                foreach (var neighbor in node.Neighbors)
                {
                    nodeData += $"{neighbor.Data} ";
                }

                dataList.Add(nodeData.Trim());
            }

            return dataList;
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
