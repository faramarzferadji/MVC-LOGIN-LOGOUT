using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using LogingModel.Models;
using System.Configuration;

namespace LogingModel.Controllers
{
    public class CustomerController : Controller
    {
        string connectionstrings = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AdminDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(User user)
        {
            using (SqlConnection con = new SqlConnection(connectionstrings))
            {
                con.Open();
                string query = "Select Password, Username from ADTable where Password = @Password and Username=@Username";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@Username", user.Username);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Session["UPassword"] = user.Password.ToString();
                    Session["Username"] = user.Username.ToString();
                    return RedirectToAction("Wellcome");
                }
                else
                {
                    ViewData["Massege"] = "Please enter the valid data  ";
                }
            }
                return View();
        }
        public ActionResult Wellcome()
        {
            return View();
        }
    }
}