﻿using DAL.Interfaces;
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

        public IEnumerable<Emp> GetEmpsHierarchy()
        {
            var emps = new List<Emp>();

            using (SqlConnection connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = "sp_GetHierarchy";
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
    }
}
