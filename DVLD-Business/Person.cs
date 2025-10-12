using DVLD_Data_Access;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsPerson
    {
        public enum enMode { AddNew, Update }
        public enMode Mode = enMode.AddNew;
        public int PersonID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string FullName()
        {
            return FirstName + " " + SecondName + " " + ThirdName + " " + LastName;
        }
        public DateTime DateOfBirth { get; set; }
        public short Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalityCountryID { get; set; }

        public clsCountry CountryInfo;
        public string ImagePath { get; set; }


        private bool _AddNewPerson()
        {
            this.PersonID = clsPersonDataAccess.AddNewPerosn(this.NationalNo, this.FirstName, this.SecondName,
                this.ThirdName, this.LastName, this.DateOfBirth, this.Gender, this.Address, this.Phone,
                this.Email, this.NationalityCountryID, this.ImagePath);
            return (this.PersonID != -1);
        }

        private bool _UpdatePerson()
        {
            return clsPersonDataAccess.UpdatePerson(this.PersonID,this.NationalNo,this.FirstName,this.SecondName,
                this.ThirdName,this.LastName,this.DateOfBirth,this.Gender,this.Address,this.Phone,
                this.Email,this.NationalityCountryID,this.ImagePath);
        }

        public clsPerson()
        {
            this.PersonID = -1;
            this.NationalNo = string.Empty;
            this.FirstName = string.Empty;
            this.SecondName = string.Empty;
            this.ThirdName = string.Empty;
            this.LastName = string.Empty;
            this.DateOfBirth = DateTime.Now;
            this.Gender = -1;
            this.Address = string.Empty;
            this.Phone = string.Empty;
            this.Email = string.Empty;
            this.NationalityCountryID = -1;
            this.ImagePath = string.Empty;


            Mode = enMode.AddNew;
        }

        private clsPerson(int PersonID,string NationalNo, string FirstName,string SecondName
        ,string ThirdName,string LastName,DateTime DateOfBirth,short Gender,string Address,string Phone
        ,string Email,int NationalityCountryID,string ImagePath)
        {
            this.PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gender = Gender;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;
            this.CountryInfo = clsCountry.FindCountry(NationalityCountryID);
            this.ImagePath = ImagePath;

            Mode = enMode.Update;
        }

        public static clsPerson GetPersonByID(int PersonID)
        {
            string NationalNo = string.Empty;
            string FirstName = string.Empty;
            string SecondName = string.Empty;
            string ThirdName = string.Empty;
            string LastName = string.Empty;
            short Gender = -1;
            string Email = string.Empty;
            int NationalityCountryID = -1;
            string ImagePath = string.Empty;
            string Address = string.Empty;
            string Phone = string.Empty;
            DateTime DateOfBirth = DateTime.Now;


            if (clsPersonDataAccess.GetPersonByID(PersonID,ref NationalNo,ref FirstName,ref SecondName,
                ref ThirdName,ref LastName,ref DateOfBirth, ref Gender,ref Address, ref Phone,ref Email,
                ref NationalityCountryID,ref ImagePath))
            { 
                return new clsPerson(PersonID,NationalNo,FirstName,SecondName,ThirdName,LastName,
                    DateOfBirth,Gender,Address,Phone,Email,NationalityCountryID,ImagePath);
            }
            else
            {
                return null;
            }
        }

        public static clsPerson GetPersonByNationalNo(string NationalNo)
        {
            int PersonID = -1;
            string FirstName = string.Empty;
            string SecondName = string.Empty;
            string ThirdName = string.Empty;
            string LastName = string.Empty;
            short Gender = -1;
            string Email = string.Empty;
            int NationalityCountryID = -1;
            string ImagePath = string.Empty;
            string Address = string.Empty;
            string Phone = string.Empty;
            DateTime DateOfBirth = DateTime.Now;

            if (clsPersonDataAccess.GetPersonByNationalNo(ref PersonID, NationalNo, ref FirstName, ref SecondName,
                ref ThirdName, ref LastName, ref DateOfBirth, ref Gender, ref Address, ref Phone, ref Email,
                ref NationalityCountryID, ref ImagePath))
            {
                return new clsPerson(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName,
                    DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID, ImagePath);
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
                    if (_AddNewPerson())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdatePerson();

            }
            return false;
        }

        public static bool DeletePerson(int PersonID)
        {
            return clsPersonDataAccess.DeletePerson(PersonID);
        }

        public static bool isPersonExist(int PersonID)
        {
            return clsPersonDataAccess.IsPersonExist(PersonID);
        }

        public static bool isPersonExistByNationalNo(string NationalNo)
        {
            return clsPersonDataAccess.IsPersonExistByNationalNo(NationalNo);
        }

        public static DataTable GetAllPeople()
        {
            return clsPersonDataAccess.GetAllPeople();
        }

       

       
    }
}
