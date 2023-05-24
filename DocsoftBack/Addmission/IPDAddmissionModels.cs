using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsoftBack.Addmission
{
	public class IPDAddmissionModels:BedModels
	{
		public DateTime Admit_Time { get; set; } =  DateTime.UtcNow;
		public DateTime Discharge_Datetime { get; set; } =  DateTime.UtcNow;

		public string Diagnose { get; set; }
		public string IPD_ID { get; set; }
	 
		public string Secondary_Dr { get; set; }
		public string Bil_Amount { get; set; }
		public string Discount_Per { get; set; }
		public string Discount_Amount { get; set; }
		public string TDS_Per { get; set; }
		public string faculty { get; set; }
		public string Parent_Name { get; set; }
		public string Parent_Mobile { get; set; }
		public string TDS_Amount { get; set; }
		public string Amount_After_Dis { get; set; }








	}
}
