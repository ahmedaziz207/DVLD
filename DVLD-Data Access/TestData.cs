using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Data_Access
{
    public class clsTestDataAccess
    {

        public static int AddNewTest(int TestAppointmentID, bool TestResult,string Notes, int CreatedByUserID)
        {
            int TestID = -1;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Insert into Tests 
                           values(@TestAppointmentID,@TestResult,@Notes,@CreatedByUserID);
                           Select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, Connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            if(!string.IsNullOrWhiteSpace(Notes))
                command.Parameters.AddWithValue("@Notes", Notes);
            else
                command.Parameters.AddWithValue("@Notes", DBNull.Value);


            try
            {
                Connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int integerID))
                {
                    TestID = integerID;
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

            return TestID;
        }
        public static bool DeleteTest(int LDL_AppID)
        {
            int AffectedRows = 0;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Delete from Tests where TestID in 
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
        public static int CountTrialsPerTest(int LDLappID, int TestTypeID)
        {
            int Count = 0;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT COUNT(*) FROM LocalDrivingLicenseApplications INNER JOIN
                  TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                  Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
				  where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LDLappID and TestTypeID = @TestTypeID ";

            SqlCommand command = new SqlCommand(query, Connection);
            command.Parameters.AddWithValue("@LDLappID", LDLappID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                Connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int integerID))
                {
                    Count = integerID;
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

            return Count;

        }
        public static int CountPassedTestsfromThree(int LDLappID)
        {
            int Count = 0;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT COUNT(*) FROM LocalDrivingLicenseApplications INNER JOIN
      TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
       Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
	  where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LDLappID and TestResult =1";

            SqlCommand command = new SqlCommand(query, Connection);
            command.Parameters.AddWithValue("@LDLappID", LDLappID);

            try
            {
                Connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int integerID))
                {
                    Count = integerID;
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

            return Count;

        }
    }
}
