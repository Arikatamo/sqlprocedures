using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLPROCEDURES
{
    class Employees
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
    }
    class Program
    {
        static DataTable GetEmployeesData(List<Employees> list)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Name");
            dt.Columns.Add("Gender");
            foreach (var item in list)
            {
                dt.Rows.Add(item.Id,item.Name,item.Gender);
            }
            return dt;
        }
        static void Main(string[] args)
        {
            List<Employees> list = new List<Employees>()
            {
                new Employees
                {
                    Id = 10,
                    Name = "Stepan",
                    Gender = "Male"
                },
                new Employees
                {
                    Id = 11,
                    Name = "Petia",
                    Gender = "Male"
                },
                new Employees
                {
                    Id = 12,
                    Name = "Galia",
                    Gender = "Female"
                }
            };
            string strCon = "Data Source = arikatamobook.database.windows.net; Initial Catalog = Arikatamo_Cook; User ID = Arikatamo; Password = As8rgd759648*";

            using (SqlConnection con = new SqlConnection(strCon))
            {
                SqlCommand cmd = new SqlCommand("ProcArikatamo", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter paramTVP = new SqlParameter()
                {
                    ParameterName = "@InputEmploees",
                    Value = GetEmployeesData(list)
                };
                cmd.Parameters.Add(paramTVP);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
