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

        public FrmSinglePageReader(Manga manga)
        {
            InitializeComponent();
            this.manga = manga;
        }

        private void PopulateChapters()
        {
            foreach (Chapter chapter in SortChapters(manga.GetChapters()))
            {
                cmboChapter.Items.Add(chapter.GetID());
            }

            try
            {
                cmboChapter.SelectedItem = cmboChapter.Items.IndexOf(manga.GetCurrentChapter());
            } catch (Exception)
            {
                MessageBox.Show("An error occured while selecting the current chapter");
                cmboChapter.SelectedIndex = 0;
            }
        }

        private void PopulatePages()
        {
            Chapter c = SortChapters(manga.GetChapters())[cmboChapter.SelectedIndex];

            foreach (Page page in SortPages(c.GetPages()))
            {
                cmboPage.Items.Add(page.GetID());
            }

            try
            {
                cmboPage.SelectedItem = cmboPage.Items.IndexOf(manga.GetCurrentPage());
            } catch (Exception)
            {
                MessageBox.Show("An error occured while selecting the current page");
                cmboPage.SelectedIndex = 0;
            }
        }

        private void NextPage()
        {

        }

        private void PreviousPage()
        {

        }

        private void LoadImage()
        {

        }

        #region Helper functions
        /// <summary>
        /// Sort chapters in accending order
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        private Chapter[] SortChapters(ArrayList aList)
        {
            Chapter[] items = (Chapter[])aList.ToArray();
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
        private Page[] SortPages(ArrayList aList)
        {
            Page[] items = (Page[])aList.ToArray();

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
