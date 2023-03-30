using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsoftBack.Registration
{
	public class Registration
	{


		public  string getid()
		{

			int set;

			 var sql  = "Select Count(ID) from Petient_Details Where Reg_Date =  '" + DateTime.Today.Date.ToString("MM-dd-yyyy") + "'";

			set =  MainEngine.GetFirst<int>(sql);
			  



			string HUID = DateTime.Now.Date.ToString("yyyy");
			HUID = HUID + DateTime.Now.Date.ToString("MM");
			HUID = HUID + DateTime.Now.Date.ToString("dd");
			int i = set;
			i++;

			HUID = HUID + "-" + i;

			return HUID;




		}

		public async Task AddRegistration(RegisterModels models)
		{

			var sql = @"Insert into Petient_Details (UHID,Mobile_No,Aadhar_No,Patient_Name,Weight,Birthdate,Address,City,Pincode,Height) values(@UHID,@Mobile_No,@Aadhar_No,@Patient_Name,@Weight,@Birthdate,@Address,@City,@Pincode,@Height)";

			await MainEngine.ExecuteQuery(sql, models);
		}
	}
}
