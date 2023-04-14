using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsoftBack.Doctor
{
	public class DoctorCheckupModels : IDoctorCheckupModels
	{
		public string Appointment_ID { get; set; }
		public DateTime NextFollowUp { get; set; }
		public string UHID { get; set; } 
		public string Chief_Complaint { get; set; }
		public DateTime Date_ { get; set; }
		public string BP { get; set; }
		public string Pulse { get; set; }
		public string PA { get; set; }
		public string Fever { get; set; }
		public string Spo2 { get; set; }
		public string Note { get; set; }

		public string CheckupID { get; set; }
		public string Consultant { get; set; }
		public string DocAdd { get; set; }
		public string Dosage { get; set; }
		public string Duration { get; set; }
		public string Instruction { get; set; }
		public string Medicine { get; set; }
		public string Quantity { get; set; }
		public string Sr_No { get; set; }
		public string Ap_ID { get; set; }
		public string Examination { get; set; }
		public string QtyPer { get; set; }
		public string TimeTab { get; set; }
		public string EatTime { get; set; }


	}
}
