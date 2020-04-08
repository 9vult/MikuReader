using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MikuReader.Core
{
    public class ReaderHelper
    {
        /// <summary>
        /// Sort chapters in accending order
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static Chapter[] SortChapters(List<Chapter> aList)
        {
            Chapter[] items = aList.ToArray();
            // Get the numeric values of the items.
            int num_items = items.Length;
            const string float_pattern = @"-?\d+\.?\d*";
            double[] values = new double[num_items];
            for (int i = 0; i < num_items; i++)
            {
                string match = Regex.Match(items[i].GetNum(), float_pattern).Value;
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
        public static Page[] SortPages(List<Page> aList)
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
    }
}
