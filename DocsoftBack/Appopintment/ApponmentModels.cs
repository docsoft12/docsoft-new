using DocsoftBack.Registration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsoftBack.Appopintment
{
	public class ApponmentModels:RegisterModels
	{
		public string Search { get; set; }

        public int Consultant_ID { get; set; }
		[Required(ErrorMessage = "Please select Apointmnet Time")]
		public string Ap_Time { get; set; }

		public string Url { get; set; } = "Suc";
        public string Time_Slot { get; set; }

        public string Status { get; set; }
        public DateTime Attended_Time { get; set; } = DateTime.UtcNow;
	
		public string faculty { get; set; }
		public decimal Fees { get; set; }
		public decimal Fees_Received { get; set; }
		public DateTime Date_ { get; set; }
		public DateTime Date_Reg { get; set; }
 
		public string Recept_No { get; set; }
		public string Age { get; set; }
		public string Booked_By { get; set; }
		public string Appointment_ID { get; set; }
		public int id { get; set; }
        public string Source { get; set; }
		public string Payment_Mode { get; set; }
		public string Payment_Ref { get; set; }








	}
}
