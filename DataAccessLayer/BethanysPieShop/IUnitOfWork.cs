using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.BethanysPieShop
{
    public interface IUnitOfWork
    {
        public ICatagoryRepository CatagoryRepository { get; }
        public IPieRepository PieRepository { get; }
        public IUserRepository UserRepository { get; }
        //public IRoleRepository RoleRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public IOrderDetailsRepository OrderDetailsRepository { get; }
    }
}
