using DVLD_Data_Access;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsInternationalLicense
    {
        public enum enMode { AddNew, Update }
        public enMode Mode = enMode.AddNew;
        public int InternationalLicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int IssuedUsingLocalLicenseID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public int CreatedByUserID { get; set; }


        public clsInternationalLicense()
        {
            this.InternationalLicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.IssuedUsingLocalLicenseID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.IsActive = false;
            this.CreatedByUserID = -1;
            Mode = enMode.AddNew;

        }

        private clsInternationalLicense(int InternationalLicenseID, int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID
            , DateTime IssueDate, DateTime ExpirationDate , bool IsActive,int CreatedByUserID)
        {
            this.InternationalLicenseID = InternationalLicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;
            Mode = enMode.Update;
        }

        private bool _AddNewInternationalLicense()
        {
            this.InternationalLicenseID = clsInternationalLicenseDataAccess.AddNewInternationalLicense(this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID
               , this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);
            return (this.InternationalLicenseID != -1);

        }

        private bool _UpdateInternationalLicense()
        {
            return clsInternationalLicenseDataAccess.UpdateInternationalLicense(this.InternationalLicenseID, this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID
               , this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewInternationalLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateInternationalLicense();

            }
            return false;
        }


        public static clsInternationalLicense GetInternationalLicsense(int InternationalLicenseID)
        {
            int ApplicationID = -1;
            int DriverID = -1;
            int IssuedUsingLocalLicenseID = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            bool IsActive = false;
            int CreatedByUserID = -1;

            if (clsInternationalLicenseDataAccess.GetInternationalLicsense(InternationalLicenseID, ref ApplicationID, ref DriverID, ref IssuedUsingLocalLicenseID,
                ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID))
            {
                return new clsInternationalLicense(InternationalLicenseID, ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate
                   , IsActive, CreatedByUserID);
            }
            else
            {
                return null;
            }
        }

        public static clsInternationalLicense GetInternationalLicsenseByLocalLicense(int LicenseID)
        {
            int InternationalLicenseID = clsInternationalLicenseDataAccess.GetInterLiceseID(LicenseID);
            int ApplicationID = -1;
            int DriverID = -1;
            int IssuedUsingLocalLicenseID = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            bool IsActive = false;
            int CreatedByUserID = -1;

            if (clsInternationalLicenseDataAccess.GetInternationalLicsense(InternationalLicenseID, ref ApplicationID, ref DriverID, ref IssuedUsingLocalLicenseID,
                ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID))
            {
                return new clsInternationalLicense(InternationalLicenseID, ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate
                   , IsActive, CreatedByUserID);
            }
            else
            {
                return null;
            }
        }

        public static bool IsInternationalLicenseExist(int LocalLicenseID)
        {
            return clsInternationalLicenseDataAccess.IsInternationalLicenseExist(LocalLicenseID);  
        }

        public static DataTable GetInternationalLicenses(int PersonID)
        {
            return clsInternationalLicenseDataAccess.GetInternationalLicenses(PersonID);
        }

        public static DataTable GetInternationalLicenseApplications()
        {
            return clsInternationalLicenseDataAccess.GetInternationalLicenseApplications();    
        }
    }
}
