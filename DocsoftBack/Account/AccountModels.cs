using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsoftBack.Account
{
	public class AccountModels
	{
        public string Name { get; set; }
		public string Particular { get; set; }

        public string Credit_Debit { get; set; }
        public string DateTime { get; set; }
        public string Updated_By { get; set; }
		public string Refrence_No { get; set; }
		public string Credit { get; set; }
		public string Payment_Mode { get; set; }
		public string Payment_Ref { get; set; }
        public int Bill_No { get; set; }
        public string Payment_Of { get; set; }

    }
}
