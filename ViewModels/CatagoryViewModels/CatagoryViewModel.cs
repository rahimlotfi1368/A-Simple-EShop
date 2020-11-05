namespace ViewModels
{
    public class CatagoryViewModel
    {
        public CatagoryViewModel():base()
        {

        }

        [System.ComponentModel.DataAnnotations.Required
                          (AllowEmptyStrings = false,
                          ErrorMessageResourceType = typeof(Resources.Messages),
                          ErrorMessageResourceName = nameof(Resources.Messages.RequiredValidationErrorMessage))]


        [System.ComponentModel.DataAnnotations.Display
                 (ResourceType = typeof(Resources.DataDictionary), Name = nameof(Resources.DataDictionary.ID))]

        public System.Guid Id { get; set; }

        //**************************************************************************************
        [System.ComponentModel.DataAnnotations.Required
                           (AllowEmptyStrings = false,
                           ErrorMessageResourceType = typeof(Resources.Messages),
                           ErrorMessageResourceName = nameof(Resources.Messages.RequiredValidationErrorMessage))]
        [System.ComponentModel.DataAnnotations.Display
                  (ResourceType = typeof(Resources.DataDictionary), Name = nameof(Resources.DataDictionary.CatagoryName))]
        public string CatagoryName { get; set; }
        //**************************************************************************************

        [System.ComponentModel.DataAnnotations.Display
                   (ResourceType = typeof(Resources.DataDictionary), Name = nameof(Resources.DataDictionary.CatagoryDescription))]
        public string Description { get; set; }
        //**************************************************************************************
    }
}
