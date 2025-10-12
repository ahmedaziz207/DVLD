using DVLD_Data_Access;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsLDL_Application
    {
        public int LocalDrivingLicenseApplicationID { get; set; }
        public int ApplicationID { get; set; }
        public int LicenseClassID { get; set; }

        public clsLDL_Application()
        {
            this.LocalDrivingLicenseApplicationID = -1;
            this.ApplicationID = -1;
            this.LicenseClassID = -1;
        }

        private clsLDL_Application(int LocalDrivingLicenseApplicationID,int ApplicationID,int LicenseClassID)
        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.ApplicationID = ApplicationID;
            this.LicenseClassID = LicenseClassID;
        }

        public static DataTable GetAll_LDL_Applications_View()
        {
            return clsLDL_ApplicationDataAccess.GetAll_LDL_Applications_View();
        }

        private bool _AddNew_LDL_Application()
        {
            this.LocalDrivingLicenseApplicationID = clsLDL_ApplicationDataAccess.AddNew_LDL_Application(this.ApplicationID, this.LicenseClassID);
            return (this.LocalDrivingLicenseApplicationID != -1);
        }

        public bool Save()
        {
            return _AddNew_LDL_Application();
        }

        public static bool IsApplicationWithThisLicenseClassExist(string NationalNo, string LicenseClass)
        {
            return clsLDL_ApplicationDataAccess.IsApplicationWithThisLicenseClassExist(NationalNo, LicenseClass);   
        }

        public static clsLDL_Application GetLDL_Application(int LocalDrivingLicenseApplicationID)
        {
            int ApplicationID = -1;
            int LicenseClassID = -1;

            if (clsLDL_ApplicationDataAccess.GetLDL_Application(LocalDrivingLicenseApplicationID, ref ApplicationID, ref LicenseClassID))
            {
                return new clsLDL_Application(LocalDrivingLicenseApplicationID, ApplicationID, LicenseClassID);
            }
            else
            {
                return null;
            }
        }

        public static bool DeleteLDL_Application(int LDL_ApplicationID)
        {
            return clsLDL_ApplicationDataAccess.DeleteLDL_Application(LDL_ApplicationID);
        }

        public bool DoesAttendTestType(int TestTypeID)
        {
            return clsLDL_ApplicationDataAccess.DoesAttendTestType(this.LocalDrivingLicenseApplicationID, TestTypeID);
        }
    }
}
