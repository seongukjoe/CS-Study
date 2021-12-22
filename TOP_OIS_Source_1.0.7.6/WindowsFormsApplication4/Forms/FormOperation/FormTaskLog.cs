using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XModule.Standard;


namespace XModule.Forms.FormOperation
{
    public partial class FormTaskLog : Form
    {
        
        public FormTaskLog()
        {
            InitializeComponent();
            ListListBox = new List<ListBox>();
            ListButton = new List<Button>();

            for (int i = 0; i < (int)TaskLog.LOG_MAX; i++)
                TaskLogAdd(i);

        }

        public void TaskLogAdd(int Index)
        {
            ListBox lb = new ListBox();
            lb.FormattingEnabled = true;
            lb.TabIndex = Index;
            lb.Name = $"lb{Enum.GetName(typeof(TaskLog), Index)}";
            lb.Dock = DockStyle.Top;
            this.Controls.Add(lb);

            //this.listBox1.FormattingEnabled = true;
            //this.listBox1.ItemHeight = 12;
            //this.listBox1.Location = new System.Drawing.Point(102, 547);
            //this.listBox1.Name = "listBox1";
            //this.listBox1.Size = new System.Drawing.Size(105, 88);
            //this.listBox1.TabIndex = 134;

            //pbLog.Controls.Add(lb);
            ListListBox.Add(lb);

            Button btn = new Button();
            btn.TabIndex = Index;
            btn.Name = $"btn{Enum.GetName(typeof(TaskLog), Index)}";
            btn.Text = Enum.GetName(typeof(TaskLog), Index).ToUpper();
            btn.FlatStyle = FlatStyle.Flat;
            btn.TextAlign = ContentAlignment.MiddleCenter;
            btn.AutoSize = true;
            btn.Click += btnClick;

            flowLayoutPanel1.Controls.Add(btn);
            ListButton.Add(btn);
            

        }

        private List<ListBox> ListListBox;
        private List<Button> ListButton;
        private void FormTaskLog_Load(object sender, EventArgs e)
        {
            cDEF.TaskLog.OnWriteLog += TaskLog_OnWriteLog;
            ShowLog((TaskLog)0);
        }

        private void TaskLog_OnWriteLog(object sender, int index, string log)
        {
            if (ListListBox[index].InvokeRequired)
            {
                ListListBox[index].Invoke((System.Windows.Forms.MethodInvoker)delegate
                {
                    ListListBox[index].BeginUpdate();
                    ListListBox[index].Items.Insert(0, log);
                    ListListBox[index].EndUpdate();
                });
            }
        }

        TaskLog curTaskLog = (TaskLog)0;

        void ShowLog(TaskLog taskLog)
        {
            foreach(Button btnn in ListButton)
            {
                btnn.BackColor = Color.DarkGray;
                btnn.ForeColor = Color.Gray;
            }

            Button btn = ListButton[(int)taskLog];    //GetButton(taskLog);
            btn.BackColor = Color.RoyalBlue;
            btn.ForeColor = Color.Yellow;

            foreach (ListBox lb in ListListBox)
            {
                lb.Hide();
            }

            ListBox listBox = ListListBox[(int)taskLog];  //GetListBox(taskLog);
            listBox.Left = pbLog.Left;
            listBox.Top = pbLog.Top;
            listBox.Width = pbLog.Width;
            listBox.Height = pbLog.Height;
            listBox.Show();

            curTaskLog = taskLog;
        }

        private void btnClick(object sender, EventArgs e)
        {
            int tag = Convert.ToInt32((sender as Button).TabIndex);
            ShowLog((TaskLog)tag);
        }
        
        void ClearLog(TaskLog taskLog)
        {
            ListListBox[(int)taskLog].Items.Clear();
        }
        private void btnClean_Click(object sender, EventArgs e)
        {
            ClearLog(curTaskLog);
        }

        private void btnAllClean_Click(object sender, EventArgs e)
        {
            foreach (ListBox lb in ListListBox)
            {
                lb.Items.Clear();
            }
        }

        private void BtnEXIT_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
