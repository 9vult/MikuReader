using MikuReader.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MikuReader.wf.Forms
{
    public partial class FrmSinglePageReader : Form
    {
        private Manga manga;
        private Chapter currentChapter;
        private Page currentPage;

        public FrmSinglePageReader(Manga manga)
        {
            InitializeComponent();
            this.manga = manga;
            this.currentChapter = null;
            this.currentPage = null;
            try
            {
                PopulateChapters();
                cmboPage.SelectedIndex = cmboPage.Items.IndexOf(manga.GetCurrentPage());
                LoadImage();
            } catch (Exception ex)
            {
                MessageBox.Show("An error occured while preparing the Reader:\n" + ex.Message);
            }
        }

        private void PopulateChapters()
        {
            cmboChapter.Items.Clear();
            foreach (Chapter chapter in SortChapters(manga.GetChapters()))
            {
                cmboChapter.Items.Add(chapter.GetID());
                if (chapter.GetID().Equals(manga.GetCurrentChapter()))
                    currentChapter = chapter;
            }

            try
            {
                cmboChapter.SelectedIndex = cmboChapter.Items.IndexOf(currentChapter.GetID());
            } catch (Exception)
            {
                MessageBox.Show("An error occured while selecting the current chapter");
                cmboChapter.SelectedIndex = 0;
            }
        }

        private void PopulatePages()
        {
            cmboPage.Items.Clear();
            Page[] pages = SortPages(currentChapter.GetPages());
            foreach (Page page in pages)
            {
                cmboPage.Items.Add(page.GetID());
            }

            cmboPage.SelectedIndex = 0;
        }

        private void NextPage()
        {
            if (cmboPage.SelectedIndex < cmboPage.Items.Count - 1)
                cmboPage.SelectedIndex += 1;
        }

        private void PreviousPage()
        {
            if (cmboPage.SelectedIndex > 0)
                cmboPage.SelectedIndex -= 1;
        }

        private void LoadImage()
        {
            if (pbPageDisplay.Image != null)
                pbPageDisplay.Image.Dispose();

            try
            {
                Page currentPage = SortPages(currentChapter.GetPages())[cmboPage.SelectedIndex];
                pbPageDisplay.Image = new Bitmap(currentPage.GetPath());
            } catch (Exception e)
            {
                MessageBox.Show("An error occured while loading the image file. The file may be corrupted and/or null.\n" +
                                "Message: " + e.Message);
            }
            
        }

        #region Events

        private void PbPageDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                NextPage();
            }
            if (e.Button == MouseButtons.Right)
            {
                PreviousPage();
            }
        }

        private void CmboChapter_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentChapter = SortChapters(manga.GetChapters())[cmboChapter.SelectedIndex];
            PopulatePages();
        }

        private void CmboPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadImage();
            progressBar.Value = (int)((cmboPage.SelectedIndex / (float)cmboPage.Items.Count) * 100f);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Up || keyData == Keys.Down ||
                keyData == Keys.Left || keyData == Keys.Right)
            {
                object sender = Control.FromHandle(msg.HWnd);
                KeyEventArgs e = new KeyEventArgs(keyData);
                FrmSinglePageReader_KeyDown(sender, e);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void FrmSinglePageReader_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                case Keys.Left:
                case Keys.S:
                case Keys.Down:
                    NextPage();
                    break;
                case Keys.D:
                case Keys.Right:
                case Keys.W:
                case Keys.Up:
                    PreviousPage();
                    break;
            }
        }

        private void FrmSinglePageReader_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                manga.Save(cmboChapter.SelectedItem.ToString(), cmboPage.SelectedItem.ToString());
            } catch (Exception ex)
            {
                MessageBox.Show("Failed to initialize tracking save procedure:\n" + ex.Message);
            }
            // TODO: Notify Launcher to refresh page numbers
        }

        #endregion

        #region Helper functions
        /// <summary>
        /// Sort chapters in accending order
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        private Chapter[] SortChapters(List<Chapter> aList)
        {
            Chapter[] items = aList.ToArray();
            // Get the numeric values of the items.
            int num_items = items.Length;
            const string float_pattern = @"-?\d+\.?\d*";
            double[] values = new double[num_items];
            for (int i = 0; i < num_items; i++)
            {
                string match = Regex.Match(items[i].GetID(), float_pattern).Value;
                if (!double.TryParse(match, out double value))
                    value = double.MinValue;
                values[i] = value;
            }

            // Sort the items array using the keys to determine order.
            Array.Sort(values, items);
            return items;
        }

        /// <summary>
        /// Sort pages in accending order
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        private Page[] SortPages(List<Page> aList)
        {
            Page[] items = aList.ToArray();

            // Get the numeric values of the items.
            int num_items = items.Length;
            const string float_pattern = @"-?\d+\.?\d*";
            double[] values = new double[num_items];
            for (int i = 0; i < num_items; i++)
            {
                string match = Regex.Match(items[i].GetID(), float_pattern).Value;
                if (!double.TryParse(match, out double value))
                    value = double.MinValue;
                values[i] = value;
            }

            // Sort the items array using the keys to determine order.
            Array.Sort(values, items);
            return items;
        }
        #endregion
    }
}
