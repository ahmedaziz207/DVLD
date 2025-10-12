using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_Data_Access
{
    public class clsInternationalLicenseDataAccess
    {
        public static int AddNewInternationalLicense(int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate
            , bool IsActive, int CreatedByUserID)
        {
            int InternationalLicenseID = -1;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO InternationalLicenses 
                   (ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID)
                   VALUES 
                   (@ApplicationID, @DriverID, @IssuedUsingLocalLicenseID, @IssueDate, @ExpirationDate, @IsActive, @CreatedByUserID);
                   SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, Connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                Connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int integerID))
                {
                    InternationalLicenseID = integerID;
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

            return InternationalLicenseID;
        }

        public static bool UpdateInternationalLicense(int InternationalLicenseID, int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate
            , bool IsActive, int CreatedByUserID)
        {
            int AffectedRows = 0;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update InternationalLicenses 
                        set ApplicationID = @ApplicationID
                           ,DriverID =  @DriverID
                           ,IssuedUsingLocalLicenseID = @IssuedUsingLocalLicenseID
                           ,IssueDate = @IssueDate
                           ,ExpirationDate = @ExpirationDate
                           ,IsActive = @IsActive
                           ,CreatedByUserID = @CreatedByUserID
                            Where InternationalLicenseID = @InternationalLicenseID";

            SqlCommand command = new SqlCommand(query, Connection);

            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                Connection.Open();
                AffectedRows = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //
            }
            finally
            {
                Connection.Close();
            }

            return (AffectedRows > 0);
        }
        public static bool GetInternationalLicsense(int InternationalLicenseID, ref int ApplicationID, ref int DriverID, ref int IssuedUsingLocalLicenseID, ref DateTime IssueDate, ref DateTime ExpirationDate
          , ref bool IsActive, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM InternationalLicenses WHERE InternationalLicenseID = @InternationalLicenseID";
            SqlCommand command = new SqlCommand(query, Connection);
            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

            try
            {
                Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    isFound = true;

                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    IssuedUsingLocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    IsActive = (bool)reader["IsActive"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];

                }
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

        public static int GetInterLiceseID(int LicenseID)
        {
            int InternationalLicenseID = -1;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT InternationalLicenses.InternationalLicenseID
FROM     InternationalLicenses INNER JOIN
                  Licenses ON InternationalLicenses.IssuedUsingLocalLicenseID = Licenses.LicenseID
				   where Licenses.licenseID = @LicenseID";

            SqlCommand command = new SqlCommand(query, Connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);


            try
            {
                Connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int integerID))
                {
                    InternationalLicenseID = integerID;
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

            return InternationalLicenseID;
        }

        public static bool IsInternationalLicenseExist(int LocalLicenseID)
        {
            bool isFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT x=1 FROM Licenses INNER JOIN
                  InternationalLicenses ON Licenses.LicenseID = InternationalLicenses.IssuedUsingLocalLicenseID
				  WHERE Licenses.LicenseID = @LocalLicenseID";
            SqlCommand command = new SqlCommand(query, Connection);
            command.Parameters.AddWithValue("@LocalLicenseID", LocalLicenseID);

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

        public static DataTable GetInternationalLicenses(int PersonID)
        {
            DataTable dt = new DataTable();

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT InternationalLicenses.InternationalLicenseID as 'int.License ID', InternationalLicenses.ApplicationID as 'Application ID'
                  , InternationalLicenses.IssuedUsingLocalLicenseID as 'L.License ID', InternationalLicenses.IssueDate as 'Issue Date'
                  , InternationalLicenses.ExpirationDate as 'Expiration Date', 
                  InternationalLicenses.IsActive as 'Is Active' FROM InternationalLicenses INNER JOIN
                  Drivers ON InternationalLicenses.DriverID = Drivers.DriverID
				  WHERE Drivers.PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, Connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

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

        public static DataTable GetInternationalLicenseApplications()
        {
            DataTable dt = new DataTable();

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT InternationalLicenses.InternationalLicenseID as 'Int.License ID', Applications.ApplicationID as 'Application ID', InternationalLicenses.DriverID as 'Driver ID'
                  ,InternationalLicenses.IssuedUsingLocalLicenseID as 'L.License ID', InternationalLicenses.IssueDate as 'Issue Date', InternationalLicenses.ExpirationDate as 'Expiration Date', 
                  InternationalLicenses.IsActive as 'Is Active' FROM  Applications INNER JOIN
                  InternationalLicenses ON Applications.ApplicationID = InternationalLicenses.ApplicationID";

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
