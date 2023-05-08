using DocsoftBack.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsoftBack.Addmission
{
	public class BedModels:DoctorCheckupModels
	{
        public string Bed_type { get; set; }
		public string Bed_No { get; set; }

        public string Status { get; set; }
        public int Charges { get; set; }
	}
}
