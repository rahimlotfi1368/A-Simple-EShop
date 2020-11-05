using Microsoft.AspNetCore.Identity;

namespace Models
{
    public class User: Models.BaseEntity// Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public User(Microsoft.AspNetCore.Identity.RoleManager<Microsoft.AspNetCore.Identity.IdentityRole> roleManager):base()
        {
            this.roleManager = roleManager;
        }
        public User()
        {

        }
        //***********************************************************************
        #region ModelRelationShips
        public System.Guid RoleId { get; set; }
        public IdentityRole Role { get; set; }

        public System.Collections.Generic.IList<Order> Orders { get; set; }
        #endregion
        //***********************************************************************


        //***********************************************************************
        #region Properties


        // **********
        [System.ComponentModel.DataAnnotations.Required
            (AllowEmptyStrings = false)]

        [System.ComponentModel.DataAnnotations.StringLength
            (maximumLength: 20, MinimumLength = 6)]
               
        public string Username { get; set; }
        // **********

        // **********
        [System.ComponentModel.DataAnnotations.Required
            (AllowEmptyStrings = false)]

        [System.ComponentModel.DataAnnotations.StringLength
            (maximumLength: 40, MinimumLength = 8)]

        [System.ComponentModel.DataAnnotations.DataType
            (System.ComponentModel.DataAnnotations.DataType.Password)]
        public string Password { get; set; }
        // **********

        [System.ComponentModel.DataAnnotations.Display
           (ResourceType = typeof(Resources.DataDictionary),
           Name = nameof(Resources.DataDictionary.ImageName))]
        public string ImageName { get; set; }
       
        // **********
        [System.ComponentModel.DataAnnotations.Display
            (Name = nameof(Resources.DataDictionary.Email))]

        [System.ComponentModel.DataAnnotations.Required
            (AllowEmptyStrings = false)]

        [System.ComponentModel.DataAnnotations.StringLength
            (maximumLength: 50, MinimumLength = 3)]

        [System.ComponentModel.DataAnnotations.DataType(System.ComponentModel.DataAnnotations.DataType.EmailAddress)]
        [System.ComponentModel.DataAnnotations.RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
            ErrorMessageResourceType =typeof(Resources.Messages),
            ErrorMessageResourceName  =nameof(Resources.Messages.EmailAddressError))]
        public string EMail { get; set; }
        // **********
        [System.ComponentModel.DataAnnotations.Required(
            ErrorMessageResourceType =typeof(Resources.Messages),
            ErrorMessageResourceName =nameof(Resources.Messages.AddressError))]

        [System.ComponentModel.DataAnnotations.StringLength
            (maximumLength: 100, MinimumLength = 3)]

        [System.ComponentModel.DataAnnotations.Display
            (Name = nameof(Resources.DataDictionary.Address))]
        public string Address { get; set; }
        // **********

        [System.ComponentModel.DataAnnotations.Required(
            ErrorMessageResourceType =typeof(Resources.Messages),
            ErrorMessageResourceName =nameof(Resources.Messages.ZipCodeError))]

        [System.ComponentModel.DataAnnotations.StringLength
            (maximumLength: 100, MinimumLength = 3)]

        [System.ComponentModel.DataAnnotations.Display
            (Name = nameof(Resources.DataDictionary.ZipCode))]
        public string ZipCode { get; set; }
        // **********
        [System.ComponentModel.DataAnnotations.Required(
            ErrorMessageResourceType =typeof(Resources.Messages),
            ErrorMessageResourceName =nameof(Resources.Messages.CityError))]

        [System.ComponentModel.DataAnnotations.StringLength
            (maximumLength: 100, MinimumLength = 3)]

        [System.ComponentModel.DataAnnotations.Display
            (Name = nameof(Resources.DataDictionary.City))]
        public string City { get; set; }
        // **********

        [System.ComponentModel.DataAnnotations.Required(
            ErrorMessageResourceType =typeof(Resources.Messages),
            ErrorMessageResourceName =nameof(Resources.Messages.StateError))]

        [System.ComponentModel.DataAnnotations.StringLength
            (maximumLength: 100, MinimumLength = 3)]

        [System.ComponentModel.DataAnnotations.Display
            (Name = nameof(Resources.DataDictionary.State))]
        public string State { get; set; }
        // **********

        [System.ComponentModel.DataAnnotations.Required(
            ErrorMessageResourceType =typeof(Resources.Messages),
            ErrorMessageResourceName =nameof(Resources.Messages.CountryError))]

        [System.ComponentModel.DataAnnotations.StringLength
            (maximumLength: 100, MinimumLength = 3)]

        [System.ComponentModel.DataAnnotations.Display
            (Name = nameof(Resources.DataDictionary.Country))]
        public string Country { get; set; }
        // **********

        [System.ComponentModel.DataAnnotations.Required(
            ErrorMessageResourceType =typeof(Resources.Messages),
            ErrorMessageResourceName =nameof(Resources.Messages.PhoneNumberError))]

        [System.ComponentModel.DataAnnotations.StringLength
            (maximumLength: 100, MinimumLength = 3)]

        [System.ComponentModel.DataAnnotations.Display
            (Name = nameof(Resources.DataDictionary.PhoneNumber))]
        public string PhoneNumber { get; set; }
        // **********
        [System.ComponentModel.DataAnnotations.Display
            (Name = nameof(Resources.DataDictionary.FullName))]

        [System.ComponentModel.DataAnnotations.Required
            (AllowEmptyStrings = false)]

        [System.ComponentModel.DataAnnotations.StringLength
            (maximumLength: 50, MinimumLength = 3)]
                
        public string FullName { get; set; }
        // **********

        //***********
        [System.ComponentModel.DataAnnotations.Display
            (Name = nameof(Resources.DataDictionary.IsActive))]
        public bool IsActive { get; set; }
        //***********
        //***********
        [System.ComponentModel.DataAnnotations.Display
            (Name = nameof(Resources.DataDictionary.TotalShoping))]
        public decimal TotalShoping { get; set; }
        //***********
        #endregion
        //***********************************************************************
    }
}
