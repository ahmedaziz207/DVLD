using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Data_Access
{
    public class clsDetainedLicenseDataAccess
    {
        public static int AddNewDetainedLicense(int LicenseID, DateTime DetainDate, decimal FineFees, int CreatedByUserID, bool IsReleased)
        {
            int DetainID = -1;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO DetainedLicenses
                            (LicenseID,DetainDate,FineFees,CreatedByUserID,IsReleased)
                            Values
                            (@LicenseID,@DetainDate,@FineFees,@CreatedByUserID,@IsReleased);
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, Connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsReleased", IsReleased);

            try
            {
                Connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int integerID))
                {
                    DetainID = integerID;
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

            return DetainID;
        }

        public static bool UpdateDetainedLicense(int DetainID, int LicenseID, DateTime DetainDate, decimal FineFees, int CreatedByUserID, bool IsReleased
            , DateTime? ReleaseDate,int? ReleasedByUserID, int? ReleaseApplicationID)
        {
            int AffectedRows = 0;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update DetainedLicenses 
                        set LicenseID = @LicenseID
                           ,DetainDate =  @DetainDate
                           ,FineFees = @FineFees
                           ,CreatedByUserID = @CreatedByUserID
                           ,IsReleased = @IsReleased
                           ,ReleaseDate = @ReleaseDate
                           ,ReleasedByUserID = @ReleasedByUserID
                           ,ReleaseApplicationID = @ReleaseApplicationID
                            Where DetainID = @DetainID";

            SqlCommand command = new SqlCommand(query, Connection);

            command.Parameters.AddWithValue("@DetainID", DetainID);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsReleased", IsReleased);

            command.Parameters.AddWithValue("@ReleaseDate", (object)ReleaseDate ?? DBNull.Value);
            command.Parameters.AddWithValue("@ReleasedByUserID", (object)ReleasedByUserID ?? DBNull.Value);
            command.Parameters.AddWithValue("@ReleaseApplicationID", (object)ReleaseApplicationID ?? DBNull.Value);

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

        public static bool GetDetainedLicsense(ref int DetainID, int LicenseID,ref DateTime DetainDate,ref decimal FineFees,ref int CreatedByUserID,ref bool IsReleased
            ,ref DateTime? ReleaseDate,ref int? ReleasedByUserID,ref int? ReleaseApplicationID)
        {
            bool isFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM DetainedLicenses WHERE LicenseID = @LicenseID and IsReleased = 0";
            SqlCommand command = new SqlCommand(query, Connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    isFound = true;

                    DetainID = (int)reader["DetainID"];
                    DetainDate = (DateTime)reader["DetainDate"];
                    FineFees = (decimal)reader["FineFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsReleased = (bool)reader["IsReleased"];

                    ReleasedByUserID = reader["ReleasedByUserID"] != DBNull.Value ? Convert.ToInt32(reader["ReleasedByUserID"]) : (int?)null;
                    ReleaseApplicationID = reader["ReleaseApplicationID"] != DBNull.Value ? Convert.ToInt32(reader["ReleaseApplicationID"]) : (int?)null;
                    ReleaseDate = reader["ReleaseDate"] != DBNull.Value ? Convert.ToDateTime(reader["ReleaseDate"]) : (DateTime?)null;

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

        public static bool IsLicenseDetained(int LicenseID)
        {
            bool isFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT X=1 FROM DetainedLicenses WHERE LicenseID = @LicenseID and IsReleased = 0";
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

        public static DataTable GetAllDetainedLicenses()
        {
            DataTable dt = new DataTable();

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT dbo.DetainedLicenses.DetainID as 'D.ID', dbo.DetainedLicenses.LicenseID as 'L.ID', dbo.DetainedLicenses.DetainDate, dbo.DetainedLicenses.IsReleased , dbo.DetainedLicenses.FineFees, dbo.DetainedLicenses.ReleaseDate, dbo.People.NationalNo as 'N.No', 
                  dbo.People.FirstName + ' ' + dbo.People.SecondName + ' ' + ISNULL(dbo.People.ThirdName, ' ') + ' ' + dbo.People.LastName AS FullName, dbo.DetainedLicenses.ReleaseApplicationID as 'R.app ID'
                  FROM     dbo.People INNER JOIN
                  dbo.Drivers ON dbo.People.PersonID = dbo.Drivers.PersonID INNER JOIN
                  dbo.Licenses ON dbo.Drivers.DriverID = dbo.Licenses.DriverID RIGHT OUTER JOIN
                  dbo.DetainedLicenses ON dbo.Licenses.LicenseID = dbo.DetainedLicenses.LicenseID";

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