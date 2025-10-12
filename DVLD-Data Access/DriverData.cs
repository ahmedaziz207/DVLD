using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Data_Access
{
    public class clsDriverDataAccess
    {
        public static int AddNewDriver(int PersonID,int CreatedByUserID,DateTime CreatedDate)
        {
            int DriverID = -1;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Insert into Drivers 
                           values(@PersonID,@CreatedByUserID,@CreatedDate);
                           Select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, Connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@CreatedDate", CreatedDate);

            try
            {
                Connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int integerID))
                {
                    DriverID = integerID;
                }
            }
            catch (Exception ex)
            {
                //
            }
            finally
            {
                Connection.Close();
            }

            return DriverID;
        }

        public static bool DoesDriverHaveThisLicense(int PersonID, string LicenseClassName)
        {
            bool isFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT Licenses.LicenseID FROM  Drivers INNER JOIN
                  Licenses ON Drivers.DriverID = Licenses.DriverID INNER JOIN
                  LicenseClasses ON Licenses.LicenseClassID = LicenseClasses.LicenseClassID
				  where Drivers.PersonID = @PersonID and LicenseClasses.ClassName = @LicenseClassName";

            SqlCommand command = new SqlCommand(query, Connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClassName", LicenseClassName);

            try
            {
                Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isFound = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                Connection.Close();
            }

            return isFound;
        }

        public static int ReturnDriverIDIsExist(int PersonID)
        {
            int DriverID = -1;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT Drivers.DriverID FROM Drivers INNER JOIN
                  People ON Drivers.PersonID = People.PersonID
				  where People.PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, Connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                Connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int integerID))
                {
                    DriverID = integerID;
                }
            }
            catch (Exception ex)
            {
                //
            }
            finally
            {
                Connection.Close();
            }

            return DriverID;
        }

        public static DataTable GetDrivers()
        {
            DataTable dt = new DataTable();

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT dbo.Drivers.DriverID as 'Driver ID', dbo.Drivers.PersonID as 'Person ID', dbo.People.NationalNo as 'National No', dbo.People.FirstName + ' ' + dbo.People.SecondName + ' ' + ISNULL(dbo.People.ThirdName, '') + ' ' + dbo.People.LastName AS 'Full Name', dbo.Drivers.CreatedDate as 'Date',
                      [Active Licenses]=(SELECT COUNT(LicenseID) AS NumberOfActiveLicenses
                       FROM      dbo.Licenses
                       WHERE   (IsActive = 1) AND (DriverID = dbo.Drivers.DriverID)) 
					   +(SELECT count(*) FROM InternationalLicenses 
				       Where (IsActive = 1) and (InternationalLicenses.DriverID = dbo.Drivers.DriverID)) 
                       FROM     dbo.Drivers INNER JOIN
                  dbo.People ON dbo.Drivers.PersonID = dbo.People.PersonID";

            SqlCommand command = new SqlCommand(query, Connection);

            try
            {
                Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                //
            }
            finally
            {
                Connection.Close();
            }

            return dt;
        }
    }
}
