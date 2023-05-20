using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
 
using System.Net.Mail;
using System.Net;

using System.IO;

namespace DocsoftBack.Addmission

{
   public class CommanFunctions
	{
        static public SqlConnection cn = new SqlConnection(MainEngine.GetConnection);

        public static string[] Getitemlist(String IType)
        {
            // SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings["conn"]);


            SqlDataAdapter da = new SqlDataAdapter("Select distinct trim(Item) from Comman_items where Item_Type='" + IType + "'and Status ='Active'", cn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            string[] Itemlist = new string[ds.Tables[0].Rows.Count];
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                Itemlist[i] = (ds.Tables[0].Rows[i][0].ToString());
                Itemlist[i].Trim();
            }
            return Itemlist;
        }
        public static String getid(String Tbname,String cname)
        {
            //DateTimePicker timePickerFrom;

           

            SqlDataAdapter da1 = new SqlDataAdapter("Select * from "+Tbname+" Where "+cname+"= '"+ DateTime.Today.Date.ToString("MM-dd-yyyy") +"'", cn);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);


            String HUID = DateTime.Now.Date.ToString("yyyy");
            HUID = HUID + DateTime.Now.Date.ToString("MM");
            HUID = HUID + DateTime.Now.Date.ToString("dd");
            int i = ds1.Tables[0].Rows.Count;
            i++;

            HUID = HUID + "-" + i;

            return HUID;




        }

        public static String getPetient(String UHID)
        {

           
                SqlDataAdapter da = new SqlDataAdapter("Select Patient_Name from Petient_Details where UHID='" + UHID + "'", cn);
                DataSet ds = new DataSet();
            
                 da.Fill(ds);

                return ds.Tables[0].Rows[0][0].ToString();
        


        }
        public static int PutAPdt(String ApID)
        {

            SqlDataAdapter da = new SqlDataAdapter("Update Appoint_OPD set Attended_Time='"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +"'  where Appointment_ID='" + ApID + "'", cn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            return 1;

        }



        public static String getAPID(String Tbname, String cname)
        {
            //NOT IN USE 

            DateTime dt = DateTime.Today.Date;
            dt=dt.AddDays(1);





            SqlDataAdapter da1 = new SqlDataAdapter("Select * from " + Tbname + " Where " + cname + " between'" + DateTime.Today.Date.ToString("MM-dd-yyyy HH:mm") + "' and '" + dt.ToString("MM-dd-yyyy HH:mm") + "'", cn);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);


            String HUID = DateTime.Now.Date.ToString("yyyy");
            HUID = HUID + DateTime.Now.Date.ToString("MM");
            HUID = HUID + DateTime.Now.Date.ToString("dd");
            int i = ds1.Tables[0].Rows.Count;
            i++;

            HUID = HUID + "-" + i;

            return HUID;
        }
        public static DataTable adapter(String Q)
        {

            SqlDataAdapter da = new SqlDataAdapter(Q, cn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;



        }
        public static string CellValue(String Q)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(Q, cn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0][0].ToString();
                }
                return "";
            }
            catch { return ""; }
        }
        public static decimal P_Age(String UHID)
        {
            decimal Age=0;

            DataTable dt = CommanFunctions.adapter("select Birthdate from Petient_Details where  UHID ='" + UHID + "'");

            try
            {
                

                DateTime datetime1 = DateTime.Parse(dt.Rows[0][0].ToString());

                //txt_age.Text = Convert.ToString((thisyear * 30));

                Age = DateTime.Now.Year - datetime1.Year;
                decimal thismonth = DateTime.Now.Month - datetime1.Month;
                thismonth = thismonth / 12;
                Age = Age + thismonth;

                Age = (decimal)System.Math.Round(Age, 1);

                
                
                //MessageBox.Show(thisyear.ToString());

            }
            catch
            { }
            return Age;

        }
        public static String Recipt_No(String category, String UHID)
        {
            String Recept_No = "00";


                SqlDataAdapter da4 = new SqlDataAdapter("Insert into Recept_No (Category,Refrence_No) Values('"+ category + "','"+UHID+"') ", CommanFunctions.cn);
                DataSet ds4 = new DataSet();
                da4.Fill(ds4);
            

            DataTable dt5 = CommanFunctions.adapter("Select * from  Recept_No  where Refrence_No='"+UHID+"' order by ID DESC");
            if (dt5.Rows.Count > 0)
            {

                Recept_No = dt5.Rows[0][0].ToString();
            }

            return Recept_No;

        }
   
        public static string Randompassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();

            string password = new string(Enumerable.Repeat(chars, 5)
              .Select(s => s[random.Next(s.Length)]).ToArray());

            return password;

        }

      
        public static int Update(String Table_Name, String where_Field, String where_value, String set_filed , String NewValue)
        {

            SqlDataAdapter da = new SqlDataAdapter("Update "+Table_Name+" set Status ='"+ NewValue + "' where "+ where_Field + "  =  "+where_value+"", cn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            return 1;

        }





    }
}
