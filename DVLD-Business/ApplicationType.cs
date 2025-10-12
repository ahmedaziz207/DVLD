using DVLD_Data_Access;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsApplicationType
    {
        public int ApplicationTypeID { get; set; }
        public string ApplicationTypeTitle { get; set; }
        public decimal ApplicationFees { get; set; }


        private bool _UpdateApplicationType()
        {
            return clsApplicationTypeDataAccess.UpdateApplicationType(this.ApplicationTypeID, this.ApplicationTypeTitle, this.ApplicationFees);
        }

        private clsApplicationType(int ApplicationTypeID, string ApplicationTypeTitle, decimal ApplicationFees)
        {
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.ApplicationFees = ApplicationFees;
        }

        public static clsApplicationType GetApplicationType(int ApplicationTypeID)
        {
            string ApplicationTypeTitle = "";
            decimal ApplicationFees = 0;

            if (clsApplicationTypeDataAccess.GetApplicationType(ApplicationTypeID, ref ApplicationTypeTitle, ref ApplicationFees))
            {
                return new clsApplicationType(ApplicationTypeID, ApplicationTypeTitle, ApplicationFees);
            }
            else
            {
                return null;
            }
        }

        public bool Save()
        {
            return _UpdateApplicationType();
        }

        public static bool IsApplicationTypeExist(int ApplicationTypeID)
        {
            return clsApplicationTypeDataAccess.IsApplicationTypeExist(ApplicationTypeID);
        }

        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypeDataAccess.GetAllApplicationTypes(); 
        }

    }
}
