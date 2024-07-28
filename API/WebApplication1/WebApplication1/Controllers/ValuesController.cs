using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoAppControler : ControllerBase
    {
        private IConfiguration _configuration;
        private object newNotes;

        public TodoAppControler(IConfiguration configuration)

        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetNotes")]
        public JsonResult GetNotes()
        {
            string query = "Select * From dbo.Notes";
            DataTable table = new();
            string sqlDataresource = _configuration.GetConnectionString("TotoAppDBCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new(sqlDataresource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query,myCon))
                {
                    myReader=myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();


                }

            }
            return new JsonResult(table);

        }

        [HttpPost]
        [Route("AddNotes")]
        public JsonResult AddNotes([FromForm] string newNotes)
        {
            string query = "insert into dbo.Notes (description) values(@newNotes)";
            DataTable table = new DataTable();
            string sqlDataresource = _configuration.GetConnectionString("TotoAppDBCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataresource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@newNotes", newNotes);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();


                }

            }
            return new JsonResult("Added Successfully");

        }
        [HttpPut]
        [Route("UpdateNotes")]
        public JsonResult UpdateNotes(string updateNotes, int id)
        {
            string query = "update dbo.notes set description=@updateNotes where id=@id";
            DataTable table = new DataTable();
            string sqlDataresource = _configuration.GetConnectionString("TotoAppDBCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new(sqlDataresource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@updateNotes", updateNotes);
                    myCommand.Parameters.AddWithValue("@id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();


                }

            }
            return new JsonResult("Data Update Successfully");

        }

        [HttpDelete]
        [Route("DeleteNotes")]
        public JsonResult DeleteNotes(int id)
        {
            string query = "Delete from dbo.notes where id=@id";
            DataTable table = new DataTable();
            string sqlDataresource = _configuration.GetConnectionString("TotoAppDBCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new(sqlDataresource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();


                }

            }
            return new JsonResult("Data Deleted Successfully");

        }
    }
}
