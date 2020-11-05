namespace Models
{
    public class Pie:BaseEntity
    {
        public Pie():base()
        {

        }

        //*******************************************
        #region ModelRelationShips
        public System.Guid CatagoryId { get; set; }
        public Catagory PieCatagory { get; set; }
        public System.Collections.Generic.IList<OrderDetails> OrderDetails { get; set; }
        #endregion

        //*******************************************
        [System.ComponentModel.DataAnnotations.Required
                            (AllowEmptyStrings = false,
                            ErrorMessageResourceType = typeof(Resources.Messages),
                            ErrorMessageResourceName = nameof(Resources.Messages.RequiredValidationErrorMessage))]

        [System.ComponentModel.DataAnnotations.Display
                   (ResourceType =typeof(Resources.DataDictionary),Name =nameof(Resources.DataDictionary.Name))]
        public string Name { get; set; }

        //*******************************************
        [System.ComponentModel.DataAnnotations.Required
                            (AllowEmptyStrings = false,
                            ErrorMessageResourceType = typeof(Resources.Messages),
                            ErrorMessageResourceName = nameof(Resources.Messages.RequiredValidationErrorMessage))]

        [System.ComponentModel.DataAnnotations.Display
                   (ResourceType =typeof(Resources.DataDictionary),Name =nameof(Resources.DataDictionary.ShortDescription))]
        public string ShortDescription { get; set; }
        //*******************************************
        [System.ComponentModel.DataAnnotations.Required
                            (AllowEmptyStrings = false,
                            ErrorMessageResourceType = typeof(Resources.Messages),
                            ErrorMessageResourceName = nameof(Resources.Messages.RequiredValidationErrorMessage))]

        [System.ComponentModel.DataAnnotations.Display
                   (ResourceType =typeof(Resources.DataDictionary),Name =nameof(Resources.DataDictionary.LongDescription))]
        public string LongDescription { get; set; }
        //*******************************************

        [System.ComponentModel.DataAnnotations.Required
                            (AllowEmptyStrings = false,
                            ErrorMessageResourceType = typeof(Resources.Messages),
                            ErrorMessageResourceName = nameof(Resources.Messages.RequiredValidationErrorMessage))]

        [System.ComponentModel.DataAnnotations.Display
                   (ResourceType =typeof(Resources.DataDictionary),Name =nameof(Resources.DataDictionary.AllergyInformation))]
        public string AllergyInformation { get; set; }
        //*******************************************
        [System.ComponentModel.DataAnnotations.Required
                            (AllowEmptyStrings = false,
                            ErrorMessageResourceType = typeof(Resources.Messages),
                            ErrorMessageResourceName = nameof(Resources.Messages.RequiredValidationErrorMessage))]

        [System.ComponentModel.DataAnnotations.Display
                   (ResourceType =typeof(Resources.DataDictionary),Name =nameof(Resources.DataDictionary.Price))]
        public decimal Price { get; set; }
        //*******************************************

        [System.ComponentModel.DataAnnotations.Display
           (ResourceType = typeof(Resources.DataDictionary),
           Name = nameof(Resources.DataDictionary.ImageName))]
        public string ImageName { get; set; }
        //*******************************************
        [System.ComponentModel.DataAnnotations.Display
                   (ResourceType =typeof(Resources.DataDictionary),Name =nameof(Resources.DataDictionary.IsPieOfTheWeek))]
        public bool IsPieOfTheWeek { get; set; }
        //*******************************************
        [System.ComponentModel.DataAnnotations.Display
                   (ResourceType =typeof(Resources.DataDictionary),Name =nameof(Resources.DataDictionary.InStock))]
        public bool InStock { get; set; }
        //*******************************************
        [System.ComponentModel.DataAnnotations.Display
                          (ResourceType = typeof(Resources.DataDictionary),
                            Name = nameof(Resources.DataDictionary.Visit))]
        public int Visit { get; set; }
        //*******************************************
    }
}
