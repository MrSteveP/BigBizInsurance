using CrossCutting.Core;

namespace UserManagement.Domain.ViewModels
{
    public  class UserFilterViewModel: FilterVModel<UserViewModel>
    {
        public string UserName { set; get; }
        public string Email { set; get; }
        public string PhoneNumber { set; get; }
        public string Name { set; get; }
    }
}
