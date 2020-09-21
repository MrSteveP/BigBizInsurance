using System;
using System.Collections.Generic;
using System.Text;

namespace CrossCutting.Web
{
    /// <summary>
    ///     Refers to an included script using the script tag src attribute.
    ///     IncludeOrderPriority is so that higher order scripts are included later.
    ///     That is, if you want jQueryUI to be always after jQuery, you need to make
    ///     sure that the includeOrderPriority of jQueryUI is higher than jQuery.
    ///     The sort is low to high and files are rendered in that order.
    /// </summary>
    public class ScriptReference
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptReference"/> class.
        /// </summary>
        /// <param name="scriptPath">
        /// The script path.
        /// </param>
        /// <param name="includeOrderPriorty">
        /// The include order priorty.
        /// </param>
        public ScriptReference(string scriptPath, int includeOrderPriorty = 0)
        {
            this.ScriptPath = scriptPath;
            this.IncludeOrderPriorty = includeOrderPriorty;
        }

        /// <summary>
        /// Gets the include order priorty.
        /// </summary>
        public int IncludeOrderPriorty { get; }

        /// <summary>
        /// Gets the script path.
        /// </summary>
        public string ScriptPath { get; }
    }
}
