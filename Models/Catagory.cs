namespace Models
{
    public class Catagory:BaseEntity
    {
        public Catagory():base()
        {

        }
         [System.ComponentModel.DataAnnotations.Required
                            (AllowEmptyStrings = false,
                            ErrorMessageResourceType = typeof(Resources.Messages),
                            ErrorMessageResourceName = nameof(Resources.Messages.RequiredValidationErrorMessage))]
         [System.ComponentModel.DataAnnotations.Display
                   (ResourceType =typeof(Resources.DataDictionary),Name =nameof(Resources.DataDictionary.CatagoryName))]
        public string CatagoryName { get; set; }
        //*******************************************

        [System.ComponentModel.DataAnnotations.Display
                   (ResourceType =typeof(Resources.DataDictionary),Name =nameof(Resources.DataDictionary.CatagoryDescription))]
        public string Description { get; set; }
        //*******************************************

        public System.Collections.Generic.IList<Pie> Pies { get; set; }
    }
}
