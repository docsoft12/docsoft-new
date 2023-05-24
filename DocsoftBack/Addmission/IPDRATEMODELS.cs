using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsoftBack.Addmission
{
	public class IPDRATEMODELS
	{
		public  string Discription { get; set; }
		public int Rate { get; set; }
		public string Unit { get; set; }
		public string Status { get; set; }
		public string Item_Type { get; set; }
        public DateTime Admit_Time { get; set; }
        public int Amount { get; set; }


        public string UHID { get; set; }
        public int ID { get; set; }
		public string Bill_No { get; set; }

		 
        public string IPD_ID { get; set; }
        public int Sr_No { get; set; }
        public DateTime DateTime_ { get; set; } = DateTime.UtcNow;
    }
}
