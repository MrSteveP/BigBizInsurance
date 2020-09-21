using System;
using System.Collections.Generic;
using System.Text;

namespace CrossCutting.Web
{
    #region usings

    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    ///     ScriptManager keeps track of all the scripts (referenced javascript files) and scriptTexts (blocks of actual
    ///     javascript)
    ///     that have been added to the project.  ScriptManager makes sure there are no duplicates add so when it is time to
    ///     output the
    ///     javascript files, they are already deduped.
    /// </summary>
    public class CssManager : ICssManager
    {
        // getter only prop retrieves scripts
        // this is the filenames (or URL's) of the script tags

        /// <summary>
        /// Gets the css list.
        /// </summary>
        public List<LinkReference> CssList { get; } = new List<LinkReference>();

        /// <summary>
        /// The add css.
        /// </summary>
        /// <param name="css">
        /// The css.
        /// </param>
        public void AddCss(LinkReference css)
        {
            if (this.CssList.All(x => x.CssPath != css.CssPath))
                this.CssList.Add(css);
        }
    }
}
