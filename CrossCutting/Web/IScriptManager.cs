using System;
using System.Collections.Generic;
using System.Text;

namespace CrossCutting.Web
{
    #region usings

    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// The ScriptManager interface.
    /// </summary>
    public interface IScriptManager
    {
        /// <summary>
        /// Gets the scripts.
        /// </summary>
        List<ScriptReference> Scripts { get; }

        /// <summary>
        /// Gets the script texts.
        /// </summary>
        List<string> ScriptTexts { get; }

        /// <summary>
        /// The add script.
        /// </summary>
        /// <param name="script">
        /// The script.
        /// </param>
        void AddScript(ScriptReference script);

        /// <summary>
        /// The add script text.
        /// </summary>
        /// <param name="scriptTextExecute">
        /// The script text execute.
        /// </param>
        void AddScriptText(string scriptTextExecute);
    }
}
