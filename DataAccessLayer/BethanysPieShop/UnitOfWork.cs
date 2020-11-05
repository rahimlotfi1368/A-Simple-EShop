using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.BethanysPieShop
{
    public class UnitOfWork:System.Object,IUnitOfWork
    {
        protected Models.DataBaseContext DataBaseContext { get; set; }
        public UnitOfWork(Models.DataBaseContext dataBaseContext):base()
        {
            if (dataBaseContext==null)
            {
                dataBaseContext = new Models.DataBaseContext();
            }

            DataBaseContext = dataBaseContext;
        }

        //********************************************Pie*************************************
        private BethanysPieShop.IPieRepository pieRepository;

        public BethanysPieShop.IPieRepository PieRepository
        {
            get
            {
                if (pieRepository == null)
                {
                    pieRepository = new PieRepository(DataBaseContext);
                }

                return (pieRepository);
            }
        }
        //**************************************************************************************

        //********************************************Catagory*************************************
        private BethanysPieShop.ICatagoryRepository catagoryRepository;

        public BethanysPieShop.ICatagoryRepository CatagoryRepository
        {
            get
            {
                if (catagoryRepository == null)
                {
                    catagoryRepository = new CatagoryRepository(DataBaseContext);
                }

                return (catagoryRepository);
            }
        }
        //**************************************************************************************

        //********************************************User*************************************
        private BethanysPieShop.IUserRepository userRepository;

        public BethanysPieShop.IUserRepository UserRepository
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(DataBaseContext);
                }

                return (userRepository);
            }
        }
        //**************************************************************************************
        //********************************************Role*************************************
        //private BethanysPieShop.IRoleRepository roleRepository;

        //public BethanysPieShop.IRoleRepository RoleRepository
        //{
        //    get
        //    {
        //        if (roleRepository == null)
        //        {
        //            roleRepository = new RoleRepository(DataBaseContext);
        //        }

        //        return (roleRepository);
        //    }
        //}
        //**************************************************************************************

        //********************************************Order*************************************
        private BethanysPieShop.IOrderRepository orderRepository;

        public BethanysPieShop.IOrderRepository OrderRepository
        {
            get
            {
                if (orderRepository == null)
                {
                    orderRepository = new OrderRepository(DataBaseContext);
                }

                return (orderRepository);
            }
        }
        //**************************************************************************************

        //********************************************orderDetails*************************************
        private BethanysPieShop.IOrderDetailsRepository orderDetailsRepository;

        public BethanysPieShop.IOrderDetailsRepository OrderDetailsRepository
        {
            get
            {
                if (orderDetailsRepository == null)
                {
                    orderDetailsRepository = new OrderDetailsRepository(DataBaseContext);
                }

                return (orderDetailsRepository);
            }
        }
        //**************************************************************************************
    }
}
