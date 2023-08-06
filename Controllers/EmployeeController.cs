using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using OnlineDoctorAppoinment.Models;
using System.Data;

namespace OnlineDoctorAppoinment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        [Route("addUser")]
        public JsonResult addUser(Employee e)
        {
            string query = @"INSERT INTO [dbo].[comit.user]
           ([first_name]
           ,[last_name]
           ,[phone]
           ,[aboutus]
           ,[email])
     VALUES ('"+ e.FirstName+"','"+e.LastName+"','"+e.Phone+"','"+e.AboutUs+"','"+e.Email+"') ";
            string reult = string.Empty;
            try
            {
               
                DataTable dataTable = new DataTable();
                string sqldatasource = _configuration.GetConnectionString("EmployeeConnection").ToString();
                using (SqlConnection conn = new SqlConnection(sqldatasource))
                {

                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        dataTable.Load(reader);
                        reader.Close();
                        conn.Close();

                    }

                }
                reult = "Successfully Added";
                return new JsonResult(reult);
            }
            catch (Exception ex)
            {
                reult= ex.Message;
                return    new JsonResult(reult);
            }
            

        }
    }
}
