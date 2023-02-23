using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASS_4
{
    class EmployeeDao
    {
        string strConnection;

        public string getConnectionString()
        {
            string strConnection = @"server=Admin;database=BookStores;uid=sa;pwd=12345";
            return strConnection;
        }
        public EmployeeDao()
        {
            strConnection = getConnectionString();
        }

        public Employee getEmployee(string empID, string empPassword)
        {
            Employee employee = null;
            try
            {
                string SQL = "select * from Employee " +
                    "where empID=@empID and empPassword=@empPassword";
                SqlConnection cnn = new SqlConnection(strConnection);
                SqlCommand cmd = new SqlCommand(SQL, cnn);
                cmd.Parameters.AddWithValue("@empID", empID);
                cmd.Parameters.AddWithValue("@empPassword", empPassword);
                cnn.Open();
                SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (rd.HasRows)
                {
                    if (rd.Read())
                    {
                        bool empRole = Boolean.Parse(rd["empRole"].ToString());
                        employee = new Employee(empID, empPassword, empRole);
                    }
                }
            } catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return employee;
        }

        public bool updateEmployee(Employee employee)
        {
            SqlConnection cnn = new SqlConnection(strConnection);
            string SQL = "Update Employee set empPassword=@empPassword " +
                "where empID=@empID";
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@empID", employee.empID);
            cmd.Parameters.AddWithValue("@empPassword", employee.empPassword);
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            int count = cmd.ExecuteNonQuery();
            return (count > 0);
        }

        //public List<UserInfo> GetUserList()
        //{
        //    List<UserInfo> data = new List<UserInfo>();
        //    string SQL = "select * from UserInfo";
        //    SqlConnection cnn = new SqlConnection(strConnection);
        //    SqlCommand cmd = new SqlCommand(SQL, cnn);
        //    cnn.Open();
        //    SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //    if (rd.HasRows)
        //    {
        //        while (rd.Read())
        //        {
        //            dynamic u = new UserInfo()
        //            {
        //                UserName = rd["UserName"].ToString(),
        //                Password = rd["Password"].ToString(),
        //                BirthDate = DateTime.Parse(rd["Birthdate"].ToString()),
        //                Address = rd["Address"].ToString(),
        //                Email = rd["Email"].ToString()
        //            };
        //            data.Add(u);
        //        }
        //    }
        //    return data;
        //}
    }
}
