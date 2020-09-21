using System;
using System.Collections.Generic;

namespace CrossCutting.Helpers
{
    public abstract class FilterVModel<T>
    {
        public int jtStartIndex { get; set; } = 0;
        public int PageNumber
        {
            get
            {
                if (jtStartIndex == 0)
                    return 0;

                return (int)Math.Ceiling(jtStartIndex / (double)jtPageSize);
            }
        }
        public int? jtPageSize { get; set; } = 20;
        public int TotalCount { set; get; }

        public string jtSorting { set; get; } = string.Empty;

        /// <summary>
        ///     Gets or sets the items.
        /// </summary>
        public List<T> Items { get; set; }
    }

}
