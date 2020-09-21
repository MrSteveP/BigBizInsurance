using System;
using System.Collections.Generic;
using System.Text;

namespace CrossCutting.Web
{
    public class LinkReference
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LinkReference"/> class.
        /// </summary>
        /// <param name="cssPath">
        /// The css path.
        /// </param>
        /// <param name="includeOrderPriorty">
        /// The include order priorty.
        /// </param>
        public LinkReference(string cssPath, int includeOrderPriorty = 0)
        {
            this.CssPath = cssPath;
            this.IncludeOrderPriorty = includeOrderPriorty;
        }

        /// <summary>
        /// Gets the css path.
        /// </summary>
        public string CssPath { get; }

        /// <summary>
        /// Gets the include order priorty.
        /// </summary>
        public int IncludeOrderPriorty { get; }
    }
}
