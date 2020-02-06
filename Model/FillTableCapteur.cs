using EO.Internal;
using Intuit.Ipp.Data;
using Sitecore.FakeDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet4.Model
{
    public class FillTableCapteur
    {
        public static void fillup()
        {
            Employee objEmp = new Employee();
            // fields to be insert
            objEmp. = "John";
            objEmp.EmployeeAge = 21;
            objEmp.EmployeeDesc = "Designer";
            objEmp.EmployeeAddress = "Northampton";
            objDataContext.Employees.InsertOnSubmit(objEmp);
            // executes the commands to implement the changes to the database
            objDataContext.SubmitChanges();

        }



}
}
