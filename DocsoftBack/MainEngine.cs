using Dapper;
using System.Data;
using System.Data.SqlClient;


namespace DocsoftBack
{
	public  class MainEngine
	{



		public static string GetConnection = "Data Source=localhost\\sqlexpress;Initial Catalog=DatabaseH;Integrated Security=True";
			

	public static List<T> GetList<T>(string Querys)
		{
			using (IDbConnection conn = new SqlConnection(GetConnection))
			{

				return conn.Query<T>(Querys).ToList();

			}
		}


		public static T GetFirst<T>(string Querys)
		{
			using (IDbConnection conn = new SqlConnection(GetConnection))
			{

				return conn.QueryFirst<T>(Querys);

			}
		}


		public static async Task ExecuteQuery<T>(string qry, T model)
		{
			using (IDbConnection conn = new SqlConnection(GetConnection))
			{
				var execute = await conn.ExecuteAsync(qry,model);

			}

		}


    
	 }  
}
