using CFSIS.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFSIS.Server.DataAccess
{
    public class SubDistrictsDataAccessLayer
    {
        CFSISContext db = new CFSISContext();

        //To Get all districts details   
        public IEnumerable<SubDistricts> GetAllSubDistricts()
        {
            try
            {
                return db.SubDistricts.ToList();
            }
            catch
            {
                throw;
            }
        }

        //To Add new subdistrict record     
        public void AddSubDistricts(SubDistricts subdistrict)
        {
            try
            {
                db.SubDistricts.Add(subdistrict);
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //To Update the records of a particluar subdistrict    
        public void UpdateSubDistricts(SubDistricts subdistrict)
        {
            try
            {
                db.Entry(subdistrict).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //Get the details of a particular subdistrict    
        public SubDistricts GetSubDistrictsData(int id)
        {
            try
            {
                SubDistricts subdistrict = db.SubDistricts.Find(id);
                return subdistrict;
            }
            catch
            {
                throw;
            }
        }

        //To Delete the record of a particular subdistrict    
        public void DeleteSubDistricts(int id)
        {
            try
            {
                SubDistricts emp = db.SubDistricts.Find(id);
                db.SubDistricts.Remove(emp);
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
    }
}
