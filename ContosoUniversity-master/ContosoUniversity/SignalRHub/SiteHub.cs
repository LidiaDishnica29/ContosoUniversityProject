using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels;
using Microsoft.AspNet.SignalR;

namespace ContosoUniversity.SignalRHub
{
    public class SiteHub : Hub
    {
        private SchoolContext db = new SchoolContext();

        // get the chat for the report
        public void Read()
        {

            var data = from student in db.Students
                       group student by student.EnrollmentDate into dateGroup
                       select new EnrollmentDateGroup()
                       {
                           EnrollmentDate = dateGroup.Key,
                           StudentCount = dateGroup.Count()
                       };
            foreach (var a in data)
            {
                Clients.All.readMessageToPage(a.EnrollmentDate, a.StudentCount);
            }
        }         
    }
}