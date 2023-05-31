using DocsoftBack;
using Microsoft.AspNetCore.Mvc;
using DocsoftBack.Addmission;
using DocsoftBack.Doctor;
using Newtonsoft.Json;
using System.Data;
using System.Security.Cryptography;
using System.Data.SqlClient;
 

namespace Docsoftnew.Controllers
{
	public class AddmissionController : Controller
	{
		public IActionResult Addmission()
		{
			return View();
		}


		public IActionResult BedMaster()
		{
			return View();
		}


		[HttpPost]
		public JsonResult GetCharges(string combo)
		{

			String Charges = CommanFunctions.CellValue("Select Charges from bed_Status where Bed_type='" + combo + "' ");
			string chars = "";
			string shows;
			if (Charges != "")
			{
				chars = Charges;
				shows = "hide";
				return Json(new { Charges = chars, Visible = shows});
			}
			else
			{
				shows = "show";
				chars = "";
				return Json(new
				{ Visible = shows
				});
			}


		}


		[HttpPost]
		 public JsonResult GetFill(string chatg)
		{
			List<BedModels> getbeds = new();
			string da = "SELECT * from Bed_Status where bed_type = '" + chatg + "' ";

		    getbeds =  MainEngine.GetList<BedModels>(da).ToList();

			return Json(getbeds);
		}
 


		public int count;


		public JsonResult BedMasterAdd(string start, string end, string chatg, string charges)
		{
			string Added = "";

			try
			{
				for (int i = int.Parse(start); i <= int.Parse(end); i++)
				{
					if (CommanFunctions.CellValue("Select bed_type from bed_Status where bed_No =" + i + "") != "")
					{

						count++;
					}
				}

				if (count > 0)
				{
					return Json(new { allread = "In this range beds are already added" });
				}


				for (int i = int.Parse(start); i <= int.Parse(end); i++)
				{
					SqlDataAdapter da = new SqlDataAdapter("INSERT INTO Bed_Status (Bed_Type,Bed_No,Status,Charges) VALUES('" + chatg + "', '" + i + "','Available','" + charges + "')", CommanFunctions.cn);

					DataSet ds = new DataSet();
					da.Fill(ds);
					Added = Added + "-" + i.ToString();

				}
			}
			catch (Exception ex)
			{
				Added = "";

			}
			if (Added !="")
			{
				Added = "Successfully Added Beds";

			}
			else
			{
				Added = "";

			}
			return Json(new { success= Added });
		}



		public int k = 0;
		[HttpPost]
		public IActionResult GetSelect(string IPD , string Admit,string uhid)
		{


			IPDRATEMODELS models = new IPDRATEMODELS()
			{
				IPD_ID = IPD,
				Admit_Time = DateTime.Parse(Admit),
				UHID = uhid

			};
			TempData["IPDRATEMODELS"] = JsonConvert.SerializeObject(models);
			return Json(new { redirectUrl = Url.Action("IPDBilling", "Addmission") });

		}

		[HttpPost]
		public async Task<JsonResult> delete_row(string apoint, string srn)
		{

			var sql = $"Delete from Billing where IPD_ID = {apoint} and Sr_No = {srn}";
			await MainEngine.ExecuteQuery<string>(sql);

			return Json("Done it From deleterow");
		}


		[HttpPost]
		public async Task<JsonResult> AddRoom(string ipd, string Desc, string chatg, string rate, string unit, string Amount)
		{

			string[] list = { ipd, Desc, chatg, rate, unit, Amount };

			if (list.Any(x => string.IsNullOrWhiteSpace(x)))
			{

			}
			else
			{



				try
				{

					try
					{
						var getmaxvalue = "SELECT MAX(Sr_No) AS Sr_No FROM Billing where IPD_ID = '" + ipd + "'";

						max = MainEngine.GetFirst<int>(getmaxvalue);



					}
					catch (InvalidOperationException ex)
					{
						 
							max = 0;

						 

					}

					max = max + 1;
					var sql = "insert into Billing (IPD_ID,Sr_No,Discription,Item_Type,Rate,Unit,Amount)Values('" + ipd + "','" + max + "','" + Desc + "','" + chatg + "','" + rate + "','" + unit + "','" + Amount + "')";
					await MainEngine.ExecuteQuery<string>(sql);
				}
				catch (Exception ex)
				{

				}
			}
			return Json("");
		}
		 
		public int max = 0;
		[HttpPost]
		public async Task<JsonResult> AddDesc(string ipd,string Desc,string chatg , string rate, string unit , string Amount)
		{

			string[] list = { ipd, Desc, chatg, rate, unit, Amount };

			if (list.Any(x => string.IsNullOrWhiteSpace(x)))
			{

			}
			else
			{  
				try
				{
	 var getmaxvalue = "SELECT MAX(Sr_No) AS Sr_No FROM Billing WHERE IPD_ID = '"+ipd+"'";
		 
					max = MainEngine.GetFirst<int>(getmaxvalue);
				}
				catch (System.Data.DataException ex)
				{
			 
					Console.WriteLine("Error occurred while parsing column 0: " + ex.Message);
					 
					max = 0; 
				}

				max = max + 1;
					var sql = "insert into Billing (IPD_ID,Sr_No,Discription,Item_Type,Rate,Unit,Amount)Values('" + ipd + "'," + max + ",'" + Desc + "','" + chatg + "','" + rate + "','" + unit + "','" + Amount + "')";
					await MainEngine.ExecuteQuery<string>(sql);
				 
			}
			return Json("");
		}

		public class IPD_SET
		{
            public string Bill_Amount { get; set; }
			public int Discount_Per { get; set; }

            public string Discount_Amount { get; set; }
            public string Amount_After_Dis { get; set; }
        
		}

		[HttpPost]
		public JsonResult GetAmount(string IPD)
		{
			List<IPD_SET> getlist = new();
			try
			{
				var sql = "Select Bill_Amount,Discount_Per,Discount_Amount,Amount_After_Dis from IPD where IPD_ID = '" + IPD+"'`";
				getlist = MainEngine.GetList<IPD_SET>(sql).ToList();

			}
			catch (InvalidOperationException ex)
			{
				return Json("you not saveing propertly");
			}

			return Json(getlist);
		}

		[HttpPost]
		public async Task<JsonResult> SaveBill(string ipd , string  AferDisc, string Discount , string AfterDiscs, string Amount)
		{ 

			 
			string sql = "update IPD set Bill_Amount= '" + Amount + "' ,Discount_Per='" + Discount + "' ,Discount_Amount='" + AfterDiscs + "' ,Amount_After_Dis= '" + AferDisc + "' where IPD_ID  = '" + ipd + "' ";
			await MainEngine.ExecuteQuery<string>(sql);
			 

			TempData["SuccessMessage"] = "Successfully saved";

			return Json(new { redirectUrl = Url.Action("IPDBilling", "Addmission") });
		}



		[HttpPost]
		public JsonResult GetDesc(string Desc)
		{
			List<IPDRATEMODELS> ListMod = new();
			try
			{

				var sql = "select Rate,Item_Type from Ipd_Rate_Card where Description = '"+Desc+"'";
				ListMod = MainEngine.GetList<IPDRATEMODELS>(sql).ToList();

			}
			catch(Exception ex)
			{ 
			}

			return Json(ListMod);

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

			    TempData["SuccessMessage"] = "Successfully saved.";

			}
			catch (InvalidOperationException ex)
			{
				if (ex.Message.Contains("Sequence contains no elements"))
				{
					if(models.Chief_Complaint == null && models.UHID == null)
					{
						TempData["ErrorMessage"] = "Please Select Patient and please Mentation Cheaf Complaint and Bed";
					}
					else if(models.UHID == null)
					{
						TempData["ErrorMessage"] = "Please Select Patient and please Mentation Bed";

					}
					else if(models.Chief_Complaint == null)
					{
						TempData["ErrorMessage"] = "Please Mentation Cheaf Complaint";
					}
					else
					{
						TempData["ErrorMessage"] = "Please Mentation Bed";


					}
				}
		
					 
			}

			return View();
		}

		public string ReciptNo;
		public    JsonResult Discharge(string date_,string ipd,string uhid)
		{

			try
			{
				SqlDataAdapter da1 = new SqlDataAdapter("Update IPD_Bed set End_DateTime = '" + date_ + "'Where IPD= '" + ipd + "'and End_DateTime  is NUll ", MainEngine.GetConnection);
				DataSet ds1 = new DataSet();
				da1.Fill(ds1);

				//MessageBox.Show(ds1.Tables[0].Rows.Count.ToString());


				SqlDataAdapter da = new SqlDataAdapter("Update IPD set Discharge_Datetime = '" + date_ + "',  Status='Discharged'  Where IPD_ID= '" + ipd + "'", MainEngine.GetConnection);
				DataSet ds = new DataSet();
				da.Fill(ds);


				SqlDataAdapter da2 = new SqlDataAdapter("Update Bed_Status set Status ='Available', IPD= Null, UHID= Null  where IPD =" + ipd + " and status ='Occupied' ", MainEngine.GetConnection);
				DataSet ds2 = new DataSet();
				da2.Fill(ds2);


				ReciptNo = CommanFunctions.CellValue("Select Bill_No from Billing  where IPD_ID='" + ipd + "'");
				if (ReciptNo == "")
				{

					ReciptNo = CommanFunctions.Recipt_No("IPD", uhid);
				}

				else
				{
					SqlDataAdapter da3 = new SqlDataAdapter("Update Billing set Bill_No = '" + ReciptNo + "', Created_By = 'User' , DateTime_='" + DateTime.Now.ToString("MM-dd-yyyy HH:mm") + "' where IPD_ID =" + ipd + " ", MainEngine.GetConnection);
					DataSet ds3s = new DataSet();
					da3.Fill(ds3s);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			int ks = Convert.ToInt32(ipd);

			string url = $"{GetApi.ApiUrl}//Bill?id={ks}";
			return Json(new { redirectUrl = url });

		}


		public JsonResult GetBed(string btype)
		{
			List<string> getb = new();

			var sql = "Select Bed_No from Bed_Status Where Status='Available' and bed_type='"+btype+"'";
			getb = MainEngine.GetList<string>(sql).ToList();
			Console.WriteLine(btype);
			return Json(getb);

		}

		public class DayWiseCalculation
		{

            public string GetDays  { get; set; }
			public string RoundDays { get; set; }
			 
            public string Charges  { get; set; }
            public string Bed_Type  { get; set; }
		}



		public JsonResult GetTable(string ipd)
		{
			List<IPDRATEMODELS> iPDRATEMODELs = new();

			try
			{
				var sql = "Select * from Billing Where IPD_ID = '" + ipd + "'";
				iPDRATEMODELs = MainEngine.GetList<IPDRATEMODELS>(sql).ToList();

			}
			catch(InvalidOperationException ex)
			{

			}

			return Json(iPDRATEMODELs);
		}


		[HttpPost]
		public JsonResult GetData(string IPD , string Admit , string GetDate)
		{ 
			
			DayWiseCalculation days = new();

			DateTime dt1 = DateTime.Parse(GetDate);

			DateTime dt = DateTime.Parse(Admit);

			TimeSpan ts = dt1 - dt;

			 days.GetDays = ts.TotalDays.ToString("0.00");

			if (ts.TotalDays > ts.Days)
			{
				days.RoundDays = (ts.Days + 1).ToString();
			}
			var sql = "Select Charges  from Bed_status  where IPD = '" + IPD + "'";
			var sql1 = "Select Bed_type  from Bed_status  where IPD = '" + IPD + "'";

			days.Charges = MainEngine.GetFirst<string>(sql);
			days.Bed_Type = MainEngine.GetFirst<string>(sql1);
			return Json(new { Bed_Type  = days.Bed_Type,Charges = days.Charges, RoundDays  = days.GetDays , GetDays= days.GetDays});

		}





		public IActionResult IPDBilling()
		{
			var json = TempData["IPDRATEMODELS"] as string;
			if (json==null)
			{
				return RedirectToAction("NameAdmit");

			}

			var myModel = JsonConvert.DeserializeObject<IPDRATEMODELS>(json);
			Console.WriteLine("Done from Billing");
			return View(myModel);
		}


 
	}
}
