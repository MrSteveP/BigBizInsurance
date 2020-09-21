using Common.Application.Persistance;
using Common.Application.Services.Abstraction;
using Common.Domain.DTOs;
using Common.Domain.Module;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Application.Services.Concrete
{
    public sealed class AttachmentTypeSettings : IAttachmentTypeSettings
    {
        public AttachmentTypeSettings(ICommonUnitOfWork commonsUnitOfWork)
        {
            this.UnitOfWork = commonsUnitOfWork;
            this.LoadSettings();
        }

        private ICommonUnitOfWork UnitOfWork { get; }
        public AttachmentTypeDto AvatarImageSettings { get ; set  ; }
        public AttachmentTypeDto VideoSettings { get  ; set  ; }
        public AttachmentTypeDto TextFileSettings { get  ; set  ; }

        private void LoadSettings()
        {
            AvatarImageSettings = this.UnitOfWork.AttachmentTypeRepository.GetAttachmentTypeFromCache((int)CommonEnumerations.AttachmentTypes.AvatarImage);
            VideoSettings = this.UnitOfWork.AttachmentTypeRepository.GetAttachmentTypeFromCache((int)CommonEnumerations.AttachmentTypes.Video);
            TextFileSettings = this.UnitOfWork.AttachmentTypeRepository.GetAttachmentTypeFromCache((int)CommonEnumerations.AttachmentTypes.TextFile);
        }
    }
}
