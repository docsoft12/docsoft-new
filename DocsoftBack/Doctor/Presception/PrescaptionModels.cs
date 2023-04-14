using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsoftBack.Doctor.Presception
{
	public class PrescaptionModels : IPrescaptionModels
	{
		public string Appointment_ID { get; set; }
		public string UHID { get; set; }
		public string CheckupID { get; set; }
		public string Sr_No { get; set; }
		public string Medicine { get; set; }
		public string Dosage { get; set; }
		public string Instruction { get; set; }
		public string Duration { get; set; }
		public string Quantity { get; set; }
		public string Ap_ID { get; set; }
		public DateTime Date_ { get; set; }
		public string Examination { get; set; }
	}
}
