using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class DeptRepository: IDeptRepository
    {
        private SqlConnectionStringBuilder sqlConnectionString = new SqlConnectionStringBuilder();
        //string connectionString = @"Data Source =.\SQLEXPRESS; Initial Catalog = TheTaskDb2; Integrated Security = True";

        public DeptRepository()
        {
            sqlConnectionString.DataSource = @".\SQLEXPRESS";
            sqlConnectionString.InitialCatalog = "TheTaskDb2";
            sqlConnectionString.IntegratedSecurity = true;
            sqlConnectionString.Pooling = true;
        }

        public IEnumerable<Dept> GetAll()
        {
            var depts = new List<Dept>();

            using (SqlConnection connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM DEPT;";

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
    }
}
