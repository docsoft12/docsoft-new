using DocsoftBack;
using Microsoft.AspNetCore.Mvc;
using DocsoftBack.Addmission;
using DocsoftBack.Doctor;
using Newtonsoft.Json;

namespace Docsoftnew.Controllers
{
	public class AddmissionController : Controller
	{
		public IActionResult Addmission()
		{
			return View();
		}




		public int k = 0;
		[HttpPost]
		public IActionResult GetSelect(string apd, string UHID)
		{

			var sql1 = "select Top 1 CheckupID from Dr_CheckUp  ORDER BY CheckupID DESC  ";

			var result = MainEngine.GetFirst<int>(sql1);

			k = Convert.ToInt32(result);
			k = k + 1;



			DoctorCheckupModels models = new DoctorCheckupModels()
			{
				Appointment_ID = apd,
				UHID = UHID,
				CheckupID = k.ToString()

			};
			TempData["DoctorCheckupModels"] = JsonConvert.SerializeObject(models);
			return Json(new { redirectUrl = Url.Action("CheckUp", "Doctor") });

		}




		public IActionResult NameAdmit()
		{ 


			return View();
		}



		[HttpPost]
		public async Task<IActionResult> Addmission(IPDAddmissionModels models)
		{
			try
			{

				string se = "select BirthDate from Petient_Details where UHID = '" + models.UHID+"'";
				string rg = MainEngine.GetFirst<string>(se);



				string fs = "select Bed_Type from Bed_Status where Bed_No = '" + models.Bed_No + "'";
				string rgs = MainEngine.GetFirst<string>(fs);

				string fss = "select Charges from Bed_Status where Bed_No = '" + models.Bed_No + "'";
				int rgss = MainEngine.GetFirst<int>(fss);

		

				DateTime db = Convert.ToDateTime(rg);
				int age = 0;
				age = DateTime.Now.Subtract(db).Days;
				age = age / 365;


				 
				 



				var pupdate = "Update Petient_Details Set  Parent_Name='" + models.Parent_Name + "',Parent_Mobile= '" + models.Parent_Mobile + "',Parent_Relation='" + models.Parent_Relation + "' where UHID = '" + models.UHID + "'";
				var sql = "Insert into IPD (Consultant_ID,UHID,Admit_Time,faculty,Booked_By,Status,Age,Chief_Complant,Diagnose,Secondary_Dr)Values('" + models.Consultant_ID + "','" + models.UHID + "','" + models.Admit_Time.ToString("MM-dd-yyy HH:mm") + "','" + models.faculty + "','1 ', 'Admitted' ," + age + ", '" + models.Chief_Complaint + "','" + models.Diagnose + "','" + models.Secondary_Dr + "')";
				
				await MainEngine.ExecuteQuery<string>(pupdate);
				await MainEngine.ExecuteQuery<string>(sql);
				string getsqlipd = "Select IPD_ID from IPD Where UHID ='" + models.UHID + "'order by IPD_ID desc";
				string IPD = MainEngine.GetFirst<string>(getsqlipd);
				var addIpdbed = "Insert into IPD_Bed (Bed_No,UHID,Start_DateTime,IPD,Bed_Type, Charges)Values(" + models.Bed_No + ",'" + models.UHID + "','" + models.Admit_Time + "','" + IPD + "','" + rgs + "'," + rgss + ")";

				await MainEngine.ExecuteQuery<string>(addIpdbed);
				var bedstatus = "Update Bed_Status Set  Status ='Occupied', UHID= '" + models.UHID + "', IPD= " + IPD + "  where  Bed_No='" + models.Bed_No + "'";

				await MainEngine.ExecuteQuery<string>(bedstatus);

				 
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			return View();
		}


		public JsonResult GetBed(string btype)
		{
			List<string> getb = new();

			var sql = "Select Bed_No from Bed_Status Where Status='Available' and bed_type='"+btype+"'";
			getb = MainEngine.GetList<string>(sql).ToList();
			Console.WriteLine(btype);
			return Json(getb);

		}



		public IActionResult IPDBilling()
		{
			 
			return View();
		}


 
	}
}
