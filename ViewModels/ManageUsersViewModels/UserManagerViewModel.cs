using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class UserManagerViewModel:System.Object
    {
        public UserManagerViewModel():base()
        {
            UserRoles = new System.Collections.Generic.List<string>();
        }

        public System.Collections.Generic.List<string> UserRoles { get; set; }
        public string UserId { get; set; }
        public string RoleName { get; set; }
        public System.Guid RoleId { get; set; }
    }
}
