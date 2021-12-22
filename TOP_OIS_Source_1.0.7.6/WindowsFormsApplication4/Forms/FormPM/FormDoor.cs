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

namespace XModule.Forms.FormWorking
{
    public partial class FormDoor : Form
    {
        public FormDoor()
        {
            InitializeComponent();
        }
        private void FormDoor_Load(object sender, EventArgs e)
        {
        }

        private void FormDoor_Shown(object sender, EventArgs e)
        {
            FBuzzerStatus = 0;
            timer.Enabled = true;
            FTickCount = Environment.TickCount;
        }

        private void FormDoor_FormClosed(object sender, FormClosedEventArgs e)
        {
            FTickCount = Environment.TickCount;
            timer.Enabled = false;
            FBuzzerStatus = 0;

   
        }

        private int FBuzzerStatus = 0;
        private int FTimeInterval1 = 0;
        private int FTimeInterval2 = 0;
        private int FTickCount = Environment.TickCount;
        private void timer_Tick(object sender, EventArgs e)
        {
            
        }

        private void glassButton_DoorLock_Click(object sender, EventArgs e)
        {
         
    
        }

        private void glassButton_DoorOpen_Click(object sender, EventArgs e)
        {

           
        }


      
        private void glassButton_Close_Click(object sender, EventArgs e)
        {
            FTickCount = Environment.TickCount;
            timer.Enabled = false;
            FBuzzerStatus = 0;

            //cDEF.Run.Digital.Output[cDO.Melody_BUZZER_Bit_1] = false;
            //cDEF.Run.Digital.Output[cDO.Melody_BUZZER_Bit_2] = false;
            //cDEF.Run.Digital.Output[cDO.Melody_BUZZER_Bit_3] = false;
            //cDEF.Run.Digital.Output[cDO.Melody_BUZZER_Bit_4] = false;
            //cDEF.Run.Digital.Output[cDO.Melody_BUZZER_Bit_5] = false;

            Close();
        }

        
       

    }
}
