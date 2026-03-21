using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IncidentReportSystem.Models.Tables
{
    public class tbl_users_model
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
        public int RoleID { get; set; }
        public int AccountStatusID { get; set; }
        public DateTime CreatedAt {  get; set; }
        public DateTime LastLoginAt { get; set; }

    }
}