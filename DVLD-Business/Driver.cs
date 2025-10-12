using DVLD_Data_Access;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsDriver
    {
        public int DriverID { get; set; }
        public int PersonID { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }


        public clsDriver() 
        {
            this.DriverID = -1;
            this.PersonID = -1;
            this.CreatedByUserID = -1;
            this.CreatedDate = DateTime.Now;
        }


        private bool _AddNewDriver()
        {
            this.DriverID = clsDriverDataAccess.AddNewDriver(this.PersonID,this.CreatedByUserID,this.CreatedDate);
            return (this.DriverID != -1);

        }

        public bool Save()
        {
            return _AddNewDriver();
        }

        public static bool DoesDriverHaveThisLicense(int PersonID,string LicenseClassName)
        {
            return clsDriverDataAccess.DoesDriverHaveThisLicense(PersonID, LicenseClassName);
        }


        public static int ReturnDriverIDIsExist(int PersonID)
        {
            return clsDriverDataAccess.ReturnDriverIDIsExist(PersonID);
        }

        public static DataTable GetDrivers()
        {
            return clsDriverDataAccess.GetDrivers();

        }
    }
}
