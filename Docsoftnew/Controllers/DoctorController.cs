using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DocsoftBack.CommonModels;
using DocsoftBack;
using DocsoftBack.Doctor;

namespace Docsoftnew.Controllers
{
	public class DoctorController : Controller
	{
		public static DoctorCheckupModels GetDoctorModels;
		public IActionResult Apoinment()
		{
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
