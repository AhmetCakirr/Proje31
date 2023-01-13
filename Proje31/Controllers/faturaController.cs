using Microsoft.AspNetCore.Mvc;
using Proje31.Services;
using System.Data.SqlClient;

namespace Proje31.Controllers
{
    [Route("fatura")]
    public class faturaController : Controller
    {
        string cs = "Data Source=DESKTOP-INPQ5Q1\\MSSQLSERVER02;Initial Catalog=MyFirstSemiActualProject;Integrated Security=SSPI";
       
        
        
        [Route("fatura")]
        public IActionResult fatura()
        {
            ViewBag.id = HttpContext.Session.GetInt32("id");
            ViewBag.aboneNo = HttpContext.Session.GetString("aboneNo");
            ViewBag.adsoyad = HttpContext.Session.GetString("adsoyad");
            ViewBag.tutar = HttpContext.Session.GetInt32("tutar");
            return View();
        }

        [HttpPost]
        [Route("fatura")]
        public IActionResult fatura(string aboneNo, int ResponseCode, int id, string adsoyad, int tutar)
        {
            try
            {
                using (SqlConnection Con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("sp_faturalistele", Con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    List<SqlParameter> list = new List<SqlParameter>();
                    list.Add(new SqlParameter("@aboneNo", aboneNo));
                    Con.Open();
                    cmd.Parameters.AddRange(list.ToArray<SqlParameter>());
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ResponseCode = Convert.ToInt32(dr["responsecode"]);
                        if (ResponseCode == 100)
                        {
                            id = Convert.ToInt32(dr["id"]);
                            aboneNo = (string)dr["aboneNo"];
                            adsoyad = (string)dr["adsoyad"];
                            tutar = Convert.ToInt32(dr["tutar"]);

                            HttpContext.Session.SetInt32("id", id);
                            HttpContext.Session.SetString("aboneNo", aboneNo );
                            HttpContext.Session.SetString("adsoyad", adsoyad );
                            HttpContext.Session.SetInt32("tutar", tutar);
                            return RedirectToAction("fatura");
                        }
                        else
                        {
                            ViewBag.msg = "Abone numarasına göre biri bulunamadı";
                            return View("fatura");
                        }
                    }
                    Con.Close();
                    return View();
                }//layout 
            }
            catch
            {
                return View("Error");
            }

        }
        [HttpPost]
        [Route("faturaode")]
        public IActionResult faturasil(string aboneNo, int ResponseCode, int id, string adsoyad, int tutar)
        {
            try
            {
                using (SqlConnection Con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("sp_faturasil", Con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    List<SqlParameter> list = new List<SqlParameter>();
                    list.Add(new SqlParameter("@aboneNo", aboneNo));
                    Con.Open();
                    cmd.Parameters.AddRange(list.ToArray<SqlParameter>());
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ResponseCode = Convert.ToInt32(dr["responsecode"]);
                        if (ResponseCode == 100)
                        {                           
                            return RedirectToAction("fatura");
                        }
                        else
                        {
                            ViewBag.msg = "Abone numarasına göre biri bulunamadı";
                            return View("fatura");
                        }
                    }
                    Con.Close();
                    return View();
                }//layout 
            }
            catch
            {
                return View("Error");
            }

        }
        //[Route("faturalistele")]
        //public IActionResult faturaListele()
        //{
        //    ViewBag.id = HttpContext.Session.GetInt32("id");
        //    ViewBag.aboneNo = HttpContext.Session.GetString("aboneNo");
        //    ViewBag.adsoyad = HttpContext.Session.GetString("adsoyad");
        //    ViewBag.tutar = HttpContext.Session.GetInt32("tutar");
        //    return View("faturalistele");

        //}
    }
}
