using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MikuReader
{
    public partial class FrmDoublePageReader : Form
    {
        public FrmDoublePageReader()
        {
            InitializeComponent();
        }

        private Chapter curChapter;
        private DirectoryInfo root;
        private FrmStartPage startPage;

        private ArrayList chapters = new ArrayList();
        private int numPagesInCurrentChapter = 0;
        
        /// <summary>
        /// Preperation
        /// </summary>
        /// <param name="m"></param>
        /// <param name="startPage"></param>
        public void StartUp(Manga m, FrmStartPage startPage)
        {
            this.root = m.mangaDirectory;
            this.startPage = startPage;
            
            foreach (DirectoryInfo dir in root.GetDirectories("*", SearchOption.TopDirectoryOnly)) // chapters
            {
                FileInfo[] files = dir.GetFiles("*");
                ArrayList pages = new ArrayList();

                foreach (FileInfo fi in files) // pages
                {
                    string shortName = Path.GetFileNameWithoutExtension(fi.Name);
                    string num = Regex.Match(shortName, @"(\d+(\.\d+)?)|(\.\d+)").Value;
                    pages.Add(new Page(num, fi));
                }

                Chapter c = new Chapter(dir.Name, dir, SortPages((Page[])pages.ToArray(typeof(Page))));
                if (dir.Name == m.currentChapter)
                {
                    curChapter = c;
                } 
                chapters.Add(c);
            }

            UpdateChapters();
            UpdatePages(curChapter);
            cmboPage.SelectedItem = m.currentPage;
        }

        /// <summary>
        /// Sorts the chapters by accending number order
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        private Chapter[] SortChapters(Chapter[] items)
        {
            // Get the numeric values of the items.
            int num_items = items.Length;
            const string float_pattern = @"-?\d+\.?\d*";
            double[] values = new double[num_items];
            for (int i = 0; i < num_items; i++)
            {
                string match = Regex.Match(items[i].num.ToString(), float_pattern).Value;
#pragma warning disable IDE0018 // Inline variable declaration
                double value;
#pragma warning restore IDE0018 // Inline variable declaration
                if (!double.TryParse(match, out value))
                    value = double.MinValue;
                values[i] = value;
            }

            // Sort the items array using the keys to determine order.
            Array.Sort(values, items);
            return items;
        }

        /// <summary>
        /// Sort the pages by accending number order
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        private Page[] SortPages(Page[] items)
        {
            // Get the numeric values of the items.
            int num_items = items.Length;
            const string float_pattern = @"-?\d+\.?\d*";
            double[] values = new double[num_items];
            for (int i = 0; i < num_items; i++)
            {
                string match = Regex.Match(items[i].num.ToString(), float_pattern).Value;
#pragma warning disable IDE0018 // Inline variable declaration
                double value;
#pragma warning restore IDE0018 // Inline variable declaration
                if (!double.TryParse(match, out value))
                    value = double.MinValue;
                values[i] = value;
            }

            // Sort the items array using the keys to determine order.
            Array.Sort(values, items);
            return items;
        }

        /// <summary>
        /// Go to the next page
        /// </summary>
        private void NextPage() // +
        {
            if (cmboPage.SelectedIndex < cmboPage.Items.Count - 2)
            { 
                cmboPage.SelectedIndex = cmboPage.SelectedIndex + 2;
                float currentPage = float.Parse(cmboPage.SelectedItem.ToString());
                float percent = (currentPage / (float)numPagesInCurrentChapter) * 100;
                progress.Value = (int)percent;
            } else if (cmboPage.SelectedIndex < cmboPage.Items.Count - 1)
            {
                cmboPage.SelectedIndex = cmboPage.SelectedIndex + 1;
                float currentPage = float.Parse(cmboPage.SelectedItem.ToString());
                float percent = (currentPage / (float)numPagesInCurrentChapter) * 100;
                progress.Value = (int)percent;
            }
        }

        /// <summary>
        /// Go to the previous page
        /// </summary>
        private void PreviousPage() // -
        {
            if (cmboPage.SelectedIndex > 1)
            {
                cmboPage.SelectedIndex = cmboPage.SelectedIndex - 2;
                float currentPage = float.Parse(cmboPage.SelectedItem.ToString());
                float percent = (currentPage / (float)numPagesInCurrentChapter) * 100;
                progress.Value = (int)percent;
            } else if (cmboPage.SelectedIndex > 0)
            {
                cmboPage.SelectedIndex = cmboPage.SelectedIndex - 1;
                float currentPage = float.Parse(cmboPage.SelectedItem.ToString());
                float percent = (currentPage / (float)numPagesInCurrentChapter) * 100;
                progress.Value = (int)percent;
            }
        }

        /// <summary>
        /// Display the current page in the picturebox
        /// </summary>
        /// <param name="page1"></param>
        /// <param name="page2"></param>
        private void DisplayPage(Page page1, Page page2)
        {
            if (pictureBox1.Image != null) pictureBox1.Image.Dispose();
            if (pictureBox2.Image != null) pictureBox2.Image.Dispose();
            try
            {
                pictureBox1.Image = Image.FromFile(page1.file.FullName);
                if (page2 != null)
                    pictureBox2.Image = Image.FromFile(page2.file.FullName);
            }
            catch (Exception e)
            {
                MessageBox.Show("An error occured while reading the file. The file may be corrupted or null.\n" +
                                "All known info is as follows:\n\n" + e.Message + e.StackTrace);
            }
        }

        /// <summary>
        /// Update the chapter list
        /// </summary>
        private void UpdateChapters()
        {
            cmboChapter.Items.Clear();
            Chapter[] cs = SortChapters((Chapter[])chapters.ToArray(typeof(Chapter)));
            foreach (Chapter c in cs)
            {
                cmboChapter.Items.Add(c.num);
            }
            cmboChapter.SelectedItem = curChapter.num;
        }

        /// <summary>
        /// Update the page lsit for the current chapter
        /// </summary>
        /// <param name="chap"></param>
        private void UpdatePages(Chapter chap)
        {            
            cmboPage.Items.Clear();
            
            foreach (Page p in chap.pages)
            {
                cmboPage.Items.Add(p.num);
            }
            cmboPage.SelectedIndex = 0;
            numPagesInCurrentChapter = cmboPage.Items.Count;
            progress.Value = 0;
        }

        private void PnlLeft_Click(object sender, EventArgs e)
        {
            NextPage();
        }

        private void PnlRight_Click(object sender, EventArgs e)
        {
            PreviousPage();
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
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

        private void PictureBox2_MouseDown(object sender, MouseEventArgs e)
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

        private void CmboPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < curChapter.pages.Length; i++)
            {
                if (curChapter.pages[i].num == cmboPage.SelectedIndex.ToString())
                {
                    if (i < curChapter.pages.Length - 1)
                        DisplayPage(curChapter.pages[i], curChapter.pages[i + 1]);
                    else
                        DisplayPage(curChapter.pages[i], null);
                }
            }
        }

        private void CmboChapter_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Chapter c in chapters)
            {
                if (c.num == cmboChapter.SelectedItem.ToString())
                {
                    curChapter = c;
                    UpdatePages(c);
                    break;
                }
            }
        }

        private void FrmReader_KeyDown(object sender, KeyEventArgs e)
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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Up || keyData == Keys.Down ||
                keyData == Keys.Left || keyData == Keys.Right)
            {
                object sender = Control.FromHandle(msg.HWnd);
                KeyEventArgs e = new KeyEventArgs(keyData);
                FrmReader_KeyDown(sender, e);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Save tracking information when the user closes the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmReader_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (File.Exists(root.FullName + "\\tracker"))
            {
                File.WriteAllText(root.FullName + "\\tracker", String.Empty);
                File.WriteAllText(root.FullName + "\\tracker", cmboChapter.SelectedItem.ToString() + "|" + cmboPage.SelectedItem.ToString());
            }
            startPage.RefreshContents();
        }
    }
}
