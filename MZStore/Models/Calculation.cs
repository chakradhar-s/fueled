using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MZStore.Models
{
    public class Calculation
    {
        public decimal Price(int quan,decimal money)
        {
            return quan * money;
        }
        public decimal Total { get; set; }
    }
}