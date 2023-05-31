using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsoftBack.Registration
{
	public class RegisterModels : IRegisterModels
	{
		public int? ID { get; set; }
		public string UHID { get; set; }
		
		public string Mobile_No { get; set; }
		[Required(ErrorMessage = "Please enter Aadhar No")]
		[StringLength(16)] 
		public string? Aadhar_No { get; set; }
		[Required(ErrorMessage = "Please enter Patient Name")]

		public string? Patient_Name { get; set; }

		[Required(ErrorMessage = "Please enter Weight")]

		public decimal? Weight { get; set; }
		public string? Sex { get; set; }

		public DateTime? Birthdate { get; set; }
		[Required(ErrorMessage = "Enter a your address")]
		public string? Address { get; set; }
		[Required(ErrorMessage = "please enter your city name")]


		public string? City { get; set; }
		[Required(ErrorMessage = "Enter Your valid PinCode")]
		[StringLength(6)]
		public string Pincode { get; set; }
		public DateTime? Reg_By { get; set; }
		public string Occupation { get; set; }
		[Required(ErrorMessage = "Please  Enter your Height")]

		public string Height { get; set; }
		public DateTime Reg_Date { get; set; }
		public string Parent_Name { get; set; }
		public string Parent_Mobile { get; set; }
		public string Parent_Relation { get; set; }

		public DateTime Updated { get; set; }
		public string P_Image { get; set; }
	}
}