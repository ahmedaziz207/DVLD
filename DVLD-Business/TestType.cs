using DVLD_Data_Access;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsTestType
    {
        public int TestTypeID { get; set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }
        public decimal TestTypeFees { get; set; }


        private bool _UpdateTestType()
        {
            return clsTestTypeDataAccess.UpdateTestType(this.TestTypeID, this.TestTypeTitle, this.TestTypeDescription, this.TestTypeFees);
        }

        private clsTestType(int TestTypeID, string TestTypeTitle,string TestTypeDescription, decimal TestTypeFees)
        {
            this.TestTypeID = TestTypeID;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeDescription = TestTypeDescription;
            this.TestTypeFees = TestTypeFees;
        }

        public static clsTestType GetTestType(int TestTypeID)
        {
            string TestTypeTitle = "";
            string TestTypeDescription = "";
            decimal TestFees = 0;

            if (clsTestTypeDataAccess.GetTestType(TestTypeID, ref TestTypeTitle,ref TestTypeDescription, ref TestFees))
            {
                return new clsTestType(TestTypeID, TestTypeTitle, TestTypeDescription, TestFees);
            }
            else
            {
                return null;
            }
        }


        public bool Save()
        {
            return _UpdateTestType();
        }

        public static bool IsTestTypeExist(int TestTypeID)
        {
            return clsTestTypeDataAccess.IsTestTypeExist(TestTypeID);
        }


        public static DataTable GetAllTestTypes()
        {
            return clsTestTypeDataAccess.GetAllTestTypes();
        }
    }
}
