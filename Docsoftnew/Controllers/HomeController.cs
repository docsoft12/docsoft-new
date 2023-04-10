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
using Newtonsoft.Json.Serialization;
using DocsoftBack.Doctor;

namespace Docsoftnew.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		private readonly IWebHostEnvironment _envirment;

		private readonly IHomeTrain _train;
		 
		public static string temp;
		public static DateTime days;

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
		public IActionResult Search(ApponmentModels models)
		{

			temp =  models.Search;
			 

			return RedirectToAction("Appointment");
		
		}

		[HttpPost]
		 public async Task<IActionResult> BookApoint(ApponmentModels modes)
		{



			Console.WriteLine(modes.Ap_Time);
			ApponmentModels models = new ApponmentModels()
			{





				Consultant_ID = modes.Consultant_ID,
			 
				
				Ap_Time = modes.Ap_Time,
				Time_Slot = modes.Time_Slot,
				Attended_Time = modes.Attended_Time,
				faculty = modes.faculty,
				Fees = modes.Fees,
				Fees_Received = modes.Fees_Received,
			 Recept_No = acp.Recipt_No("OPD", modes.Search),
				Booked_By = modes.Booked_By,
				Appointment_ID = acp.APID().ToString(),
				UHID = modes.UHID,
				Date_ = DateTime.UtcNow,
				Date_Reg = DateTime.UtcNow,
			 




			};


			await acp.BookAppointment(models);
			Console.WriteLine("Success");


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
					Reg_Date = DateTime.Parse(DateTime.Today.ToString("yyyy-MM-dd")),
				  

					
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



		 
		 
		public JsonResult gettime(string id, string id2)
		{
			List<DoctorTimingModels> md = new();
		  md =   acp.GetTiming(id, id2);


			return Json(md);


 
		}

	 public JsonResult  GETLISTTIME(string id)
	 {
			List<string> getaptime = new();
			string[] array_ = id.Split('-');






			string StartTime1 = array_[0];
			string EndTime1 = array_[1];

			DateTime slotE = DateTime.Parse(EndTime1);

			DateTime slotS = DateTime.Parse(StartTime1);
			getaptime.Clear();

			for (DateTime slotT = slotS; slotE > slotT; slotT.AddMinutes(10))
			{
				StartTime1 = slotT.ToString("HH:mm");

				slotT = slotT.AddMinutes(10);
				getaptime.Add(StartTime1.ToString());

			}



			return Json(getaptime);
		}




		public JsonResult GETUID(string id)
		{
			Console.WriteLine(id);
			var sql = "";
			string apponments = "";
			sql = "select UHID,Patient_Name,Mobile_No,Aadhar_No from Petient_Details where UHID = '" + id + "' OR Patient_Name = '" + id + "' OR Mobile_No = '" + id + "' OR  Aadhar_No = '" + id + "'  ";
			apponments = MainEngine.GetFirst<string>(sql);

			

			return Json(apponments);
		}







		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}



	}
}