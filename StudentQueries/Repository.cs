using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentQueries
{
	class Repository
	{
		private class DataSet
		{
			public List<Student> Students { get; set; }
			public List<Group> Groups { get; set; }
		}

		DataSet _dataset;

		public IEnumerable<Student> Students
		{
			get
			{
				return _dataset.Students;
			}
		}

		public IEnumerable<Group> Groups
		{
			get
			{
				return _dataset.Groups;
			}
		}

		public Repository()
		{
			_dataset = JsonConvert.DeserializeObject<DataSet>(File.ReadAllText("../../students.json"));
		}
	}
}
