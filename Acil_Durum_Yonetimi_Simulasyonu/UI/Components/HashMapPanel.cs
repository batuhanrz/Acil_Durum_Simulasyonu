using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Acil_Durum_Yonetimi_Simulasyonu.DataStructures;
using Acil_Durum_Yonetimi_Simulasyonu.Services;

namespace Acil_Durum_Yonetimi_Simulasyonu.UI.Components
{
    public partial class HashMapPanel : UserControl
    {
        private Button btnPut;
        private Button btnRemove;
        private Button btnGet;
        private TextBox txtKeyInput;
        private TextBox txtValueInput;
        private TextBox txtGetKeyInput;
        private ListBox lstHashMapItems;
        private RichTextBox rtbAnalysis;
        private HashMap<string, string> hashMap;
        public event Action DataUpdated;


        public HashMapPanel()
        {
            InitializeComponent();
            hashMap = new HashMap<string, string>();
            hashMap.HashMapChanged += OnHashMapChanged;
        }

        private void InitializeComponent()
        {
            this.Size = new System.Drawing.Size(350, 550);

            txtKeyInput = new TextBox
            {
                Size = new System.Drawing.Size(100, 30),
                Location = new System.Drawing.Point(20, 20),
                ForeColor = Color.Gray,
                Text = "Key"
            };
            txtKeyInput.GotFocus += (s, e) => RemovePlaceholder(txtKeyInput, "Key");
            txtKeyInput.LostFocus += (s, e) => SetPlaceholder(txtKeyInput, "Key");

            txtValueInput = new TextBox
            {
                Size = new System.Drawing.Size(100, 30),
                Location = new System.Drawing.Point(130, 20),
                ForeColor = Color.Gray,
                Text = "Value"
            };
            txtValueInput.GotFocus += (s, e) => RemovePlaceholder(txtValueInput, "Value");
            txtValueInput.LostFocus += (s, e) => SetPlaceholder(txtValueInput, "Value");

            btnPut = new Button
            {
                Text = "Put",
                Size = new System.Drawing.Size(100, 30),
                Location = new System.Drawing.Point(240, 20)
            };
            btnPut.Click += BtnPut_Click;

            txtGetKeyInput = new TextBox
            {
                Size = new System.Drawing.Size(100, 30),
                Location = new System.Drawing.Point(20, 70),
                ForeColor = Color.Gray,
                Text = "Get Key"
            };
            txtGetKeyInput.GotFocus += (s, e) => RemovePlaceholder(txtGetKeyInput, "Get Key");
            txtGetKeyInput.LostFocus += (s, e) => SetPlaceholder(txtGetKeyInput, "Get Key");

            btnGet = new Button
            {
                Text = "Get",
                Size = new System.Drawing.Size(100, 30),
                Location = new System.Drawing.Point(130, 70)
            };
            btnGet.Click += BtnGet_Click;

            btnRemove = new Button
            {
                Text = "Remove",
                Size = new System.Drawing.Size(100, 30),
                Location = new System.Drawing.Point(240, 70)
            };
            btnRemove.Click += BtnRemove_Click;

            lstHashMapItems = new ListBox
            {
                Size = new System.Drawing.Size(300, 250),
                Location = new System.Drawing.Point(20, 120)
            };

            rtbAnalysis = new RichTextBox
            {
                Size = new System.Drawing.Size(300, 100),
                Location = new System.Drawing.Point(20, 380),
                ReadOnly = true
            };

            this.Controls.Add(txtKeyInput);
            this.Controls.Add(txtValueInput);
            this.Controls.Add(btnPut);
            this.Controls.Add(txtGetKeyInput);
            this.Controls.Add(btnGet);
            this.Controls.Add(btnRemove);
            this.Controls.Add(lstHashMapItems);
            this.Controls.Add(rtbAnalysis);
        }

        private void BtnPut_Click(object sender, EventArgs e)
        {
            string key = txtKeyInput.Text.Trim();
            string value = txtValueInput.Text.Trim();

            if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
            {
                string result = OperationTimer.MeasureTimeAndMemory(() => hashMap.Put(key, value));
                AppendAnalysis($"Put({key}, {value}): {result}");
                DataUpdated?.Invoke();
                txtKeyInput.Clear();
                txtValueInput.Clear();
            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir anahtar ve değer girin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            string key = txtGetKeyInput.Text.Trim();

            if (!string.IsNullOrEmpty(key))
            {
                string result = OperationTimer.MeasureTimeAndMemory(() => hashMap.Remove(key));
                AppendAnalysis($"Remove({key}): {result}");
                DataUpdated?.Invoke();
                txtGetKeyInput.Clear();
            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir anahtar girin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnGet_Click(object sender, EventArgs e)
        {
            string key = txtGetKeyInput.Text.Trim();

            if (!string.IsNullOrEmpty(key))
            {
                try
                {
                    string result = OperationTimer.MeasureTimeAndMemory(() =>
                    {
                        string value = hashMap.Get(key);
                        MessageBox.Show($"Anahtar: {key} | Değer: {value}", "Bilgi", MessageBoxButtons.OK);
                    });

                    AppendAnalysis($"Get({key}): {result}");
                }
                catch (Exception)
                {
                    MessageBox.Show("Anahtar bulunamadı!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir anahtar girin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void OnHashMapChanged()
        {
            UpdateListBox();
        }

        private void UpdateListBox()
        {
            lstHashMapItems.Items.Clear();
            foreach (var key in hashMap.GetKeys())
            {
                lstHashMapItems.Items.Add($"{key}: {hashMap.Get(key)}");
            }
        }

        private void AppendAnalysis(string message)
        {
            rtbAnalysis.AppendText($"{message}\n");
        }

        public List<string> GetAllData()
        {
            var dataList = new List<string>();

            foreach (var key in hashMap.GetKeys())
            {
                string value = hashMap.Get(key);
                dataList.Add($"ID: {key}, Durum: {value}");
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
