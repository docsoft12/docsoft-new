using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DocsoftBack.Addmission
{
	public class IpdBilling
	{

		public static List<string> GetDescriptions()
		{ 
		 var sql = "select Description from Ipd_Rate_Card where status ='Active'";
		  return MainEngine.GetList<string>(sql).ToList();


		}





	}
}
