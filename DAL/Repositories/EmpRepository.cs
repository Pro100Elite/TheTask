using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class EmpRepository : IEmpRepository
    {
        private SqlConnectionStringBuilder sqlConnectionString = new SqlConnectionStringBuilder();

        public EmpRepository()
        {
            sqlConnectionString.DataSource = @".\SQLEXPRESS";
            sqlConnectionString.InitialCatalog = "TheTaskDb2";
            sqlConnectionString.IntegratedSecurity = true;
            sqlConnectionString.Pooling = true;
        }

        public IEnumerable<Emp> GetAll()
        {
            var emps = new List<Emp>();

            using (SqlConnection connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText = "sp_GetEmp";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)

                {
                    while (reader.Read())
                    {
                        var emp = new Emp
                        {
                            EmpNo = (decimal)reader["EMPNO"],
                            EmpName = (string)reader["ENAME"],
                            Job = reader["JOB"] as string,
                            Mgr = reader["MGR"] as decimal?,
                            HireDate = reader["HIREDATE"] as DateTime?,
                            Sal = reader["SAL"] as decimal?,
                            Comm = reader["COMM"] as decimal?,
                            DeptNo = reader["DEPTNO"] as decimal?
                        };
                        emps.Add(emp);
                    }
                }
            }

            return emps;
        }

        public IEnumerable<Emp> GetEmpsHierarchy(decimal? MgrNo)
        {
            var emps = new List<Emp>();

            using (SqlConnection connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();

                if (MgrNo != null)
                {
                    cmd.CommandText = "sp_Subordinates";
                    cmd.Parameters.AddWithValue("@MgrNo", MgrNo);
                }
                else
                {
                    cmd.CommandText = "sp_President";
                }

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)

                {
                    while (reader.Read())
                    {
                        var emp = new Emp
                        {
                            EmpNo = (decimal)reader["EMPNO"],
                            EmpName = (string)reader["ENAME"],
                            Job = reader["JOB"] as string,
                            Mgr = reader["MGR"] as decimal?,
                            HireDate = reader["HIREDATE"] as DateTime?,
                            Sal = reader["SAL"] as decimal?,
                            Comm = reader["COMM"] as decimal?,
                            DeptNo = reader["DEPTNO"] as decimal?
                        };
                        emps.Add(emp);
                    }
                }
            }

            return emps;
        }

        public void Create(Emp emp)
        {
            using (SqlConnection connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText = "sp_AddEmp";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EMPNO", emp.EmpNo);
                cmd.Parameters.AddWithValue("@ENAME", emp.EmpName);
                cmd.Parameters.AddWithValue("@JOB", emp.Job);
                cmd.Parameters.AddWithValue("@MGR", emp.Mgr);
                cmd.Parameters.AddWithValue("@HIREDATE", emp.HireDate);
                cmd.Parameters.AddWithValue("@SAL", emp.Sal);
                cmd.Parameters.AddWithValue("@COMM", emp.Comm);
                cmd.Parameters.AddWithValue("@DEPTNO", emp.DeptNo);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(decimal? empNo)
        {
            //sp_DeleteEmp
        }
    }
}
