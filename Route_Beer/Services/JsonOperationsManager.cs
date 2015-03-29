using Newtonsoft.Json;
using Route_Beer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route_Beer.Services
{
	class JsonOperationsManager
	{
		public static string SerializeTests(ObservableCollection<Test> testList)
		{
			return JsonConvert.SerializeObject(testList, Formatting.Indented);
		}

		public static ObservableCollection<Test> DeSerializeTests(string json)
		{
			return JsonConvert.DeserializeObject<ObservableCollection<Test>>(json);
		}
	}
}
