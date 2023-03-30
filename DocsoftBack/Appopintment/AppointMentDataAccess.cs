using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocsoftBack.CommonModels;
using DocsoftBack.Doctor;
using DocsoftBack.Recept;
using System.ComponentModel.Design;

namespace DocsoftBack.Appopintment
{
	public class AppointMentDataAccess
	{
        public List<EmployeeModels> GetEmployees()
		{
			var sql = "Select * From Employee Where Is_Consultant = 'Yes'";

			return MainEngine.GetList<EmployeeModels>(sql);

		}


		public List<string> GetCommon(string type)
		{
			var sql = "Select distinct trim(Item) from Comman_items where Item_Type='" + type + "' AND  Status ='Active'";
			return MainEngine.GetList<string>(sql);


		}




		// for timging cultant wise - Select Start_Time, End_TIme,Fees_F from Doctors_Timing  Where Employee_ID = '" + Empid + "' and Days ='" + lab_day.Text + "'"

		public List<DoctorTimingModels> GetTiming(string ID, string days)
		{
			var sql = "Select Start_Time, End_TIme,Fees_F from Doctors_Timing  Where Employee_ID = '" + ID + "' and Days ='" + days+ "'";
			return MainEngine.GetList<DoctorTimingModels>(sql);

		}

		public async Task <String> Recipt_No(string Catogory, string UHID)
		{
			
			String Recept_No = "00";
			 

			var insql = "Insert into Recept_No (Category,Refrence_No) Values('" + Catogory + "','" + UHID + "') ";

			ReceptModels models = new()
			{
			   Category = Catogory,
				Refrence_No = UHID

			};

			await MainEngine.ExecuteQuery(insql, models);


			 var select = "Select ID from  Recept_No  where Refrence_No='" + UHID + "' order by ID DESC";

			Recept_No = MainEngine.GetFirst<string>(select);
			 
			return Recept_No;

		}




	  public int APID()
		{
			int APID = 0;
			var sql = "Select Appointment_ID from  Appoint_OPD  order by  ID DESC";
			APID = MainEngine.GetFirst<int>(sql);

			return APID;
		}




		public static decimal P_Age(string UHID)
		{
			string musafir = "";
			decimal Age = 0;

			 var sql = "select Birthdate from Petient_Details where  UHID='" + UHID + "'";

			musafir = MainEngine.GetFirst<string>(sql);


			try
			{


				DateTime datetime1 = DateTime.Parse(musafir);

		
				Age = DateTime.Now.Year - datetime1.Year;
				decimal thismonth = DateTime.Now.Month - datetime1.Month;
				thismonth = thismonth / 12;
				Age = Age + thismonth;

				Age = (decimal)System.Math.Round(Age, 1);


				 

			}
			catch
			{ }
			return Age;

		}




		public async Task BookAppointment(ApponmentModels models)
		{
			var sql = "insert into Appoint_OPD(Consultant_ID,UHID, Ap_Time, Fees_Received, Time_Slot, faculty, Date_, Date_Reg, Discount, Age, Booked_By, Fees, Recept_No, Appointment_ID)values(@Consultant_ID,@ UHID, @Ap_Time, @Fees_Received, @Time_Slot, @faculty, Date_,@ Date_Reg, @Discount, @Age,@Booked_By, @Fees, @Recept_No, @Appointment_ID)";


			await MainEngine.ExecuteQuery(sql, models);


				}






	}
}
