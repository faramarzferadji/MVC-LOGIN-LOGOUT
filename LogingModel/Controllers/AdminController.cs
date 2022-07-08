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
   
    public class AdminController : Controller
    {
        string connectionstrings = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AdminDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        // GET: Admin
        public ActionResult Index()
        {
            DataTable ADTable = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionstrings))
            {
                con.Open();
                string query = "Select * from ADTable ";
                SqlDataAdapter sqlData = new SqlDataAdapter(query, con);
                sqlData.Fill(ADTable);
            }

                return View(ADTable);
        }
        public ActionResult Search(string sname)
        {
            using (SqlConnection con = new SqlConnection(connectionstrings))
            {
                con.Open();
                string query = "Select * from ADTable where Username like '%"+sname+"%'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter sqlData = new SqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                sqlData.Fill(dataSet);
                List<Admin> ad = new List<Admin>();
                foreach (DataRow item in dataSet.Tables[0].Rows)
                {
                    ad.Add(new Admin
                    {
                        Password=Convert.ToInt32(item["Password"]),
                        Username=Convert.ToString(item["Username"])

                    });

                }
                con.Close();
                ModelState.Clear();
                return View(ad);


            }

               
        }


        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult Create(Admin admin)
        {
            using(SqlConnection con= new SqlConnection(connectionstrings))
            {
                con.Open();
                string query = "Insert into ADTable values(@Password,@Username)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Password", admin.Password);
                cmd.Parameters.AddWithValue("@Username", admin.Username);
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionstrings))
            {
                con.Open();
                string query = "Delete From ADTable where id=@Password";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Password",id);               
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        
    }
}
