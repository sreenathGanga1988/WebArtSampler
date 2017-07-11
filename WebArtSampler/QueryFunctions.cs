using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebArtSampler
{
    public static class QueryFunctions
    {

        public static DataTable ReturnQueryResultDatatable(String Qry)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString.ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = Qry;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();

                    dt.Load(rdr);
                }
            }

            return dt;
        }
    }
}