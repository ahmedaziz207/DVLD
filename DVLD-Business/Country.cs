using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_Data_Access;

namespace DVLD_Business
{
    public class clsCountry
    {
        public enum enMode { AddNew, Update }
        public enMode Mode = enMode.AddNew;
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        
        private bool _AddNewCountry()
        {
            this.CountryID = clsCountryDataAccess.AddNewCountry(this.CountryName);
            return (this.CountryID != -1);
            
        }

        private bool _UpdateCountry()
        {
            return clsCountryDataAccess.UpdateCountry(this.CountryID, this.CountryName);
        }

        public clsCountry()
        {
            this.CountryID = -1;
            this.CountryName = string.Empty;
            Mode = enMode.AddNew;
        }
        private clsCountry(int CountryCountryID, string CountryName)
        {
            this.CountryID = CountryCountryID;
            this.CountryName = CountryName;
            Mode = enMode.Update;
        }

        public static clsCountry FindCountry(int CountryCountryID)
        {
            string CountryName = "";

            if (clsCountryDataAccess.FindCountry(CountryCountryID, ref CountryName))
            {
                return new clsCountry(CountryCountryID, CountryName);
            }
            else
            {
                return null;
            }
        }

        public static clsCountry FindCountryByName(string CountryName)
        {
            int CountryID = 0;

            if (clsCountryDataAccess.FindCountryByName(ref CountryID, CountryName))
            {
                return new clsCountry(CountryID, CountryName);
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
                    if (_AddNewCountry())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateCountry();

            }
            return false;
        }

        public static bool isCountryExist(int CountryID)
        {
            return clsCountryDataAccess.IsCountryExist(CountryID);
        }

        public static bool IsCountryExistByName(string CountryName)
        {
            return clsCountryDataAccess.IsCountryExistByName(CountryName);
        }

        public static bool DeleteCountry(int CountryID)
        {
            return clsCountryDataAccess.DeleteCountry(CountryID);
        }

        public static DataTable GetAllCountries()
        {
            return clsCountryDataAccess.GetAllCountries();
        }

        
       
    }
}
