using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Core.Entities
{
	public class BasketItem
	{

        public int Id  { get; set; }
        public string name { get; set; }
        public string PictureUrl { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        public int Quntity { get; set; }
        public decimal price { get; set; }

    }
}
