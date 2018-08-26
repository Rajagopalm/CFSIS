﻿using CFSIS.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFSIS.Server.DataAccess
{
    public class EmployeeDataAccessLayer
    {
        CFSISContext db = new CFSISContext();

        public EmployeeDataAccessLayer()
        { }

        public EmployeeDataAccessLayer(CFSISContext DBcontext)
        {
            db = DBcontext;
        }

        //To Get all employees details   
        public IEnumerable<Employee> GetAllEmployees()
        {
            try
            {
                return db.Employee.ToList();
            }
            catch
            {
                throw;
            }
        }

        //To Add new employee record     
        public void AddEmployee(Employee employee)
        {
            try
            {
                db.Employee.Add(employee);
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //To Update the records of a particluar employee    
        public void UpdateEmployee(Employee employee)
        {
            try
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //Get the details of a particular employee    
        public Employee GetEmployeeData(int id)
        {
            try
            {
                Employee employee = db.Employee.Find(id);
                return employee;
            }
            catch
            {
                throw;
            }
        }

        //To Delete the record of a particular employee    
        public void DeleteEmployee(int id)
        {
            try
            {
                Employee emp = db.Employee.Find(id);
                db.Employee.Remove(emp);
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
    }
}
