using CrossCutting.Core;

namespace UserManagement.Domain.ViewModels
{
    public class PolicyFilterViewModel : FilterVModel<PolicyViewModel>
    {
        public string Name { set; get; }
    }
}
