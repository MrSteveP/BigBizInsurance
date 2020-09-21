namespace Common.Application.Services.Abstraction
{
    public interface IAttachmentSystemSettings
    {
        string AttachmentPath { set; get; }
        bool SaveFilesToDatabase { set; get; }
    }
}
