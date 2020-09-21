using Common.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Application.Services.Abstraction
{
  public  interface IAttachmentTypeSettings
    {
        AttachmentTypeDto AvatarImageSettings { set; get; }
        AttachmentTypeDto VideoSettings { set; get; }
        AttachmentTypeDto TextFileSettings { set; get; }

    }
}
