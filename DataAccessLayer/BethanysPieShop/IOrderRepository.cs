namespace DataAccessLayer.BethanysPieShop
{
    public interface IOrderRepository:IRepository<Models.Order>
    {
        public System.Collections.Generic.IEnumerable<Models.Order> GetAllOrdersByUserId(System.Guid UserId);
        public Models.Order GetOrderByUserId(System.Guid UserId);
        public bool UpdateSumOfOrder(Models.Order order);
    }
}
