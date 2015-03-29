using Microsoft.Practices.Prism.Commands;
using Route_Beer.Models;
using System.ComponentModel;
using Newtonsoft.Json;
using Microsoft.Win32;
using System.IO;
using System.Collections.ObjectModel;
using Route_Beer.Services;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Net;

namespace Route_Beer.ViewModels
{
	class TestViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private Test test = new Test();
		private ObservableCollection<Test> testList = new ObservableCollection<Test>();
		public DelegateCommand AddTestCommand { get; set; }
		public DelegateCommand OpenTestsCommand { get; set; }
		public DelegateCommand SaveTestsCommand { get; set; }
		public DelegateCommand RunTestsCommand { get; set; }

		public ObservableCollection<Test> TestList
		{
			get { return testList; }
			set
			{
				testList = value;
				NotifyPropertyChanged("TestList");
			}
		}

		private bool persistBaseUrl;
		public bool PersistBaseUrl
		{
			get { return persistBaseUrl; }
			set {
				persistBaseUrl = value;
				NotifyPropertyChanged("PersistBaseUrl");
			}
		}

		private bool persistRoute;
		public bool PersistRoute
		{
			get { return persistRoute; }
			set {
				persistRoute = value;
				NotifyPropertyChanged("PersistRoute");
			}
		}

		private string testResults;
		public string TxtTestResults
		{
			get { return testResults; }
			set
			{
				testResults = value;
				NotifyPropertyChanged("TxtTestResults");
			}
		}

		public string TxtTestName
		{
			get { return test.Name; }
			set {
				test.Name = value;
				NotifyPropertyChanged("TxtTestName");
			}
		}

		public string TxtBaseUrl
		{
			get { return test.BaseUrl; }
			set {
				test.BaseUrl = value;
				NotifyPropertyChanged("TxtBaseUrl");
			}
		}

		public string TxtRoute
		{
			get { return test.Route; }
			set {
				test.Route = value;
				NotifyPropertyChanged("TxtRoute");
			}
		}

		public RestRequest CmbRequestType
		{
			get { return test.RequestType; }
            set {
				test.RequestType = value;
				NotifyPropertyChanged("CmbRequestType");
			}
		}

		public string TxtExpectedResult
		{
			get { return test.ExpectedResult; }
            set {
				test.ExpectedResult = value;
				NotifyPropertyChanged("TxtExpectedResult");
			}
		}

		public TestViewModel()
		{
			AddTestCommand = new DelegateCommand(AddTest, test.IsValid);
			OpenTestsCommand = new DelegateCommand(OpenTests);
			SaveTestsCommand = new DelegateCommand(SaveTests);
			RunTestsCommand = new DelegateCommand(RunTests);
		}

		protected void NotifyPropertyChanged(string info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
				AddTestCommand.RaiseCanExecuteChanged();
			}
		}

		public void RunTests()
		{
			TxtTestResults = string.Empty;

			foreach(var t in testList)
			{
				var prefix = string.Empty;
				if(!t.FullUrl.StartsWith("http://"))
				{
					prefix = "http://";
				}
				HttpWebRequest req = (HttpWebRequest)WebRequest.Create(prefix + t.FullUrl);
				HttpWebResponse res;

				try
				{
					res = (HttpWebResponse)req.GetResponse();
				}
				catch(WebException e)
				{
					res = (HttpWebResponse)e.Response;
				}

				Stream resStream = res.GetResponseStream();

				TxtTestResults += "Test: " + t.Name + "\n";
				TxtTestResults += "Status: " + res.StatusCode + "\n";
				TxtTestResults += "Content: \n" + new StreamReader(resStream).ReadToEnd() + "\n";

				TxtTestResults += "------------------------------------------------\n";
			}
		}

		public void AddTest()
		{
			var previousTest = test;

			testList.Add(test);
			NotifyPropertyChanged("TestList");

			test = new Test();
			TxtTestName = string.Empty;
			CmbRequestType = previousTest.RequestType;

			if (!persistRoute)
			{
				TxtRoute = string.Empty;
			}
			else
			{
				TxtRoute = previousTest.Route;
			}

			if (!persistBaseUrl)
			{
				TxtBaseUrl = string.Empty;
			}
			else
			{
				TxtBaseUrl = previousTest.BaseUrl;
			}
		}

		public void OpenTests()
		{
			var jsonTests = string.Empty;

			OpenFileDialog openDialog = new OpenFileDialog();
			openDialog.Filter = "JSON files (.json)|*.json";

			bool? result = openDialog.ShowDialog();

			if(result == true)
			{
				jsonTests = File.ReadAllText(openDialog.FileName);
			}

			testList = DeSerializeTests(jsonTests);
			NotifyPropertyChanged("TestList");
		}

		public void SaveTests()
		{
			SaveFileDialog saveDialog = new SaveFileDialog();
			saveDialog.FileName = "Tests";
			saveDialog.DefaultExt = ".json";
			saveDialog.Filter = "JSON files (.json)|*.json";

			bool? result = saveDialog.ShowDialog();

			if(result == true)
			{
				File.WriteAllText(saveDialog.FileName, SerializeTests(testList));
			}
		}

		private string SerializeTests(ObservableCollection<Test> testList)
		{
			return JsonConvert.SerializeObject(testList, Formatting.Indented);
		}

		private ObservableCollection<Test> DeSerializeTests(string json)
		{
			return JsonConvert.DeserializeObject<ObservableCollection<Test>>(json);
		}
	}
}
