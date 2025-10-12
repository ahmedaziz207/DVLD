using DVLD_Data_Access;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsTestAppointment
    {
        public enum enMode { AddNew, Update }
        public enMode Mode = enMode.AddNew;

        public int TestAppointmentID { get; set; }
        public int TestTypeID { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsLocked { get; set; }
        public int RetakeTestApplicationID { get; set; }///RetakeTestApplicationID could be NULLLLLL


        private bool _AddNewTestAppointment()
        {
            this.TestAppointmentID = clsTestAppointmentDataAccess.AddNewTestAppointment(this.TestTypeID, this.LocalDrivingLicenseApplicationID
            , this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked, this.RetakeTestApplicationID);
            return (this.TestAppointmentID != -1);
        }

        private bool _UpdateTestAppointment()
        {
            return clsTestAppointmentDataAccess.UpdateTestAppointment(this.TestAppointmentID, this.TestTypeID, this.LocalDrivingLicenseApplicationID
                , this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked, this.RetakeTestApplicationID);
        }

        public clsTestAppointment()
        {
            this.TestAppointmentID = -1;
            this.TestTypeID = -1;
            this.LocalDrivingLicenseApplicationID = -1;
            this.AppointmentDate = DateTime.Now;
            this.PaidFees = -1;
            this.CreatedByUserID = -1;
            this.IsLocked = false;
            this.RetakeTestApplicationID = -1;

            Mode = enMode.AddNew;
        }

        private clsTestAppointment(int TestAppointmentID,int TestTypeID,int LocalDrivingLicenseApplicationID
            ,DateTime AppointmentDate,decimal PaidFees,int CreatedByUserID,bool IsLocked,int RetakeTestApplicationID)
        {
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsLocked = IsLocked;
            this.RetakeTestApplicationID = RetakeTestApplicationID;

            Mode = enMode.Update;
        }


        public static clsTestAppointment GetTestAppointment(int TestAppointmentID)
        {
            int TestTypeID = -1;
            int LocalDrivingLicenseApplicationID = -1;
            DateTime AppointmentDate = DateTime.Now;
            decimal PaidFees = -1;
            int CreatedByUserID = -1;
            bool IsLocked = false;
            int RetakeTestApplicationID = -1;

            if (clsTestAppointmentDataAccess.GetTestAppointment(TestAppointmentID, ref TestTypeID, ref LocalDrivingLicenseApplicationID
                , ref AppointmentDate, ref PaidFees,ref CreatedByUserID, ref IsLocked,ref RetakeTestApplicationID))
            {
                return new clsTestAppointment(TestAppointmentID,TestTypeID,LocalDrivingLicenseApplicationID
                ,AppointmentDate,PaidFees,CreatedByUserID,IsLocked,RetakeTestApplicationID);
            }
            else
            {
                return null;
            }
        }

        public static clsTestAppointment GetLastTestAppointmentOverAll(int LocalDrivingLicenseApplicationID)
        {
            int TestAppointmentID = -1;
            int TestTypeID = -1;
            //int LocalDrivingLicenseApplicationID = -1;
            DateTime AppointmentDate = DateTime.Now;
            decimal PaidFees = -1;
            int CreatedByUserID = -1;
            bool IsLocked = false;
            int RetakeTestApplicationID = -1;

            if (clsTestAppointmentDataAccess.GetLastTestAppointmentOverAll(ref TestAppointmentID, ref TestTypeID, LocalDrivingLicenseApplicationID
                , ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID))
            {
                return new clsTestAppointment(TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID
                , AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            }
            else
            {
                return null;
            }
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestAppointment())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateTestAppointment();

            }
            return false;
        }

        public static bool DeleteTestAppointment(int LDL_AppID)
        {
            return clsTestAppointmentDataAccess.DeleteTestAppointment(LDL_AppID);
        }

        public static DataTable GetAllTestAppointments(int LDLappID, int TestTypeID)
        {
             return clsTestAppointmentDataAccess.GetAllTestAppointments(LDLappID,TestTypeID);
        }

        public static bool IsThereOpenAppointment(int LDLappID, int TestTypeID)
        {
            return clsTestAppointmentDataAccess.IsThereOpenAppointment(LDLappID, TestTypeID);
        }

       
    }
}
