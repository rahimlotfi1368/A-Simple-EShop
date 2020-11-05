namespace Models
{
    public class OrderDetails:BaseEntity
    {
        public OrderDetails():base()
        {

        }
        //***********************************************************************
        #region ModelRelationShips
        public System.Guid OrderId { get; set; }
        public Order Order { get; set; }
        public System.Guid PieId { get; set; }
        public Pie Pie { get; set; }
        #endregion
        //***********************************************************************


        //***********************************************************************
        #region Properties
        [System.ComponentModel.DataAnnotations.Display
            (Name = nameof(Resources.DataDictionary.Amount))]

        [System.ComponentModel.DataAnnotations.Required
            (AllowEmptyStrings = false)]
        public int Amount { get; set; }
        //************************************
        [System.ComponentModel.DataAnnotations.Display
            (Name = nameof(Resources.DataDictionary.Price))]

        [System.ComponentModel.DataAnnotations.Required
            (AllowEmptyStrings = false)]
        public decimal Price { get; set; }
               
        #endregion
        //***********************************************************************
    }
}
