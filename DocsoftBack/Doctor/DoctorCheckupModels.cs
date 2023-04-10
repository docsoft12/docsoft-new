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
		public string Dosage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public string Duration { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public string Instruction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public string Medicine { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public string Quantity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public string Sr_No { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public string Ap_ID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public string Examination { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
	}
}
