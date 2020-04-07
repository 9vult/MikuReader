using MikuReader.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MikuReader.wf.Forms
{
    public partial class FrmChapterSelect : Form
    {
        public string[] ReturnValue { get; set; }

        public FrmChapterSelect(Chapter[] chapters, String[] currentDlcs)
        {
            InitializeComponent();

            foreach (Chapter c in chapters)
            {
                lstChapters.Items.Add(c.GetNum());
            }

            if (currentDlcs == null)    // Select all for download
            {
                for (int i = 0; i < lstChapters.Items.Count; i++)
                {
                    lstChapters.SetItemChecked(i, true);
                }
            }
            else    // Select only speciified chapters for download
            {
                foreach (String num in currentDlcs)
                {
                    if (!num.Equals("-1"))
                    {
                        int index = lstChapters.Items.IndexOf(num);
                        lstChapters.SetItemChecked(index, true);
                    }
                }
            } 
        }

        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        private void FrmChapterSelect_Load(object sender, EventArgs e)
        {

        }

        private void BtnAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstChapters.Items.Count; i++)
            {
                lstChapters.SetItemChecked(i, true);
            }
        }

        private void BtnNone_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstChapters.Items.Count; i++)
            {
                lstChapters.SetItemChecked(i, false);
            }
        }

        private void BtnDone_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Altering chapter downloads will disable the Update Chapters feature for this title.\n" +
                                                    "If you wish to revert this decision in the future, use the Default button.\n\n" +
                                                    "Proceed?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                List<string> selected = new List<string>();
                foreach (object item in lstChapters.CheckedItems)
                {
                    selected.Add(item.ToString());
                }

                ReturnValue = selected.ToArray();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }            
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void BtnResetToDefault_Click(object sender, EventArgs e)
        {
            ReturnValue = new string[] { "-1" };
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
