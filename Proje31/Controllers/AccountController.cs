using Microsoft.AspNetCore.Mvc;
using Proje31.Services;
using System.Data.SqlClient;

namespace Proje31.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        string cs = "Data Source=DESKTOP-INPQ5Q1\\MSSQLSERVER02;Initial Catalog=MyFirstSemiActualProject;Integrated Security=SSPI";
        private AccountServices accountServices;

        public AccountController(AccountServices _accountServices)
        {
            accountServices = _accountServices;
        }

        [Route("")]
        [Route("~/")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("index")]
        public IActionResult Login(string username, string pw, int ResponseCode, string firstName, string surname)
        {
            try
            {
                using (SqlConnection Con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("sp_userLogin", Con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    List<SqlParameter> list = new List<SqlParameter>();
                    list.Add(new SqlParameter("@username", username));
                    list.Add(new SqlParameter("@pw", pw));
                    Con.Open();
                    cmd.Parameters.AddRange(list.ToArray<SqlParameter>());
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ResponseCode = Convert.ToInt32(dr["responsecode"]);
                        if (ResponseCode == 100)
                        {
                            firstName = dr["firstName"].ToString();
                            surname = dr["surname"].ToString();
                            HttpContext.Session.SetString("username", username);
                            HttpContext.Session.SetString("firstName", firstName);
                            HttpContext.Session.SetString("surname", surname);
                            return RedirectToAction("welcome");
                        }
                        else
                        {
                            ViewBag.msg = "Kullanıcı Adı veya Şifre Yanlış";
                            return View("Index");
                        }
                    }
                    return View();
                }//layout 
            }
            catch
            {
                return View("Error");
            }

        }
        [Route("Welcome")]
        public IActionResult Welcome()
        {
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.firstName = HttpContext.Session.GetString("firstName");
            ViewBag.surname = HttpContext.Session.GetString("surname");
            return View("Welcome");
        }
        [Route("deneme")]
        public IActionResult deneme()
        {
            
            return View();
        }
        [Route("regis")]
        public IActionResult Regis(string username, string pw, int ResponseCode, string posta, string ad, string soyad)
        {
            using (SqlConnection Con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("sp_userSave", Con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                List<SqlParameter> list = new List<SqlParameter>();
                list.Add(new SqlParameter("@username", username));
                list.Add(new SqlParameter("@pw", pw));
                list.Add(new SqlParameter("@eMail", posta));
                list.Add(new SqlParameter("@firstName", ad));
                list.Add(new SqlParameter("@surname", soyad));

                Con.Open();
                cmd.Parameters.AddRange(list.ToArray<SqlParameter>());
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ResponseCode = Convert.ToInt32(dr["responsecode"]);
                    if (ResponseCode == 100)
                    {
                        return RedirectToAction("index");
                    }
                    else
                    {
                        ViewBag.msg = "hata";
                        return View("deneme");
                    }
                }
                return View();
            }

        }
        [Route("kayıtol")]
        public IActionResult kayıtol()
        {
            
            return RedirectToAction("deneme");
        }
        [Route("kredi")]
        public IActionResult kredi()
        {

            return View();
        }
        [Route("bos")]  
        public IActionResult bos() 
        {

            return View();
        }
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.GetString("username");
            return View("Index");
        }


    }
}
