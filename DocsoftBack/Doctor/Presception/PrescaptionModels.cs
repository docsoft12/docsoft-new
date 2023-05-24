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
		public string Aadhar_No { get; set; }
		public string Address { get; set; }
		public DateTime? Birthdate { get; set; }
		public string City { get; set; }
		public string Height { get; set; }
		public int? ID { get; set; }
		public string Mobile_No { get; set; }
		public string Occupation { get; set; }
		public string P_Image { get; set; }
		public string Parent_Mobile { get; set; }
		public string Parent_Name { get; set; }
		public string Parent_Relation { get; set; }
		public string Patient_Name { get; set; }
		public string Pincode { get; set; }
		public DateTime? Reg_By { get; set; }
		public DateTime Reg_Date { get; set; }
		public string Sex { get; set; }
		public DateTime Updated { get; set; }
		public decimal? Weight { get; set; }
	}
}
