using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class PasswordChangeViewModel
    {
        public PasswordChangeViewModel()
        {

        }

        [System.ComponentModel.DataAnnotations.Required
          (AllowEmptyStrings = false)]

        [System.ComponentModel.DataAnnotations.StringLength
          (maximumLength: 20, MinimumLength = 6)]

        public string Username { get; set; }
        //*****************************************************
        [System.ComponentModel.DataAnnotations.Required
           (AllowEmptyStrings = false)]

        [System.ComponentModel.DataAnnotations.StringLength
           (maximumLength: 40, MinimumLength = 8)]

        [System.ComponentModel.DataAnnotations.DataType
           (System.ComponentModel.DataAnnotations.DataType.Password)]
        public string OldPassword { get; set; }
        //*****************************************************
        [System.ComponentModel.DataAnnotations.Required
           (AllowEmptyStrings = false)]

        [System.ComponentModel.DataAnnotations.StringLength
           (maximumLength: 40, MinimumLength = 8)]

        [System.ComponentModel.DataAnnotations.DataType
           (System.ComponentModel.DataAnnotations.DataType.Password)]
        public string NewPassword { get; set; }
        //*****************************************************

        [System.ComponentModel.DataAnnotations.Required
           (AllowEmptyStrings = false)]

        [System.ComponentModel.DataAnnotations.StringLength
           (maximumLength: 40, MinimumLength = 8)]

        [System.ComponentModel.DataAnnotations.DataType
           (System.ComponentModel.DataAnnotations.DataType.Password)]

        [System.ComponentModel.DataAnnotations.Compare
                            (otherProperty: nameof(NewPassword))]
        public string ConfirmNewPassword { get; set; }
        //*****************************************************
    }
}
