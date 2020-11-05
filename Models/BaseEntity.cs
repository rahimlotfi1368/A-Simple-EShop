namespace Models
{
    public class BaseEntity:System.Object
    {
        public BaseEntity()
        {
            Id = System.Guid.NewGuid();
        }

        [System.ComponentModel.DataAnnotations.Key]

        [System.ComponentModel.DataAnnotations.Required
                            (AllowEmptyStrings = false,
                            ErrorMessageResourceType = typeof(Resources.Messages),
                            ErrorMessageResourceName = nameof(Resources.Messages.RequiredValidationErrorMessage))]
               

        [System.ComponentModel.DataAnnotations.Display
                   (ResourceType =typeof(Resources.DataDictionary),Name =nameof(Resources.DataDictionary.ID))]
                               
        public System.Guid Id { get; set; }
    }
}
