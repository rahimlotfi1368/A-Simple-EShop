namespace Models
{
    public class Order:BaseEntity
    {
        public Order():base()
        {
            CreatedDate = System.DateTime.Now;
        }
        //***********************************************************************
        #region ModelRelationShips
        public System.Collections.Generic.IList<OrderDetails> OrderDetails { get; set; }
        public System.Guid UserId { get; set; }
        public User User { get; set; }
        #endregion
        //***********************************************************************


        //***********************************************************************
        #region Properties
        [System.ComponentModel.DataAnnotations.Display
            (Name = nameof(Resources.DataDictionary.CreatedDate))]

        [System.ComponentModel.DataAnnotations.Required
            (AllowEmptyStrings = false)]
        public System.DateTime CreatedDate { get; set; }
        //***********************************************
        [System.ComponentModel.DataAnnotations.Display
                   (ResourceType =typeof(Resources.DataDictionary),Name =nameof(Resources.DataDictionary.Sum))]
        public decimal Sum { get; set; }
        //***********************************************
        public bool IsFinaly { get; set; }
        #endregion
        //***********************************************************************
    }
}
