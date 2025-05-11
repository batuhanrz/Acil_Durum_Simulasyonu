using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Acil_Durum_Yonetimi_Simulasyonu.DataStructures;
using Acil_Durum_Yonetimi_Simulasyonu.Services;

namespace Acil_Durum_Yonetimi_Simulasyonu.UI.Components
{
    public partial class LinkedListPanel : UserControl
    {
        private Button btnAdd;
        private Button btnRemove;
        private TextBox txtID;
        private TextBox txtName;
        private TextBox txtCondition;
        private ListBox lstLinkedListItems;
        private RichTextBox rtbAnalysis;
        private DataStructures.LinkedList<string> linkedList;
        public event Action DataUpdated;


        public LinkedListPanel()
        {
            InitializeComponent();
            linkedList = new DataStructures.LinkedList<string>();
            linkedList.ListChanged += OnListChanged;
        }

        private void InitializeComponent()
        {
            this.Size = new System.Drawing.Size(400, 550);

            txtID = new TextBox
            {
                Text = "ID",
                ForeColor = System.Drawing.Color.Gray,
                Size = new System.Drawing.Size(100, 30),
                Location = new System.Drawing.Point(20, 20)
            };
            txtID.GotFocus += RemovePlaceholder;
            txtID.LostFocus += SetPlaceholder;
            this.Controls.Add(txtID);

            txtName = new TextBox
            {
                Text = "Name",
                ForeColor = System.Drawing.Color.Gray,
                Size = new System.Drawing.Size(100, 30),
                Location = new System.Drawing.Point(130, 20)
            };
            txtName.GotFocus += RemovePlaceholder;
            txtName.LostFocus += SetPlaceholder;
            this.Controls.Add(txtName);

            txtCondition = new TextBox
            {
                Text = "Condition",
                ForeColor = System.Drawing.Color.Gray,
                Size = new System.Drawing.Size(100, 30),
                Location = new System.Drawing.Point(240, 20)
            };
            txtCondition.GotFocus += RemovePlaceholder;
            txtCondition.LostFocus += SetPlaceholder;
            this.Controls.Add(txtCondition);

            btnAdd = new Button
            {
                Text = "Add",
                Size = new System.Drawing.Size(100, 30),
                Location = new System.Drawing.Point(20, 60)
            };
            btnAdd.Click += BtnAdd_Click;
            this.Controls.Add(btnAdd);

            btnRemove = new Button
            {
                Text = "Remove",
                Size = new System.Drawing.Size(100, 30),
                Location = new System.Drawing.Point(130, 60)
            };
            btnRemove.Click += BtnRemove_Click;
            this.Controls.Add(btnRemove);

            lstLinkedListItems = new ListBox
            {
                Size = new System.Drawing.Size(350, 250),
                Location = new System.Drawing.Point(20, 100)
            };
            this.Controls.Add(lstLinkedListItems);

            rtbAnalysis = new RichTextBox
            {
                Size = new System.Drawing.Size(350, 100),
                Location = new System.Drawing.Point(20, 360),
                ReadOnly = true
            };
            this.Controls.Add(rtbAnalysis);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            string id = txtID.Text.Trim();
            string name = txtName.Text.Trim();
            string condition = txtCondition.Text.Trim();

            if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(condition))
            {
                string entry = $"ID: {id}, Name: {name}, Condition: {condition}";
                string result = OperationTimer.MeasureTimeAndMemory(() => linkedList.Add(entry));

                AppendAnalysis($"Add: {result}");
                DataUpdated?.Invoke();
                txtID.Clear();
                txtName.Clear();
                txtCondition.Clear();   
            }
            else
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            string id = txtID.Text.Trim();

            if (!string.IsNullOrEmpty(id))
            {
                bool removed = false;

                string result = OperationTimer.MeasureTimeAndMemory(() =>
                {
                    foreach (var entry in linkedList)
                    {
                        if (entry.StartsWith($"ID: {id},"))
                        {
                            removed = linkedList.Remove(entry);
                            break;
                        }
                    }
                });

                AppendAnalysis($"Remove: {result}");
                DataUpdated?.Invoke();

                if (!removed)
                {
                    MessageBox.Show($"ID {id} bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
                txtID.Clear();
            }
            else
            {
                MessageBox.Show("Lütfen bir ID girin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void OnListChanged()
        {
            UpdateListBox();
        }

        private void UpdateListBox()
        {
            lstLinkedListItems.Items.Clear();
            foreach (var entry in linkedList)
            {
                lstLinkedListItems.Items.Add(entry);
            }
        }

        private void AppendAnalysis(string message)
        {
            rtbAnalysis.AppendText($"{message}\n");
        }

        private void RemovePlaceholder(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;

            if (txt.Text == "ID" || txt.Text == "Name" || txt.Text == "Condition")
            {
                txt.Text = "";
                txt.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void SetPlaceholder(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;

            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                if (txt == txtID)
                    txt.Text = "ID";
                else if (txt == txtName)
                    txt.Text = "Name";
                else if (txt == txtCondition)
                    txt.Text = "Condition";

                txt.ForeColor = System.Drawing.Color.Gray;
            }
        }

        public List<string> GetAllData()
        {
            var dataList = new List<string>();

            foreach (var item in linkedList)
            {
                dataList.Add(item);
            }

            return dataList;
        }

    }
}
