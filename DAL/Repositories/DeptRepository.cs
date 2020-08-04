using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class DeptRepository: IDeptRepository
    {
        string connectionStr = ConfigurationManager.ConnectionStrings["TaskDb"].ConnectionString;

        public IEnumerable<Dept> GetAll()
        {
            var depts = new List<Dept>();

            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();

                var cmd = connection.CreateCommand();

                cmd.CommandText = "sp_GetDepts";
                cmd.CommandType = CommandType.StoredProcedure;

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)

                {
                    while (reader.Read())
                    {
                        depts.Add(new Dept
                        {
                            DeptNo = (decimal)reader["DEPTNO"],
                            DeptName = (string)reader["DNAME"],
                            Loc = (string)reader["LOC"]
                        });
                    }
                }
            }

            return depts;
        }

        public void Create(Dept dept)
        {
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();

                var cmd = connection.CreateCommand();

                cmd.CommandText = "sp_AddDept";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DNAME", dept.DeptName);
                cmd.Parameters.AddWithValue("@LOC", dept.Loc);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
