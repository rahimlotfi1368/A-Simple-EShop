using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class ShowProfileInformationViewModel:System.Object
    {
        public ShowProfileInformationViewModel():base()
        {

        }

        public Models.User User { get; set; }
        public IEnumerable<Models.Order> UserOrders { get; set; }
        public IEnumerable<Models.OrderDetails> UserOrderDetails { get; set; }


    }
}
