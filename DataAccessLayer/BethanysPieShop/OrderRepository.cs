using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.BethanysPieShop
{
    public class OrderRepository:Repository<Models.Order>,IOrderRepository
    {
        public OrderRepository(Models.DataBaseContext dataBaseContext):base(dataBaseContext)
        {

        }

        public IEnumerable<Order> GetAllOrdersByUserId(Guid UserId)
        {
            var theOrder = DataBaseContext.Orders
                     .Where(current => current.UserId == UserId && current.IsFinaly == true)
                     .Include(current => current.User)
                     .ToList();
            return theOrder;
        }

        public Order GetOrderByUserId(Guid UserId)
        {
           var theOrder = DataBaseContext.Orders
                     .Where(current => current.UserId == UserId && current.IsFinaly==false)
                     .Include(current => current.User)
                     .FirstOrDefault();

            return theOrder;
        }

        public bool UpdateSumOfOrder(Order order)
        {
            try
            {
                order.Sum = DataBaseContext.OrderDetails
                          .Where(current => current.OrderId == order.Id)
                          .Select(current => current.Amount * current.Price)
                          .Sum();

                DataBaseContext.Update(order);
                DataBaseContext.SaveChanges();
                return true;
                          
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}
