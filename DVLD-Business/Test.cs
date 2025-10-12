using DVLD_Data_Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsTest
    {
        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public bool TestResult { get; set; }
        public string Notes { get; set; }///Could Be NuLL
        public int CreatedByUserID { get; set; }    


        public clsTest()
        {
            this.TestID = -1;
            this.TestAppointmentID = -1;
            this.TestResult = false;
            this.Notes = string.Empty;
            this.CreatedByUserID = -1;
        }

        private bool _AddNewTest()
        {
            this.TestID = clsTestDataAccess.AddNewTest(this.TestAppointmentID,this.TestResult,this.Notes,this.CreatedByUserID);
            return (this.TestAppointmentID != -1);
        }

        public bool Save()
        {
            return _AddNewTest();
        }

        public static bool DeleteTest(int LDL_AppID)
        {
            return clsTestDataAccess.DeleteTest(LDL_AppID);
        }

        public static int CountTrialsPerTest(int LDLappID, int TestTypeID)
        {
            return clsTestDataAccess.CountTrialsPerTest(LDLappID, TestTypeID);
        }


        public static int CountPassedTestsfromThree(int LDLappID)
        {
            return clsTestDataAccess.CountPassedTestsfromThree(LDLappID);
        }
    }
}
