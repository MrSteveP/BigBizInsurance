namespace Common.Domain.DTOs
{
    /// <summary>
    ///     The message icon.
    /// </summary>
    public enum MessageIcon
    {
        /// <summary>
        ///     The success.
        /// </summary>
        Success,

        /// <summary>
        ///     The error.
        /// </summary>
        Error,

        /// <summary>
        ///     The security.
        /// </summary>
        Security,

        /// <summary>
        ///     The exclamation.
        /// </summary>
        Exclamation
    }

    /// <summary>
    ///     The full page message.
    /// </summary>
    public class FullPageMessage
    {
        /// <summary>
        ///     Gets or sets the back link text.
        /// </summary>
        public string BackLinkText { get; set; }

        /// <summary>
        ///     Gets or sets the back link url.
        /// </summary>
        public string BackLinkUrl { get; set; }

        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Gets or sets the icon.
        /// </summary>
        public MessageIcon Icon { get; set; }

        /// <summary>
        ///     Gets or sets the message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     Gets or sets the page title.
        /// </summary>
        public string PageTitle { get; set; }
        public string ErrorCode { get; set; }
    }
}
