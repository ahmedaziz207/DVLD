using System;
using System.Data;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_Data_Access
{
    public class clsLicenseDataAccess
    {
        public static int AddNewLicense(int ApplicationID, int DriverID, int LicenseClassID,DateTime IssueDate,DateTime ExpirationDate
            ,string Notes,decimal PaidFees,bool IsActive,byte IssueReason,int CreatedByUserID)
        {
            int LicenseID = -1;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Licenses 
                   (ApplicationID, DriverID, LicenseClassID, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID)
                   VALUES 
                   (@ApplicationID, @DriverID, @LicenseClassID, @IssueDate, @ExpirationDate, @Notes, @PaidFees, @IsActive, @IssueReason, @CreatedByUserID);
                   SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, Connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            command.Parameters.AddWithValue("@Notes", string.IsNullOrEmpty(Notes) ? (object)DBNull.Value : Notes);

            try
                {
                    Connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int integerID))
                    {
                        LicenseID = integerID;
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

            return LicenseID;
        }

        public static bool UpdateLicense(int LicenseID,int ApplicationID, int DriverID, int LicenseClassID, DateTime IssueDate, DateTime ExpirationDate
            , string Notes, decimal PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
        {
            int AffectedRows = 0;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update Licenses 
                        set ApplicationID = @ApplicationID
                           ,DriverID =  @DriverID
                           ,LicenseClassID = @LicenseClassID
                           ,IssueDate = @IssueDate
                           ,ExpirationDate = @ExpirationDate
                           ,Notes = @Notes
                           ,PaidFees = @PaidFees
                           ,IsActive = @IsActive
                           ,IssueReason = @IssueReason
                           ,CreatedByUserID = @CreatedByUserID
                            Where LicenseID = @LicenseID";

            SqlCommand command = new SqlCommand(query, Connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            if (!string.IsNullOrEmpty(Notes))
                command.Parameters.AddWithValue("@Notes", Notes);
            else
                command.Parameters.AddWithValue("@Notes", DBNull.Value);


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

        public static bool GetLicsense(int LicenseID,ref int ApplicationID,ref int DriverID,ref int LicenseClassID,ref DateTime IssueDate,ref DateTime ExpirationDate
            ,ref string Notes,ref decimal PaidFees,ref bool IsActive,ref byte IssueReason,ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Licenses WHERE LicenseID = @LicenseID";
            SqlCommand command = new SqlCommand(query, Connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    isFound = true;

                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClassID = (int)reader["LicenseClassID"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    PaidFees = (Decimal)reader["PaidFees"];
                    IsActive = (bool)reader["IsActive"];
                    IssueReason = (byte)reader["IssueReason"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    Notes = reader["Notes"]?.ToString() ?? string.Empty;

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

        public static int GetLicenseID(int appLDLID)
        {
            int LicenseID = -1;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT Licenses.LicenseID FROM Applications INNER JOIN
                  LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID INNER JOIN
                  Licenses ON Applications.ApplicationID = Licenses.ApplicationID
				  where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @appLDLID";

            SqlCommand command = new SqlCommand(query, Connection);
            command.Parameters.AddWithValue("@appLDLID", appLDLID);
            

            try
            {
                Connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int integerID))
                {
                    LicenseID = integerID;
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

            return LicenseID;
        }

        public static DataTable GetAllLocalLicenses(int PersonID)
        {
            DataTable dt = new DataTable();

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT Licenses.LicenseID as 'Lic.ID', Licenses.ApplicationID as 'App.ID', LicenseClasses.ClassName as 'Class Name' 
                          , Licenses.IssueDate as 'Issue Date', Licenses.ExpirationDate as 'Expiration Date' , Licenses.IsActive as 'Is Active'  FROM  Licenses INNER JOIN
                  Drivers ON Licenses.DriverID = Drivers.DriverID INNER JOIN
                  LicenseClasses ON Licenses.LicenseClassID = LicenseClasses.LicenseClassID
				   Where Drivers.PersonID = @PersonID
                   ORDER BY Licenses.LicenseID DESC";

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

        public static bool IsLicenseOfClass3Exist(int LicenseID)
        {
            bool isFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT X=1 FROM Licenses INNER JOIN
                  LicenseClasses ON Licenses.LicenseClassID = LicenseClasses.LicenseClassID
				  WHERE Licenses.LicenseID = @LicenseID and LicenseClasses.LicenseClassID = 3";
            SqlCommand command = new SqlCommand(query, Connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

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

        public static bool IsLicenseExist(int LicenseID)
        {
            bool isFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT X=1 FROM Licenses  WHERE LicenseID = @LicenseID";
            SqlCommand command = new SqlCommand(query, Connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

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

        public static bool IsLicenseExpired(int LicenseID)
        {
            bool isFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select x=1 from Licenses where LicenseID = @LicenseID and (DATEDIFF(day,GETDATE(),Licenses.ExpirationDate)<0);";
            SqlCommand command = new SqlCommand(query, Connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

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


    }
}
