namespace CrossCutting.Core
{
    public static  class CoreEnumerations
    {
        public class TempDataKeys
        {
            /// <summary>
            /// The message
            /// </summary>
            public const string Message = "Message";

        }
        public enum MessageTypes
        {
            success,
            info,
            warning,
            danger
        }
    }
}
