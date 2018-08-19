using CFSIS.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFSIS.Server.DataAccess
{
    public class DistrictsDataAccessLayer
    {
        CFSISContext db = new CFSISContext();

        //To Get all districtss details   
        public IEnumerable<Districts> GetAllDistricts()
        {
            try
            {
                return db.Districts.ToList();
            }
            catch
            {
                throw;
            }
        }

        //To Add new district record     
        public void AddDistricts(Districts district)
        {
            try
            {
                db.Districts.Add(district);
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //To Update the records of a particluar district    
        public void UpdateDistricts(Districts district)
        {
            try
            {
                db.Entry(district).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //Get the details of a particular district    
        public Districts GetDistrictsData(int id)
        {
            try
            {
                Districts district = db.Districts.Find(id);
                return district;
            }
            catch
            {
                throw;
            }
        }

        //To Delete the record of a particular district    
        public void DeleteDistricts(int id)
        {
            try
            {
                Districts emp = db.Districts.Find(id);
                db.Districts.Remove(emp);
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
    }
}
