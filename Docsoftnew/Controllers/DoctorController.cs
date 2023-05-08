using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DocsoftBack.CommonModels;
using DocsoftBack;
using DocsoftBack.Doctor;
using DocsoftBack.Doctor.Presception;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Data.SqlClient;

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
		public async Task<IActionResult> AddMedicine(DoctorCheckupModels models)
		{

			try
			{
				var sql = "insert into Prescription (Appointment_ID,UHID,CheckupID,Sr_No,Medicine,Dosage,Duration)values('"+models.Appointment_ID+"','"+models.UHID+"','13','1','"+models.Medicine+"','"+models.Dosage+"','"+models.Duration+"')";
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
			var json = TempData["DoctorCheckupModels"] as string;

			 if(json == null)
			{
				return RedirectToAction("NameList");

			}

			var myModel = JsonConvert.DeserializeObject<DoctorCheckupModels>(json);
			Console.WriteLine("Done from Check");
			return View(myModel);
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
		public JsonResult GetExamin(string find)
		{
			List<string> GetMedi = new();
			try
			{
				var sql = "select Examination from Examination where Examination Like '%" + find + "%' ";
				
				GetMedi = MainEngine.GetList<string>(sql).ToList();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}


			return Json(GetMedi);
		}


		public int k = 0;
		[HttpPost]
		public IActionResult GetSelect(string apd , string UHID)
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

	 






		string result;
		[HttpPost]
		public JsonResult AddExamin(string find,string apd)
		{
	 
			try
			{
				 
				try
				{
					var sq1 = "select Examination from Examination where Examination = '" + find + "'";

					 result = MainEngine.GetFirst<string>(sq1);
					Console.WriteLine("Nothing");

				 
						var sql = "insert into Examination(Ap_ID, Examination, Date_) values ('" + apd + "','" + result + "','" + DateTime.Now.ToString("MM-dd-yyyy HH:mm") + "')";
						MainEngine.ExecuteQuery<string>(sql);




 




					// Handle the result here
				}
				catch (InvalidOperationException ex)
				{
					if (ex.Message.Contains("Sequence contains no elements"))
					{
						 
							var sql = "insert into Examination(Ap_ID, Examination, Date_) values ('" + apd + "','" + find + "','" + DateTime.Now.ToString("MM-dd-yyyy HH:mm") + "')";
							MainEngine.ExecuteQuery<string>(sql);

						 
						 
							
						 
					}
					 

					
				 
				}



			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}


			return Json(null);
		}





		[HttpPost]
		public async Task<JsonResult> GetSearchMed(string find)
		{
			List<string> medicines = new List<string>();

			try
			{
				using (var connection = new SqlConnection(MainEngine.GetConnection))
				{
					await connection.OpenAsync();

					var query = "SELECT Medicine FROM Medicines WHERE Medicine LIKE @SearchString";
					using (var command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@SearchString", "%" + find + "%");

						using (var reader = await command.ExecuteReaderAsync())
						{
							while (await reader.ReadAsync())
							{
								var medicine = reader.GetString(reader.GetOrdinal("Medicine"));
								medicines.Add(medicine);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			return Json(medicines);
		}






		public JsonResult GetPrescriptions(string apoint)
		{
			List<PrescaptionModels> GetMedi = new();
			try
			{
				var sql = "select * from Prescription where Appointment_ID = '" + apoint + "'";
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
		public  async Task<JsonResult> AddCheackup(string bps , string pulses , string fevers , string apoint ,string uhid, string cheaf, string Spo2,string Next_date , string Notes, string CheckupID,string PA)
		{
			try
			{

				var sql3 = "select Consultant_ID from Appoint_OPD where Appointment_ID = '"+ apoint + "'";

				string result = MainEngine.GetFirst<string>(sql3);




				var sql1 = "select Emp_Name from Employee where Employee_ID = '" + result + "'";
				string name = MainEngine.GetFirst<string>(sql1);




				int chk = Convert.ToInt32(CheckupID) +1;





				string bp = $"{bps}-mmHg";
				string pulse = $"{pulses}-bpm";
				string Fever = $"{fevers}-°F";
				 
				var sql = "insert into Dr_CheckUp(Appointment_ID,UHID,Chief_Complaint,Date_,BP,Pulse,PA,Fever,Spo2,NextFollowUp,Note,CheckupID,Consultant)values ('" + apoint + "','" + uhid + "','" + cheaf + "','" + DateTime.Now.ToString("MM-dd-yyyy HH:mm") + "','" + bp + "','" + pulse + "','" + PA + "','" + Fever + "','" + Spo2 + "','" + Next_date + "','" + Notes + "','" + chk + "','" + name + "')";
					await MainEngine.ExecuteQuery<string>(sql);


			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);

			}

			int ks = Convert.ToInt32(apoint);
			 
			string url = $"{GetApi.ApiUrl}//Prescription?id={ks}";
			return Json(new { redirectUrl =  url });

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
		public async Task<JsonResult> delete_row(string apoint, string srn)
		{

			var sql = $"Delete from Prescription where Appointment_ID = {apoint} and Sr_No = {srn}";
			await MainEngine.ExecuteQuery<string>(sql);

			return Json("Done it From deleterow");
		}




	
		

		public int srn = 0;
		public int results = 0;
		[HttpPost]
		public async Task<JsonResult> AddMedic(string Medicine,string Duration, string QtyPer, string EatTime,string TimeTab,string apoint, string UHID)
		{


			try
			{
				var sql1 = "SELECT MAX(CAST(CheckupID AS INT)) as CheckupID FROM Prescription";

				  results = MainEngine.GetFirst<int>(sql1);

			}
			catch (InvalidOperationException ex)
			{
				if (ex.Message.Contains("Sequence contains no elements"))
				{
					results = 0;
					 }
			}

			try
			{
				var sqlsr = "SELECT MAX(CAST(Sr_No AS INT )) as Sr_No FROM Prescription where Appointment_ID= '" + apoint + "'  ";

			  srn = MainEngine.GetFirst<int>(sqlsr);

			}catch(Exception ex)
			{
				srn = 0;
				
			}

			int k = results;
			k = k + 1;

			string drt = $"{Duration} Days";
			srn++;
			var sql = "insert into Prescription (Appointment_ID,UHID,CheckupID,Sr_No,Medicine,Dosage,Duration)values('" + apoint + "','" + UHID + "','" + k + "','" + srn + "','" + Medicine + "','" + TimeTab + "','"+drt+"')";
		
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
