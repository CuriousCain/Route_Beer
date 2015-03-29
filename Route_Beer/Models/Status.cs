using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route_Beer.Models
{
	enum Status
	{
		[Description("Passing")]
		Passing,
		[Description("Failing")]
		Failing,
		[Description("Neutral")]
		Neutral
	}
}
