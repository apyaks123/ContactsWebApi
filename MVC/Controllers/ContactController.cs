using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Models;
using System.Net.Http;
using PagedList.Mvc;
using PagedList;
using Korzh.EasyQuery.Linq;
using WebApplication7.Models;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace MVC.Controllers
{




    public class ContactController : Controller
    {
        

        // GET: Contact
        public ActionResult Index(int? i, string sorts)
        {
           


            IEnumerable<mvcContactModel> contList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Contact").Result;
            contList = response.Content.ReadAsAsync<IEnumerable<mvcContactModel>>().Result;


            //sorting
            var zzz = contList.AsQueryable();
            ViewData["NameOrder"] = String.IsNullOrEmpty(sorts) ? "name_desc" : "";
            switch (sorts)
            {
                case "name_desc":
                    zzz = zzz.OrderByDescending(a => a.firstName);
                    break;
                default:
                    zzz = zzz.OrderBy(a => a.firstName);
                    break;

            }
            



           

            return View(zzz.ToPagedList(i ?? 1, 5));
        }

        

        

        [HttpGet]
        public ActionResult Search(string emp, int? i, string sorts)
        {
            
            var connectionString = ConfigurationManager.ConnectionStrings["WingtipToys"].ConnectionString;
            SqlConnection SqlConn = new SqlConnection(connectionString);
            string queryString = "SELECT *  FROM [dbo].[Table_1] where firstName like '" + emp + "'";
            SqlCommand sqlcomm = new SqlCommand(queryString, SqlConn);
            SqlConn.Open();
            SqlDataAdapter sq = new SqlDataAdapter(sqlcomm);

            DataSet ds = new DataSet();
            sq.Fill(ds);
            List<mvcContactModel> ec = new List<mvcContactModel>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                ec.Add(new mvcContactModel
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    firstName = Convert.ToString(dr["firstName"]),
                    lastName = Convert.ToString(dr["lastName"]),
                    Phone = Convert.ToString(dr["Phone"]),
                    Email = Convert.ToString(dr["Email"])


                }
                    );
            }

            

            SqlConn.Close();
            ModelState.Clear();

            


            //sorting
            var zzz = ec.AsQueryable();


            

            ViewData["NameOrder"] = String.IsNullOrEmpty(sorts) ? "name_desc" : "";
            switch (sorts)
            {
                case "name_desc":
                    zzz = zzz.OrderByDescending(a => a.lastName);
                    break;
                default:
                    zzz = zzz.OrderBy(a => a.lastName);
                    break;

            }
            
            
            if (ec.Count() == 0)
            {
                TempData["SuccessMessage"] = "Search Page Accessed or Data Not Found!";

            }
            else if (ec.Count() >= 1)
            {
                TempData["SuccessMessage"] = "Search Successful.";

            }



            return View(zzz.ToPagedList(i ?? 1, 10));

        }


       /* [HttpGet]

        public ActionResult Details(int id)
        {

            if (id == 0)
            {
                return View(new mvcContactModel());
            }

            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Contact/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcContactModel>().Result);

                TempData["SuccessMessage"] = "Details Successful.";
                

            }
            return RedirectToAction("Details");

        }
*/


        public ActionResult AddOrEdit(int id = 0)
        {


            if (id == 0)
            {
                return View(new mvcContactModel());
            }

            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Contact/"+id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcContactModel>().Result);


            }

        }

        [HttpPost]
        public ActionResult AddOrEdit(mvcContactModel cont)
        {

            if (cont.Id == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Contact", cont).Result;
                TempData["SuccessMessage"] = "Save Successful.";
            }
            else
            {

                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Contact/"+cont.Id,cont).Result;
                TempData["SuccessMessage"] = "Update Successful.";

            }
                return RedirectToAction("Index");


            
        }


        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Contact/"+id.ToString()).Result;
            TempData["SuccessMessage"] = "Remove/Delete Successful.";
            return RedirectToAction("Index");

        }


       


    }
}