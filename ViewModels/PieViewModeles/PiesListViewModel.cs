using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class PiesListViewModel:System.Object
    {
        public PiesListViewModel():base()
        {

        }

        public IEnumerable<Models.Pie> piesListByCategory { get; set; }
        public string CurrentPieCategory { get; set; }
    }
}
