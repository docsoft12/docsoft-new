using Microsoft.AspNetCore.Mvc;

namespace Docsoftnew.Controllers
{
	public class Doctor : Controller
	{
		public IActionResult Apoinment()
		{
			return View();
		}
	}
}
