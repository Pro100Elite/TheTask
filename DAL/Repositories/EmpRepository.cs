using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class EmpRepository : IEmpRepository
    {
        string connectionStr = ConfigurationManager.ConnectionStrings["TaskDb"].ConnectionString;

        public IEnumerable<Emp> GetAll()
        {
            var emps = new List<Emp>();

            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText = "sp_GetEmps";
                cmd.CommandType = CommandType.StoredProcedure;

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

        public IEnumerable<Emp> GetByDept(decimal deptNo)
        {
            var emps = new List<Emp>();

            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText = "sp_GetEmpsByDept";
                cmd.Parameters.AddWithValue("@deptNo", deptNo);
                cmd.CommandType = CommandType.StoredProcedure;

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

        public IEnumerable<Emp> GetHierarchy()
        {
            var emps = new List<Emp>();

            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText = "sp_Hierarchy";
                cmd.CommandType = CommandType.StoredProcedure;

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
        public Emp GetEmp(decimal? empNo)
        {
            Emp emp = new Emp();

            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText = "sp_GetEmpById";
                cmd.Parameters.AddWithValue("@empNo", empNo);
                cmd.CommandType = CommandType.StoredProcedure;

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    emp.EmpNo = (decimal)reader["EMPNO"];
                    emp.EmpName = (string)reader["ENAME"];
                    emp.Job = reader["JOB"] as string;
                    emp.Mgr = reader["MGR"] as decimal?;
                    emp.HireDate = reader["HIREDATE"] as DateTime?;
                    emp.Sal = reader["SAL"] as decimal?;
                    emp.Comm = reader["COMM"] as decimal?;
                    emp.DeptNo = reader["DEPTNO"] as decimal?;
                }
            }
            return emp;
        }

        public IEnumerable<EmpPlusSalGrade> GetEmpsHierarchy(decimal? MgrNo)
        {
            var emps = new List<EmpPlusSalGrade>();

            using (SqlConnection connection = new SqlConnection(connectionStr))
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

                cmd.CommandType = CommandType.StoredProcedure;

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)

                {
                    while (reader.Read())
                    {
                        var emp = new EmpPlusSalGrade
                        {
                            EmpNo = (decimal)reader["EMPNO"],
                            EmpName = (string)reader["ENAME"],
                            Job = reader["JOB"] as string,
                            Mgr = reader["MGR"] as decimal?,
                            HireDate = reader["HIREDATE"] as DateTime?,
                            Sal = reader["SAL"] as decimal?,
                            Comm = reader["COMM"] as decimal?,
                            DeptNo = reader["DEPTNO"] as decimal?,
                            Grade = (int)reader["GRADE"]
                        };
                        emps.Add(emp);
                    }
                }
            }

            return emps;
        }

        public void Create(Emp emp)
        {
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText = "sp_AddEmp";
                cmd.CommandType = CommandType.StoredProcedure;
          
                cmd.Parameters.AddWithValue("@ENAME", emp.EmpName);
                cmd.Parameters.AddWithValue("@JOB", emp.Job);
                cmd.Parameters.AddWithValue("@MGR", emp.Mgr);
                cmd.Parameters.AddWithValue("@HIREDATE", emp.HireDate);
                cmd.Parameters.AddWithValue("@SAL", emp.Sal);

                if (emp.Comm != null)
                {
                    cmd.Parameters.AddWithValue("@COMM", emp.Comm);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@COMM", DBNull.Value);
                }
                cmd.Parameters.AddWithValue("@DEPTNO", emp.DeptNo);

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(decimal? empNo)
        {
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText = "sp_DeleteEmp";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empNo", empNo);
                cmd.ExecuteNonQuery();
            }
        }

        public void Edit(Emp emp)
        {
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText = "sp_UpdateEmp";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EMPNO", emp.EmpNo);
                cmd.Parameters.AddWithValue("@ENAME", emp.EmpName);
                cmd.Parameters.AddWithValue("@JOB", emp.Job);

                if (emp.Mgr != null)
                {
                    cmd.Parameters.AddWithValue("@MGR", emp.Mgr);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@MGR", DBNull.Value);
                }
                
                cmd.Parameters.AddWithValue("@HIREDATE", emp.HireDate);
                cmd.Parameters.AddWithValue("@SAL", emp.Sal);

                if (emp.Comm != null)
                {
                    cmd.Parameters.AddWithValue("@COMM", emp.Comm);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@COMM", DBNull.Value);
                }
                cmd.Parameters.AddWithValue("@DEPTNO", emp.DeptNo);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
