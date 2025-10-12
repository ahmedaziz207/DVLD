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
    public class clsLicense
    {
        public enum enMode { AddNew, Update }
        public enMode Mode = enMode.AddNew;
        public enum enIssueReason { FirstTime = 1, Renew = 2, DamagedReplacement = 3, LostReplacement = 4} //Not Used
        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int LicenseClassID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public decimal PaidFees { get; set; }
        public bool IsActive { get; set; }
        public byte IssueReason { get; set; }
        public int CreatedByUserID { get; set; }


        public clsLicense()
        {
            this.LicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.LicenseClassID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.Notes = string.Empty;
            this.PaidFees = 0;
            this.IsActive = false;
            this.IssueReason = 0;
            this.CreatedByUserID = -1;
            Mode = enMode.AddNew;
        }

        private clsLicense(int LicenseID,int ApplicationID,int DriverID,int LicenseClassID,DateTime IssueDate,DateTime ExpirationDate
            ,string Notes,decimal PaidFees,bool IsActive,byte IssueReason,int CreatedByUserID)
        {
            this.LicenseID= LicenseID;
            this.ApplicationID= ApplicationID;
            this.DriverID= DriverID;
            this.LicenseClassID= LicenseClassID;
            this.IssueDate= IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID= CreatedByUserID;
            Mode = enMode.Update;
        }

        private bool _AddNewLicense()
        {
            this.LicenseID = clsLicenseDataAccess.AddNewLicense(this.ApplicationID, this.DriverID, this.LicenseClassID
               ,this.IssueDate,this.ExpirationDate,this.Notes,this.PaidFees,this.IsActive,this.IssueReason,this.CreatedByUserID);
            return (this.LicenseID != -1);
            
        }

        private bool _UpdateLicense()
        {
            return clsLicenseDataAccess.UpdateLicense(this.LicenseID,this.ApplicationID, this.DriverID, this.LicenseClassID
               , this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive, this.IssueReason, this.CreatedByUserID);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateLicense();

            }
            return false;
        }


        public static clsLicense GetLicensebyLDLappID(int appLDLID)
        {
            int LicenseID = clsLicenseDataAccess.GetLicenseID(appLDLID);
            int ApplicationID = -1;
            int DriverID = -1;
            int LicenseClassID = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            string Notes = string.Empty;
            decimal PaidFees = 0;
            bool IsActive = false;
            byte IssueReason = 0;
            int CreatedByUserID = -1;

            if(clsLicenseDataAccess.GetLicsense(LicenseID, ref ApplicationID, ref DriverID, ref LicenseClassID,
                ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))
            {
                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClassID, IssueDate, ExpirationDate,
                    Notes, PaidFees, IsActive, IssueReason, CreatedByUserID);
            }
            else
            {
                return null;
            }
        }

        public static clsLicense GetLicense(int LicenseID)
        {
            int ApplicationID = -1;
            int DriverID = -1;
            int LicenseClassID = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            string Notes = string.Empty;
            decimal PaidFees = 0;
            bool IsActive = false;
            byte IssueReason = 0;
            int CreatedByUserID = -1;

            if (clsLicenseDataAccess.GetLicsense(LicenseID, ref ApplicationID, ref DriverID, ref LicenseClassID,
                ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))
            {
                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClassID, IssueDate, ExpirationDate,
                    Notes, PaidFees, IsActive, IssueReason, CreatedByUserID);
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetLocalLicenses(int PersonID)
        {
            return clsLicenseDataAccess.GetAllLocalLicenses(PersonID);
        }

        public static bool IsLicenseOfClass3Exist(int LicenseID)
        {
            return clsLicenseDataAccess.IsLicenseOfClass3Exist(LicenseID);
        }

        public static bool IsLicenseExist(int LicenseID)
        {
            return clsLicenseDataAccess.IsLicenseExist(LicenseID);  
        }
        public static bool IsLicenseExpired(int LicenseID)
        {
            return clsLicenseDataAccess.IsLicenseExpired(LicenseID);
        }
    }
}
