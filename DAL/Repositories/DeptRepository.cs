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
    public class DeptRepository : IDeptRepository
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

        public Dept GetDept(decimal deptNo)
        {
            var dept = new Dept();

            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();

                var cmd = connection.CreateCommand();

                cmd.CommandText = "sp_GetDeptById";
                cmd.Parameters.AddWithValue("@deptNo", deptNo);
                cmd.CommandType = CommandType.StoredProcedure;

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)

                {
                    while (reader.Read())
                    {
                        dept.DeptNo = (decimal)reader["DEPTNO"];
                        dept.DeptName = (string)reader["DNAME"];
                        dept.Loc = (string)reader["LOC"];
                    }
                }
            }

            return dept;
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

        public void Edit(Dept dept)
        {
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText = "sp_UpdateDept";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DEPTNO", dept.DeptNo);
                cmd.Parameters.AddWithValue("@DNAME", dept.DeptName);
                cmd.Parameters.AddWithValue("@LOC", dept.Loc);

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(decimal deptNo)
        {
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();

                var cmd = connection.CreateCommand();

                cmd.CommandText = "sp_DeleteDept";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@deptNo", deptNo);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
