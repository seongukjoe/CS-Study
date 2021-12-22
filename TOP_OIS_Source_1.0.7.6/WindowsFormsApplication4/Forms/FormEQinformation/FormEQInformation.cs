using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using XModule.Standard;


namespace XModule.Forms.FormOperation
{
    public partial class FormInformation : TFrame
    {
        public FormInformation()
        {
            InitializeComponent();
            Button[] btn_disp = new Button[25];
        }

        private void FormInforation_Load(object sender, EventArgs e)
        {
          
            Open();
            
        }

        public bool Open()
        {
           
            Button[] btn_disp = { b0, b1, b2, b3, b4, b5, b6, b7, b8, b9, b10, b11, b12, b13, b14, b15, b16, b17, b18, b19, b20, b21, b22, b23, b24, b25, b26, b27 };

            string path = cPath.FILE_EQ_INFORMATION + "EQ_Information.txt";
            string str;
            string[] temp;
            int i = 0;
            if (File.Exists(path))
            {
                StreamReader sr = new StreamReader(path);
                try
                {
                    while (sr.Peek() >= 0)
                    {

                        str = sr.ReadLine();
                        if (i > 13)
                        { 
                            temp = str.Split('=');
                            btn_disp[i].Text = temp[1];
                        }
                        i++;
                    }
                    sr.Close();
                }
                catch
                {
                    sr.Close();
                    //MessageBox.Show(path + "  Error");
                }
            }
            else
                MessageBox.Show(path + " not find file");

            return true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
          
            
        }
    }
}
