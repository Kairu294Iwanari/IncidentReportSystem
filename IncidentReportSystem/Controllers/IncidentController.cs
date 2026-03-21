using IncidentReportSystem.Models.Context;
using IncidentReportSystem.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IncidentReportingSystem.Controllers
{
    public class IncidentController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoginPage()
        {
            return View();
        }
        public ActionResult RegistrationPage()
        {
            return View();
        }
        public ActionResult ResidentDashboard()
        {
            return View();
        }
        public ActionResult OfficialDashboard()
        {
            return View();
        }
        public ActionResult AdminDashboard()
        {
            return View();
        }

        public string UpsertUsers()
        {
            try
            {
                using (var connect = new IncidentReportContext())
                {
                    var getData = connect.tbl_users.Where(x => x.UserID == 2).FirstOrDefault();

                    if (getData != null)
                    {
                        getData.LastLoginAt = DateTime.Now;
                        connect.SaveChanges();
                    }
                    else
                    {

                        var usersData = new tbl_users_model()
                        {
                            FirstName = "Kairu",
                            MiddleName = "Sargan",
                            LastName = "Iwanari",
                            Username = "KaIwanari",
                            PasswordHash = "June92003",
                            PhoneNumber = 1234567890,
                            Address = "Manila",
                            RoleID = 1,
                            AccountStatusID = 1,
                            CreatedAt = DateTime.Now,
                            LastLoginAt = DateTime.Now
                        };
                        connect.tbl_users.Add(usersData);
                        connect.SaveChanges();

                        return "Success";

                    }
                }
            }catch(Exception ex)
            {
                return ErrorHandling(ex.Message, ex.StackTrace, ex.InnerException.ToString());
            }
        }
        public string ErrorHandling(string eMessage, string eStackTrace, string eInnerException)
        {
            var errorMessage = $"Error has been encountered : {eMessage} | {eStackTrace} | {eInnerException}";
            return "Unable to process your request at this time.";
        }
    }
}