using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Acil_Durum_Yonetimi_Simulasyonu.UI.Components
{
    public partial class DataAssignmentPanel : UserControl
    {
        private ComboBox cmbStatusList;
        private ComboBox cmbDataList;
        private Button btnAssignData;
        private RichTextBox rtbAnalysis;

        public event Action<string, string> DataAssigned; 

        public DataAssignmentPanel()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Size = new System.Drawing.Size(350, 200);

            cmbStatusList = new ComboBox
            {
                Size = new System.Drawing.Size(150, 30),
                Location = new System.Drawing.Point(20, 20),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            this.Controls.Add(cmbStatusList);

            cmbDataList = new ComboBox
            {
                Size = new System.Drawing.Size(150, 30),
                Location = new System.Drawing.Point(180, 20),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            this.Controls.Add(cmbDataList);

            btnAssignData = new Button
            {
                Text = "Veriyi Ata",
                Size = new System.Drawing.Size(100, 30),
                Location = new System.Drawing.Point(120, 70)
            };
            btnAssignData.Click += BtnAssignData_Click;
            this.Controls.Add(btnAssignData);

            rtbAnalysis = new RichTextBox
            {
                Size = new System.Drawing.Size(300, 80),
                Location = new System.Drawing.Point(20, 110),
                ReadOnly = true
            };
            this.Controls.Add(rtbAnalysis);
        }

        public void UpdateLists(List<string> statusList, List<string> dataList)
        {
            cmbStatusList.Items.Clear();
            cmbDataList.Items.Clear();

            foreach (var status in statusList)
                cmbStatusList.Items.Add(status);

            foreach (var data in dataList)
                cmbDataList.Items.Add(data);
        }

        private void BtnAssignData_Click(object sender, EventArgs e)
        {
            if (cmbStatusList.SelectedItem == null || cmbDataList.SelectedItem == null)
            {
                MessageBox.Show("Lütfen bir durum ve veri seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedStatus = cmbStatusList.SelectedItem.ToString();
            string selectedData = cmbDataList.SelectedItem.ToString();

            DataAssigned?.Invoke(selectedStatus, selectedData);

            string logMessage = $"Veri Atandı: {selectedData} → {selectedStatus}";
            rtbAnalysis.AppendText($"{logMessage}\n");
        }
    }
}
