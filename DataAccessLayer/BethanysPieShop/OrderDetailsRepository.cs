using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.BethanysPieShop
{
    public class OrderDetailsRepository:Repository<Models.OrderDetails>,IOrderDetailsRepository
    {
        public OrderDetailsRepository(Models.DataBaseContext dataBaseContext):base(dataBaseContext)
        {

        }

        public IEnumerable<OrderDetails> GetOrderDetailsByOrderId(Guid orderId)
        {
            var orderDetailsOfOrder = DataBaseContext.OrderDetails
                                    .Where(current => current.OrderId == orderId)
                                    .Include(current=>current.Order)
                                    .ToList();

            return orderDetailsOfOrder;
        }

        public Models.OrderDetails GetOrderDetailsByPieIdAndOrderId(Guid pieId, Guid orderId)
        {
            var theOrderDetails = DataBaseContext.OrderDetails
                                .Where(current => current.PieId == pieId )
                                .Where(current=>current.OrderId==orderId)
                                .Include(current => current.Order)
                                .FirstOrDefault();

            return theOrderDetails;
        }
    }
}
