using DocsoftBack.Appopintment;
using DocsoftBack;
using DocsoftBack.Registration;
using Docsoftnew.Models;
using Docsoftnew.Views.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Docsoftnew.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		private readonly IWebHostEnvironment _envirment;

		private readonly IHomeTrain _train;
		 
		public static string temp;
		public HomeController(ILogger<HomeController> logger, IWebHostEnvironment envirment,IHomeTrain train)
		{
			_logger = logger;
			_envirment = envirment;
			_train = train;
		}
		 
		public Registration reg = new();
		private AppointMentDataAccess acp = new();
		List<ApponmentModels> Getapponments(string Find)
		{

			List<ApponmentModels> apponments = new();
			var sql = "Select UHID,Patient_Name,Mobile_No,Aadhar_No from Petient_Details  where UHID = '"+Find+"'";
			apponments = MainEngine.GetList<ApponmentModels>(sql);

            return apponments;
		}

		[HttpGet]
 public IActionResult Appointment(ApponmentModels modes)
 {
		    

			modes.Search = temp;
			return View(modes);
 }


[HttpPost]
public IActionResult Appointment(ApponmentModels modes, string k)
{

 modes.Search = temp;
ApponmentModels models = new ApponmentModels()
{

Consultant_ID = modes.Consultant_ID,
Ap_Time = modes.Ap_Time,
Time_Slot = modes.Time_Slot,
Attended_Time = modes.Attended_Time,
faculty = modes.faculty,
Fees = modes.Fees,
Fees_Received = modes.Fees_Received,
Date_ = modes.Date_,
Date_Reg = modes.Date_Reg,
Recept_No = modes.Recept_No,
Age = modes.Age,
Booked_By = modes.Booked_By,
Appointment_ID = modes.Appointment_ID
};



return View();
		}
 
		[HttpPost]
		public IActionResult Search(ApponmentModels models)
		{

			temp =  models.Search;
			 

			return RedirectToAction("Appointment");
		
		}

 
		public IActionResult Index()
		{
			temp = " ";
			return View();

		}
		[HttpGet]
		public async Task<IActionResult> Registration()
		{
			temp = " ";


			return View();
		}


		[HttpPost]
		public async Task<IActionResult> Registration(RegisterModels models, string base64image)
		{
			try
			{
				var model = new CaptureModels();

				string UID = reg.getid();

				var files = HttpContext.Request.Form.Files;
				if (files != null)
				{
					foreach (var file in files)
					{
						if (file.Length > 0)
						{
							var fielname = file.FileName;
							var myq = Convert.ToString(Guid.NewGuid());
							var fileex = Path.GetExtension(fielname);

							var newfile = string.Concat(myq, fileex);
							var filepath = Path.Combine(_envirment.WebRootPath, "images") + $@"\{newfile}";

							if (!string.IsNullOrEmpty(filepath))
							{
								var imagebyte = System.IO.File.ReadAllBytes(filepath);
								if (imagebyte != null)
								{
									StoreImage(imagebyte);

								}

							}





							return Json(true);
						}
						else
						{

							return Json(false);


						}
					}
				}

					RegisterModels register = new RegisterModels()
				{
					UHID = UID,
					Mobile_No = models.Mobile_No,
					Aadhar_No = models.Aadhar_No,
					Patient_Name = models.Patient_Name,
					Weight = models.Weight,
					Sex = models.Sex,
					Birthdate = models.Birthdate,
					Address = models.Address,
					City = models.City,
					Pincode = models.Pincode,
					Height = models.Height,
					Reg_Date = DateTime.Parse(DateTime.Today.ToString("yyyy-MM-dd"))


				};

				await reg.AddRegistration(register);
				Console.WriteLine("Success");
			
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			return View();
		}



		private string StoreImage(byte[] imageb)
		{
			string image = "";
			
				if (imageb != null)
				{
					string base64 = Convert.ToBase64String(imageb, 0, imageb.Length);
					string imageurl = string.Concat("data:image/jpg;base64,", base64);






					image = imageurl;
					return image;



				}





			Console.WriteLine("From Images");

			Console.WriteLine(image);


			return image;

		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}