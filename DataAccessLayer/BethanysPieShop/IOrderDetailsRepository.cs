namespace DataAccessLayer.BethanysPieShop
{
    public interface IOrderDetailsRepository:IRepository<Models.OrderDetails>
    {
        public Models.OrderDetails GetOrderDetailsByPieIdAndOrderId(System.Guid pieId, System.Guid orderId);

        public System.Collections.Generic.IEnumerable<Models.OrderDetails> GetOrderDetailsByOrderId(System.Guid orderId);
    }
}
