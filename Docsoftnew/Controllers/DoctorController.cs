using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DocsoftBack.CommonModels;
using DocsoftBack;
using DocsoftBack.Doctor;
using DocsoftBack.Doctor.Presception;

namespace Docsoftnew.Controllers
{
	public class DoctorController : Controller
	{
		public static DoctorCheckupModels GetDoctorModels;
		public IActionResult Apoinment()
		{
			return View();
		}



		 

		[HttpPost]
		public async Task<IActionResult> AddMedicine(IDoctorCheckupModels models)
		{

			try
			{
				var sql = "insert into Prescription (Appointment_ID,UHID,CheckupID,Sr_No,Medicine,Dosage,Duration)values('64','20230325-5','13','1','"+models.Medicine+"','"+models.Dosage+"','"+models.Duration+"')";
				await MainEngine.ExecuteQuery(sql,models);
				Console.WriteLine("Success");
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			return View();

		}







		public IActionResult CheckUp()
		{


			return View();
		}
		 
		public async  Task<JsonResult> AddComplaint(string kk)
		{
			var sql_new = "select Item from Comman_Items where Item = '" + kk + "' ";
			string k = MainEngine.GetFirst<string>(sql_new);
			if (k!=null || k!="")
			{

				var sql = "insert into Comman_Items (Item_Type,Item,Status)values ('Chief_Complaint','" + kk + "','Active')";
				
				await MainEngine.ExecuteQuery(sql, kk);
			Console.WriteLine("Successfully Run!");

			}
		return Json(kk);

		}



		public IActionResult NameList()
		{
			return View();
		}



		[HttpPost]
		public  JsonResult GetSearchMed(string find)
		{
			List<string> GetMedi = new();
			try
			{
				var sql = "select Medicine from Medicines where Medicine Like '%"+find+"%' ";
				GetMedi =  MainEngine.GetList<string>(sql).ToList();
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
			}


			return Json(GetMedi);
		}




	
		public JsonResult GetPrescriptions()
		{
			List<PrescaptionModels> GetMedi = new();
			try
			{
				var sql = "select * from Prescription";
				GetMedi = MainEngine.GetList<PrescaptionModels>(sql).ToList();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}


			return Json(GetMedi);
		}




		[HttpPost]
		public JsonResult SearchMedi(string search)
		{
			  


			return Json(GetDoctorModels);
		}



		[HttpPost]
		public JsonResult SetCheafComplent (string comp)
		{
			GetDoctorModels = new()
			{
				Chief_Complaint = comp
			};


			 
			return Json(GetDoctorModels);
		}


		public JsonResult test()
		{

			Console.WriteLine($"call from : Component {GetDoctorModels.Chief_Complaint}");

			return Json(GetDoctorModels);
		}		 


		[HttpPost]
		public async Task<JsonResult> AddMedic(string Medicine,string Duration, string QtyPer, string EatTime,string TimeTab)
		{
			var sql = "insert into Prescription (Appointment_ID,UHID,CheckupID,Sr_No,Medicine,Dosage,Duration)values('25','202023-25','34','800','"+Medicine+"',"+QtyPer+","+Duration+")";

			await MainEngine.ExecuteQuery<string>(sql);

			return  Json("Done it From AddMec");
		}

			public JsonResult GetComplents()
		{
			List<string> complents = new List<string>();

			var sql = "select Item from Comman_Items where Item_type = 'Chief_Complaint'";
			complents = MainEngine.GetList<string>(sql);
			string[] arr = complents.ToArray();
			return  Json(arr);
			
		}
	}
}
