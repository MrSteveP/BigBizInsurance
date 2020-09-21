using System;
using System.Collections.Generic;
using System.Text;

namespace CrossCutting.Web
{
    #region usings

    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// The CssManager interface.
    /// </summary>
    public interface ICssManager
    {
        /// <summary>
        /// Gets the css list.
        /// </summary>
        List<LinkReference> CssList { get; }

        /// <summary>
        /// The add css.
        /// </summary>
        /// <param name="css">
        /// The css.
        /// </param>
        void AddCss(LinkReference css);
    }
}
