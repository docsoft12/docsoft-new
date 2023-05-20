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
using AspNetCore.Reporting;
using System.Data;
using DocsoftBack.Account;
using System.Xml.Linq;
 
using System.Net;
using Microsoft.AspNetCore.Authorization;
using System.Web.Http.Results;
using Newtonsoft.Json;

namespace Docsoftnew.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		private readonly IWebHostEnvironment _envirment;

		 
		 
		public static string temp;
		public static DateTime days;

		public HomeController( IWebHostEnvironment envirment)
		{
		 
			_envirment = envirment;
			System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
			 
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



		public string GetName(string find)
		{
			var sql = "Select Patient_Name from Petient_Details  where UHID = '" + find + "'";
			return MainEngine.GetFirst<string>(sql);
		}


		public string GetAge(string Find)
		{
			var sql = "Select Birthdate from Petient_Details  where UHID = '" + Find + "'";
			return MainEngine.GetFirst<string>(sql);
		}


		public string geturl = "";


		[HttpGet]
 public IActionResult Appointment(ApponmentModels modes)
 {




			var json = TempData["ApponmentModels"] as string;

			 

			if (json == null)
			{
				// Return an error message or a default instance of the class
				return View();
			}

			var myModel = JsonConvert.DeserializeObject<ApponmentModels>(json);
		 
			return View(myModel);
 }


 
		[HttpPost]
		public IActionResult Search(ApponmentModels models)
		{

			temp =  models.Search;
			 

			return RedirectToAction("Appointment");
		
		}



		[HttpPost]
		public JsonResult GetPrint()
		{
			if(geturl !=null)
			{

				return Json(new {urls =geturl});

			}
			else
			{
				return Json("None");

			}

		}




		public string url = "";
		[HttpPost]
		 public async Task<IActionResult> BookApoint(ApponmentModels modes)
		 { 

			try
			{
			decimal Age;
			string kp  = GetAge(modes.UHID);
			DateTime datetime1 = DateTime.Parse(kp);
			//txt_age.Text = Convert.ToString((thisyear * 30));
			Age = DateTime.Now.Year - datetime1.Year;
			decimal thismonth = DateTime.Now.Month - datetime1.Month;
			thismonth = thismonth / 12;
			Age = Age + thismonth;

			Age = (decimal)System.Math.Round(Age, 1);

			//Console.WriteLine(modes.Ap_Time);
			string res = (int.Parse(acp.Recipt_No("OPD", modes.Search)) + 1).ToString();
				ApponmentModels models = new ApponmentModels()
				{
					Consultant_ID = modes.Consultant_ID,
					Status = "Active",
					Age = Age.ToString(),
					Ap_Time = modes.Ap_Time,
					Time_Slot = modes.Time_Slot,
					Attended_Time = modes.Attended_Time,
					faculty = modes.faculty,
					Fees = modes.Fees,
					Fees_Received = modes.Fees_Received,
					Recept_No = res,
					Booked_By = modes.Booked_By,
					Appointment_ID = (int.Parse(acp.APID().ToString()) + 1).ToString(),
					UHID = modes.UHID,
					Date_ = DateTime.UtcNow,
					Date_Reg = DateTime.UtcNow,
				 
			};
			AccountModels account = new AccountModels()
			{
				Name = models.UHID,
				Credit = models.Fees_Received.ToString(),
				Credit_Debit = "Credit",
				DateTime = DateTime.Now.ToString("MM-dd-yyyy HH:MM"),
				Particular = "Consulting fees",
				Payment_Mode = modes.Payment_Mode,
				Payment_Ref = modes.Payment_Ref,
				Refrence_No = models.Appointment_ID,
				Updated_By = "Amir Test" ,
				Payment_Of = "OPD",
			   Bill_No = (int.Parse(res))
			    
			};
		 	int id;
 
				await acp.BookAppointment(models);
				await acp.AddAccount(account);
				List<dynamic> str = MainEngine.GetList<dynamic>("select * from OPD_Bill");
				id = Convert.ToInt32(models.Appointment_ID);
				url = $"{GetApi.ApiUrl}//OPDBILL?id={id}";

				TempData["SuccessMessage"] = "Successfully Submited!";

				models.Url = url;
		    
				TempData["ApponmentModels"] = JsonConvert.SerializeObject(models);

			}
			catch (Exception ex)
			{
			 
				TempData["ErrorMessage"] = "please select Patient Name ";

			}
			return RedirectToAction("Appointment");
		}




		 

		public IActionResult Index()
		{
           

            

            return View();

        }



		[HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
            
                // Check the username and password against a database or other authentication method
                if (model.Username == "doctor" && model.Password == "lion")
                {
                    return RedirectToAction("Registration", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password");
                }
           

            return View(model);


            

        }



		[HttpPost]
		public JsonResult CHKNUMBER(string number)
			{
			
			List<RegisterModels> models = new();

			try
			{


				var sql = "select UHID , Patient_Name from Petient_Details where Mobile_No = '" + number+"'";

				models = MainEngine.GetList<RegisterModels>(sql).ToList();

			}
			catch(InvalidOperationException ex)
			{
				if (ex.Message.Contains("Sequence contains no elements"))
				{
					RegisterModels getmodels = new();
					getmodels.Parent_Name = "No Data Found";
					

					models.Add(getmodels);
				}
				}

			


			return Json(models);
		}




		[HttpPost]
		public JsonResult GetApooint(string UHID)
		{
			ApponmentModels models = new();
			models.Patient_Name = UHID;

			TempData["ApponmentModels"] = JsonConvert.SerializeObject(models);
			return Json(new { redirectUrl = Url.Action("Appointment", "Home") });
		}

		[HttpGet]
		public async Task<IActionResult> Registration()
		{
			temp = "";
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


				if (register.Mobile_No!=null && register.Patient_Name !="" && register.Birthdate!=null && register.Weight !=null && register.Address !=null )
				{
					await reg.AddRegistration(register);
					Console.WriteLine("Success");

					// All properties have a value
					return RedirectToRoute(new { action = "Appointment", controller = "Home", area = "" });
				}
				else
				{
					return RedirectToRoute(new { action = "Registration", controller = "Home", area = "" });
				}

			 
			
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			return RedirectToRoute(new { action = "Registration", controller = "Home", area = "" });
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