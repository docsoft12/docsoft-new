using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DocsoftBack.CommonModels;
using DocsoftBack;
using DocsoftBack.Doctor;

namespace Docsoftnew.Controllers
{
	public class DoctorController : Controller
	{
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
			var sql = "insert into Comman_Items (Item_Type,Item,Status)values ('Chief_Complaint','" + kk + "','Active')";
			await MainEngine.ExecuteQuery(sql,kk);
			Console.WriteLine("Successfully Run!");
			return Json(kk);

		}



		public JsonResult GetComplents()
		{
			List<string> complents = new List<string>();

			var sql = "select Item from Comman_Items where Item_type = 'Chief_Complaint'";
			complents = MainEngine.GetList<string>(sql);
			return  Json(complents);
			
		}
	}
}
