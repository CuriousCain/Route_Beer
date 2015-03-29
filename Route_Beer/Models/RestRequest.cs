using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route_Beer.Models
{
	enum RestRequest
	{
		[Description("Post request")]
		POST,
		[Description("Put request")]
		PUT,
		[Description("Get request")]
		GET,
		[Description("Delete request")]
		DELETE
	}
}
