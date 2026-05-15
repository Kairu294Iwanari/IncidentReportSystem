using IncidentReportSystem.Models;
using IncidentReportSystem.Models.Context;
using IncidentReportSystem.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace IncidentReportingSystem.Controllers
{
    public class IncidentController : Controller
    {
        public ActionResult Index() { return View(); }
        public ActionResult LoginPage() { return View(); }
        public ActionResult RegistrationPage() { return View(); }
        public ActionResult ResidentDashboard() { return View(); }
        public ActionResult OfficialDashboard() { return View(); }
        public ActionResult AdminDashboard() { return View(); }

        [HttpPost]
        public JsonResult LoginUser(tbl_users_model loginData)
        {
            try
            {
                using (var connect = new IncidentReportContext())
                {
                    var user = connect.tbl_users.FirstOrDefault(x => x.Username == loginData.Username && x.PasswordHash == loginData.PasswordHash);
                    if (user != null)
                    {
                        if (user.AccountStatusID != 1) return Json(new { success = false, message = "Account suspended." });
                        user.LastLoginAt = DateTime.Now;
                        connect.SaveChanges();
                        user.PasswordHash = null;
                        return Json(new { success = true, userData = user });
                    }
                    return Json(new { success = false, message = "Invalid Username or Password." });
                }
            }
            catch (Exception ex) { return Json(new { success = false, message = $"System Error: {ex.Message}" }); }
        }

        [HttpPost]
        public string UpsertUsers(tbl_users_model userData)
        {
            try
            {
                using (var connect = new IncidentReportContext())
                {
                    if (connect.tbl_users.Any(x => x.Username == userData.Username)) return "Username is taken.";
                    userData.CreatedAt = DateTime.Now;
                    userData.LastLoginAt = DateTime.Now;
                    userData.AccountStatusID = 1;
                    connect.tbl_users.Add(userData);
                    connect.SaveChanges();
                    return "Success";
                }
            }
            catch (Exception ex) { return $"System Error: {ex.Message}"; }
        }

        // ==========================================
        // ADMIN: MANAGE USERS LOGIC
        // ==========================================
        [HttpGet]
        public JsonResult GetAllUsersList()
        {
            try
            {
                using (var connect = new IncidentReportContext())
                {
                    var users = (from u in connect.tbl_users
                                 join r in connect.tbl_roles on u.RoleID equals r.RoleID
                                 select new
                                 {
                                     u.UserID,
                                     u.FirstName,
                                     u.LastName,
                                     u.Username,
                                     u.PhoneNumber,
                                     u.Address,
                                     u.RoleID,
                                     RoleName = r.RoleName
                                 }).ToList();
                    return Json(users, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) { return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet); }
        }

        [HttpPost]
        public string AdminUpdateUser(tbl_users_model userData)
        {
            try
            {
                using (var connect = new IncidentReportContext())
                {
                    var user = connect.tbl_users.FirstOrDefault(x => x.UserID == userData.UserID);
                    if (user != null)
                    {
                        user.FirstName = userData.FirstName;
                        user.LastName = userData.LastName;
                        user.PhoneNumber = userData.PhoneNumber;
                        user.Address = userData.Address;
                        user.RoleID = userData.RoleID;
                        connect.SaveChanges();
                        return "Success";
                    }
                    return "User not found.";
                }
            }
            catch (Exception ex) { return $"System Error: {ex.Message}"; }
        }

        [HttpPost]
        public string AdminDeleteUser(tbl_users_model userData)
        {
            try
            {
                using (var connect = new IncidentReportContext())
                {
                    var user = connect.tbl_users.FirstOrDefault(x => x.UserID == userData.UserID);
                    if (user != null)
                    {
                        connect.tbl_users.Remove(user);
                        connect.SaveChanges();
                        return "Success";
                    }
                    return "User not found.";
                }
            }
            catch (Exception ex) { return $"Action Blocked: This user has existing incident records in the database. (Error: {ex.Message})"; }
        }


        // ==========================================
        // DYNAMIC CARDS & GENERAL DATA
        // ==========================================
        [HttpGet]
        public JsonResult GetCardsStatus()
        {
            try
            {
                using (var connect = new IncidentReportContext())
                {
                    var getStatus = connect.tbl_incident_statuses.Select(x => x).ToList();
                    var getIncidentStatus = (from incidents in connect.tbl_incidents
                                             join status in connect.tbl_incident_statuses on incidents.StatusID equals status.StatusID
                                             select new { incidents, status }).ToList();

                    List<StatusCardModel> cardStatusData = new List<StatusCardModel>();
                    foreach (var stat in getStatus)
                    {
                        var statData = getIncidentStatus.Where(x => x.status.StatusID == stat.StatusID).Count();
                        cardStatusData.Add(new StatusCardModel() { StatusCount = statData, StatusDesc = stat.StatusName });
                    }
                    return Json(cardStatusData, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) { return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet); }
        }

        [HttpGet]
        public JsonResult GetDashboardData()
        {
            try
            {
                using (var connect = new IncidentReportContext())
                {
                    var dashboardData = (from incident in connect.tbl_incidents
                                         join category in connect.tbl_incident_categories on incident.CategoryID equals category.CategoryID
                                         join status in connect.tbl_incident_statuses on incident.StatusID equals status.StatusID
                                         join user in connect.tbl_users on incident.ResidentID equals user.UserID
                                         select new { incident, category, status, user }).ToList();
                    return Json(dashboardData, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) { return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet); }
        }

        [HttpGet]
        public JsonResult GetDropdownData()
        {
            try
            {
                using (var connect = new IncidentReportContext())
                {
                    var categories = connect.tbl_incident_categories.Select(x => new { x.CategoryID, x.CategoryName }).ToList();
                    var priorities = connect.tbl_priority_levels.Select(x => new { x.PriorityID, x.PriorityName }).ToList();
                    return Json(new { categories, priorities }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) { return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet); }
        }

        [HttpGet]
        public JsonResult GetStatusList()
        {
            try
            {
                using (var connect = new IncidentReportContext())
                {
                    var statuses = connect.tbl_incident_statuses.Select(x => new { x.StatusID, x.StatusName }).ToList();
                    return Json(statuses, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) { return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet); }
        }

        [HttpGet]
        public JsonResult GetUserStats()
        {
            try
            {
                using (var connect = new IncidentReportContext())
                {
                    var totalUsers = connect.tbl_users.Count();
                    var residentsCount = connect.tbl_users.Count(x => x.RoleID == 1);
                    var officialsCount = connect.tbl_users.Count(x => x.RoleID == 2);
                    return Json(new { Total = totalUsers, Residents = residentsCount, Officials = officialsCount }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) { return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet); }
        }

        // ==========================================
        // DYNAMIC CHARTS 
        // ==========================================
        [HttpGet]
        public JsonResult GetPieGraph()
        {
            try
            {
                using (var connect = new IncidentReportContext())
                {
                    var roles = connect.tbl_roles.ToList();

                    PieChartModel pie_chart = new PieChartModel();
                    pie_chart.labels = new List<string>();
                    pie_chart.data = new List<int>();

                    foreach (var role in roles)
                    {
                        int count = connect.tbl_users.Count(x => x.RoleID == role.RoleID);
                        if (count > 0)
                        {
                            pie_chart.labels.Add(role.RoleName);
                            pie_chart.data.Add(count);
                        }
                    }

                    return Json(pie_chart, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) { return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet); }
        }

        [HttpGet]
        public JsonResult GetLineGraph()
        {
            try
            {
                using (var connect = new IncidentReportContext())
                {
                    string[] month_array = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
                    var status_list = connect.tbl_incident_statuses.ToList();
                    List<int> month_list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

                    ChartModel line_chart = new ChartModel();
                    line_chart.labels = new List<string>();
                    line_chart.series = new List<string>();
                    line_chart.data = new List<List<int>>();

                    foreach (var month in month_list)
                    {
                        line_chart.labels.Add(month_array[month - 1]);
                    }

                    foreach (var status in status_list)
                    {
                        line_chart.series.Add(status.StatusName);
                        List<int> temp_data = new List<int>();

                        foreach (var month in month_list)
                        {
                            int count = connect.tbl_incidents.Count(x => x.StatusID == status.StatusID && x.CreatedAt.Month == month);
                            temp_data.Add(count);
                        }
                        line_chart.data.Add(temp_data);
                    }

                    return Json(line_chart, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) { return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet); }
        }

        [HttpGet]
        public JsonResult GetBarGraph()
        {
            try
            {
                using (var connect = new IncidentReportContext())
                {
                    string[] month_array = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
                    var category_list = connect.tbl_incident_categories.ToList();
                    List<int> month_list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

                    ChartModel bar_chart = new ChartModel();
                    bar_chart.labels = new List<string>();
                    bar_chart.series = new List<string>();
                    bar_chart.data = new List<List<int>>();

                    foreach (var month in month_list)
                    {
                        bar_chart.labels.Add(month_array[month - 1]);
                    }

                    foreach (var category in category_list)
                    {
                        bar_chart.series.Add(category.CategoryName);
                        List<int> temp_data = new List<int>();

                        foreach (var month in month_list)
                        {
                            int count = connect.tbl_incidents.Count(x => x.CategoryID == category.CategoryID && x.CreatedAt.Month == month);
                            temp_data.Add(count);
                        }
                        bar_chart.data.Add(temp_data);
                    }

                    return Json(bar_chart, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) { return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet); }
        }

        // ==========================================
        [HttpPost]
        public string SubmitIncidentReport(tbl_incidents_model incidentData)
        {
            try
            {
                using (var connect = new IncidentReportContext())
                {
                    incidentData.CreatedAt = DateTime.Now;
                    incidentData.StatusID = 1;
                    connect.tbl_incidents.Add(incidentData);
                    connect.SaveChanges();
                    return "Success";
                }
            }
            catch (Exception ex) { return $"System Error: {ex.Message}"; }
        }

        [HttpPost]
        public string UpdateIncidentStatus(tbl_incidents_model updateData)
        {
            try
            {
                using (var connect = new IncidentReportContext())
                {
                    var incident = connect.tbl_incidents.FirstOrDefault(x => x.IncidentID == updateData.IncidentID);
                    if (incident != null)
                    {
                        incident.StatusID = updateData.StatusID;
                        incident.UpdatedAt = DateTime.Now;
                        connect.SaveChanges();
                        return "Success";
                    }
                    return "Incident not found.";
                }
            }
            catch (Exception ex) { return $"System Error: {ex.Message}"; }
        }
    }
}