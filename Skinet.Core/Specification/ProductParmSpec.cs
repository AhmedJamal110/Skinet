using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Core.Specification
{
	public class ProductParmSpec
	{
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? Sort { get; set; }
		
		private const int MAXPAGESIZE = 20;
		
		private int pageSize = 18 ;

		public int PigeSize
		{
			get { return pageSize; }
			set { pageSize = value > MAXPAGESIZE ? MAXPAGESIZE : value ;  }
		}

		public int PigeIndex { get; set; } = 1;


		private string? _Search;

		public string? Search
		{
			get { return _Search; }
			set { _Search = value.ToLower(); }
		}


	}
}
