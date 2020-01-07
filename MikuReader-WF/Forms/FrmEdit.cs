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
    public partial class FrmEdit : Form
    {
        private Title title;

        public FrmEdit(Title title)
        {
            InitializeComponent();
            this.title = title;
        }

        private void FrmEdit_Load(object sender, EventArgs e)
        {
            txtTitle.Text = title.GetUserTitle();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            bool changed = false;

            if (txtTitle.Text != title.GetUserTitle())
            {
                changed = true;
            }

            if (changed)
            {
                title.UpdateProperties(txtTitle.Text);
                MessageBox.Show("Changes saved! Refresh to view.");
            }
            
            this.Close();
        }


        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
