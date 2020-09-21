namespace Common.Domain.Module
{
    public static  class CommonEnumerations
    {
        public enum AttachmentTypes
        {
            AvatarImage=1,
            Video,
            TextFile
        }
        public enum ErrorTypesEnum
        {
            PageNotFound,
            NotAuthorized,
            UnexpectedError
        }
    }
}
