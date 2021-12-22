using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XModule.Standard
{
    public partial class frmTextEdit : Form
    {
        private String FOld;
        private String FEditing;
        private String FIgnore;
        private int FMaxLength;
        private bool FUpperCase;
        private bool FCapsLock;
        private bool FShift;

 
        public frmTextEdit()
        {
            InitializeComponent();
        }

        public String Old
        {
            get { return FOld; }
            set { FOld = value; }
        }

        public String Editing
        {
            get { return FEditing; }
            set { FEditing = value; }
        }
        public String Ignore
        {
            get { return FIgnore; }
            set { FIgnore = value; }
        }
        public int MaxLength
        {
            get { return FMaxLength; }
            set { FMaxLength = value; }
        }
        public bool UpperCase
        {
            get { return FUpperCase; }
            set { FUpperCase = value; }
        }

        private void frmTextEdit_Load(object sender, EventArgs e)
        {
            KeyPreview = true;
        }

        private void frmTextEdit_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == '\x1B') //ESC
            {
                e = null;

                if (FEditing == String.Empty)
                    DialogResult = DialogResult.No;
                else
                {
                    FEditing = "";
                    lbNew.Text = FEditing + "_";
                }
            }
            else if (e.KeyChar == '\b') //back
            {
                e = null;
                if (FEditing != String.Empty)
                {
                    FEditing = FEditing.Substring(0, FEditing.Length - 1);
                    lbNew.Text = FEditing + "_";
                }
            }
            else if (e.KeyChar == '\r') //enter
            {
                e = null;
                DialogResult = DialogResult.OK;
            }
            else
            {
                if (('0' <= e.KeyChar && e.KeyChar <= '9')
                    || ('a' <= e.KeyChar && e.KeyChar <= 'z')
                    || ('A' <= e.KeyChar && e.KeyChar <= 'Z')
                    || e.KeyChar == '`'
                    || e.KeyChar == '~'
                    || e.KeyChar == '!'
                    || e.KeyChar == '@'
                    || e.KeyChar == '#'
                    || e.KeyChar == '$'
                    || e.KeyChar == '%'
                    || e.KeyChar == '^'
                    || e.KeyChar == '&'
                    || e.KeyChar == '*'
                    || e.KeyChar == '('
                    || e.KeyChar == ')'
                    || e.KeyChar == '-'
                    || e.KeyChar == '='
                    || e.KeyChar == '\\'
                    || e.KeyChar == '_'
                    || e.KeyChar == '+'
                    || e.KeyChar == '|'
                    || e.KeyChar == '<'
                    || e.KeyChar == '>'
                    || e.KeyChar == '?'
                    || e.KeyChar == ','
                    || e.KeyChar == '.'
                    || e.KeyChar == '/'
                    || e.KeyChar == ':'
                    || e.KeyChar == '"'
                    || e.KeyChar == ';'
                    || e.KeyChar == '\''
                    || e.KeyChar == ' ')
                {
                    FEditing = FEditing + e.KeyChar;
                    lbNew.Text = FEditing + "_";

                    e = null;
                }
            }
        }

        public void UpdateInformation(String Text, String EditingText, int MaxLength, String Ignore, bool UpperCase)
        {
	        FShift = false;
	        FCapsLock = false;

            lbTitle.Text = Text;
            lbOld.Text = EditingText;
            lbNew.Text = EditingText;
	        FOld = EditingText;
	        FEditing = EditingText;
	        FMaxLength = MaxLength;
	        FIgnore = Ignore;
	        FUpperCase = UpperCase;
        }

        private void btnEscape_Click(object sender, EventArgs e)
        {
            if (FEditing == String.Empty)
                DialogResult = DialogResult.No;
            else
            {
                FEditing = "";
                lbNew.Text = FEditing + " ";
            }
        }

        private void btnCapsLock_Click(object sender, EventArgs e)
        {
            FCapsLock = !FCapsLock;

            (sender as Glass.GlassButton).ForeColor = (FCapsLock) ? Color.Maroon : Color.Black;
        }
        private void ShiftClick(object sender, EventArgs e)
        {
	        FShift = !FShift;

            btnShiftLeft.ForeColor = (FShift) ? Color.Maroon : Color.Black;
            btnShiftRight.ForeColor = (FShift) ? Color.Maroon : Color.Black;
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        public void AlphabetClick(object sender, EventArgs e)
        {
	        bool Upper = FShift;
            int FData = 0;
	        if(FCapsLock)
		        Upper = !Upper;

            FData = Convert.ToInt32((sender as Glass.GlassButton).Tag);

            if (Upper)
                FEditing = FEditing + Convert.ToChar(65 + FData);
            else
                FEditing = FEditing + Convert.ToChar(97 + FData); //((97 + (int)(sender as Glass.GlassButton).Tag));
	        lbNew.Text = FEditing + " ";
        }

        private void btnBackspace_Click(object sender, EventArgs e)
        {
            if (FEditing != String.Empty)
            {
                FEditing = FEditing.Substring(0, FEditing.Length - 1);
                lbNew.Text = FEditing + " ";
            }
        }

        public void CharacterClick(object sender, EventArgs e)
        {
            char[] Normal = "`1234567890-=[]\\;',./".ToArray();

	        char[] Upper = "~!@#$%^&*()_+{}|:\"<>?".ToArray();

            int FData = 0;

            FData = Convert.ToInt32((sender as Glass.GlassButton).Tag);

	        if(FShift)
                FEditing = FEditing + Upper[FData];
	        else
		        FEditing = FEditing + Normal[FData];
	        lbNew.Text = FEditing + " ";
        }

        public void btnSpaceClick(object sender, EventArgs e)
        {
	        FEditing = FEditing + " ";
	        lbNew.Text = FEditing + " ";
        }

        public bool TextEdit(String Text, ref String Value, String Ignore = "", bool UpperCase = false)
        {
            UpdateInformation(Text, Value, 0, Ignore, UpperCase);

            if (ShowDialog() == DialogResult.OK)
                if (Value != FEditing)
                {
                    Value = FEditing;
                    return true;
                }
            return false;
        }
    }
}
