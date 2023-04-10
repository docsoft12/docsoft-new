using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsoftBack.Doctor.Examination
{
	public class ExaminationModels : IExaminationModels
	{
		public string Ap_ID { get; set; }
		public string Examination { get; set; }
		public DateTime Date_ { get; set; }

	}
}
