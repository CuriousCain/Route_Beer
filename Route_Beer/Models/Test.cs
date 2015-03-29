using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route_Beer.Models
{
	class Test
	{
		public string Name { get; set; }
		public string BaseUrl { get; set; }
		public string Route { get; set; }
		public RestRequest RequestType { get; set; }
		public Status PassingStatus { get; set; }
		public string ExpectedResult { get; set; } //TODO: Set extra output
		
		public Test()
		{
			PassingStatus = Status.Neutral;
		}

		public string FullUrl { get { return BaseUrl + Route; } }

		public bool IsValid()
		{
			if (string.IsNullOrEmpty(Name))
			{
				return false;
			}
			else if (string.IsNullOrEmpty(BaseUrl))
			{
				return false;
			}
			else if (string.IsNullOrEmpty(Route))
			{
				return false;
			}
			else
			{
				return true;
			}
		}
	}
}
