using CrossCutting.Core;

namespace UserManagement.Domain.ViewModels
{
    public class RoleFilterViewModel : FilterVModel<RoleViewModel>
    {
        public string Name { set; get; }
    }
}
