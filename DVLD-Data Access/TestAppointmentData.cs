using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Data_Access
{
    public class clsTestAppointmentDataAccess
    {

        public static bool GetTestAppointment(int TestAppointmentID,ref int TestTypeID,ref int LocalDrivingLicenseApplicationID
            ,ref DateTime AppointmentDate,ref decimal PaidFees,ref int CreatedByUserID,ref bool IsLocked,ref int RetakeTestApplicationID)
        {
            bool isFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "Select * from TestAppointments where TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query, Connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            try
            {
                Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    isFound = true;

                    TestTypeID = (int)reader["TestTypeID"];
                    LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    PaidFees = (decimal)reader["PaidFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsLocked = (bool)reader["IsLocked"];

                    if (reader["RetakeTestApplicationID"] == DBNull.Value)
                        RetakeTestApplicationID = -1;
                    else
                        RetakeTestApplicationID = (int)reader["RetakeTestApplicationID"];

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

        public static bool GetLastTestAppointmentOverAll(ref int TestAppointmentID, ref int TestTypeID, int LocalDrivingLicenseApplicationID
            , ref DateTime AppointmentDate, ref decimal PaidFees, ref int CreatedByUserID, ref bool IsLocked, ref int RetakeTestApplicationID)
        {
            bool isFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Select  top 1 * from TestAppointments Where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                            Order by TestAppointmentID desc";

            SqlCommand command = new SqlCommand(query, Connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    isFound = true;

                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TestTypeID = (int)reader["TestTypeID"];
                    //LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    PaidFees = (decimal)reader["PaidFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsLocked = (bool)reader["IsLocked"];

                    if (reader["RetakeTestApplicationID"] == DBNull.Value)
                        RetakeTestApplicationID = -1;
                    else
                        RetakeTestApplicationID = (int)reader["RetakeTestApplicationID"];

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

        public static int AddNewTestAppointment(int TestTypeID, int LocalDrivingLicenseApplicationID
            ,DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID, bool IsLocked,int RetakeTestApplicationID = -1)
        {
            int TestAppointmentID = -1;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Insert into TestAppointments 
                           values(@TestTypeID,@LocalDrivingLicenseApplicationID,@AppointmentDate,@PaidFees
                           ,@CreatedByUserID,@IsLocked,@RetakeTestApplicationID);
                           Select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, Connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);
            
            if (RetakeTestApplicationID != -1)
                command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);
            else
                command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);


            try
            {
                Connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int integerID))
                {
                    TestAppointmentID = integerID;
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

            return TestAppointmentID;
        }

        public static bool UpdateTestAppointment(int TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID
                , DateTime AppointmentDate,decimal PaidFees, int CreatedByUserID,bool IsLocked, int RetakeTestApplicationID = -1)
        {
            int AffectedRows = 0;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update TestAppointments 
                           set TestTypeID = @TestTypeID
                              ,LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                              ,AppointmentDate = @AppointmentDate
                              ,PaidFees = @PaidFees
                              ,CreatedByUserID = @CreatedByUserID
                              ,IsLocked = @IsLocked
                              ,RetakeTestApplicationID = @RetakeTestApplicationID

                            where TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query, Connection);


            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);

            if (RetakeTestApplicationID != -1)
                command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);
            else
                command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);

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

        public static bool DeleteTestAppointment(int LDL_AppID)
        {
            int AffectedRows = 0;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Delete from TestAppointments where TestAppointmentID in 
                    ( SELECT Tests.TestID FROM LocalDrivingLicenseApplications INNER JOIN
                  TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                  Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
				  where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LDL_AppID)";

            SqlCommand command = new SqlCommand(query, Connection);
            command.Parameters.AddWithValue("@LDL_AppID", LDL_AppID);

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

        public static DataTable GetAllTestAppointments(int LDLappID,int TestTypeID)
        {
            DataTable dt = new DataTable();

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT TestAppointmentID AS 'Appointment ID',AppointmentDate AS 'Appointment Date'
                            ,PaidFees AS 'Paid Fees',IsLocked AS 'Is Locked' From TestAppointments
                             Where TestTypeID = @TestTypeID and LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                             order by [Appointment ID] Desc";

            SqlCommand command = new SqlCommand(query, Connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LDLappID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

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

            }
            finally
            {
                Connection.Close();
            }

            return dt;
        }

        public static bool IsThereOpenAppointment(int LDLappID, int TestTypeID)
        {
            bool isFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT TestAppointmentID AS 'Appointment ID',AppointmentDate AS 'Appointment Date'
                            ,PaidFees AS 'Paid Fees',IsLocked AS 'Is Locked' From TestAppointments
                             Where TestTypeID = @TestTypeID and LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                              and IsLocked = 0";

            SqlCommand command = new SqlCommand(query, Connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LDLappID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

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
