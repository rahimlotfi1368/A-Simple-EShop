using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class ShowCartViewModel:System.Object
    {
        public ShowCartViewModel():base()
        {

        }

        public Models.OrderDetails OrderDetails { get; set; }
        public decimal SumOfEachOrderDetails { get; set; }
        public string ImageName { get; set; }

    }
}
