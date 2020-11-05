using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViewModels
{
    public class LoginViewModel:Object
    {
        public LoginViewModel():base()
        {

        }

        [System.ComponentModel.DataAnnotations.Required
            (AllowEmptyStrings = false)]

        [System.ComponentModel.DataAnnotations.StringLength
            (maximumLength: 20, MinimumLength = 6)]

        public string Username { get; set; }
        //*****************************************************

        //[System.ComponentModel.DataAnnotations.Key]

        [System.ComponentModel.DataAnnotations.Required
            (AllowEmptyStrings = false)]

        [System.ComponentModel.DataAnnotations.StringLength
            (maximumLength: 40, MinimumLength = 8)]

        [System.ComponentModel.DataAnnotations.DataType
            (System.ComponentModel.DataAnnotations.DataType.Password)]
        public string Password { get; set; }
        //*****************************************************

        public bool RememberMe { get; set; }
        //*****************************************************

        public string ReturnUrl { get; set; }
    }
}
