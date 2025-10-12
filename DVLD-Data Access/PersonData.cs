using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Data_Access
{
    public class clsPersonDataAccess
    {
        public static bool GetPersonByID(int PersonID,ref string NationalNo, ref string FirstName, ref string SecondName
        ,ref string ThirdName,ref string LastName,ref DateTime DateOfBirth,ref short Gender,ref string Address, ref string Phone
        ,ref string Email,ref int NationalityCountryID,ref string ImagePath)
        {
            bool isFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM People WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, Connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    isFound = true;

                    NationalNo = reader["NationalNo"]?.ToString() ?? string.Empty;
                    FirstName = reader["FirstName"]?.ToString() ?? string.Empty;
                    SecondName = reader["SecondName"]?.ToString() ?? string.Empty;
                    ThirdName = reader["ThirdName"]?.ToString() ?? string.Empty;
                    LastName = reader["LastName"]?.ToString() ?? string.Empty;
                    Address = reader["Address"]?.ToString() ?? string.Empty;
                    Phone = reader["Phone"]?.ToString() ?? string.Empty;
                    Email = reader["Email"]?.ToString() ?? string.Empty;
                    ImagePath = reader["ImagePath"]?.ToString() ?? string.Empty;

                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gender = Convert.ToInt16(reader["Gender"]);
                    NationalityCountryID = Convert.ToInt32(reader["NationalityCountryID"]);

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

        public static bool GetPersonByNationalNo(ref int PersonID,string NationalNo, ref string FirstName, ref string SecondName
        , ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref short Gender, ref string Address, ref string Phone
        , ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM People WHERE NationalNo = @NationalNo";
            SqlCommand command = new SqlCommand(query, Connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            try
            {
                Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    isFound = true;

                    PersonID = Convert.ToInt32(reader["PersonID"]);
                    FirstName = reader["FirstName"]?.ToString() ?? string.Empty;
                    SecondName = reader["SecondName"]?.ToString() ?? string.Empty;
                    LastName = reader["LastName"]?.ToString() ?? string.Empty;
                    Address = reader["Address"]?.ToString() ?? string.Empty;
                    Phone = reader["Phone"]?.ToString() ?? string.Empty;

                   
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gender = Convert.ToInt16(reader["Gender"]);
                    NationalityCountryID = Convert.ToInt32(reader["NationalityCountryID"]);

                    
                    ThirdName = reader["ThirdName"]?.ToString() ?? string.Empty;
                    Email = reader["Email"]?.ToString() ?? string.Empty;
                    ImagePath = reader["ImagePath"]?.ToString() ?? string.Empty;

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

        public static int AddNewPerosn(string NationalNo, string FirstName, string SecondName
        , string ThirdName, string LastName, DateTime DateOfBirth, short Gender, string Address, string Phone
        , string Email, int NationalityCountryID, string ImagePath)
        {
            int PersonID = -1;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Insert into People 
                           values(@NationalNo,@FirstName,@SecondName,@ThirdName,@LastName,
                           @DateOfBirth,@Gender,@Address,@Phone,@Email,@NationalityCountryID,@ImagePath);
                           Select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, Connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gender", Gender);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            
            if (!string.IsNullOrEmpty(ThirdName))
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", DBNull.Value);

            // Email
            if (!string.IsNullOrEmpty(Email))
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", DBNull.Value);

            // ImagePath
            if (!string.IsNullOrEmpty(ImagePath))
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", DBNull.Value);

            try
            {
                Connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int integerID))
                {
                    PersonID = integerID;
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

            return PersonID;
        }

        public static bool UpdatePerson(int PersonID,string NationalNo, string FirstName, string SecondName
        , string ThirdName, string LastName, DateTime DateOfBirth, short Gender, string Address, string Phone
        , string Email, int NationalityCountryID, string ImagePath)
        {
            int AffectedRows = 0;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update People 
                        set NationalNo = @NationalNo
                           ,FirstName =  @FirstName
                           ,SecondName = @SecondName
                           ,ThirdName = @ThirdName
                           ,LastName =  @LastName
                           ,DateOfBirth = @DateOfBirth
                           ,Gender = @Gender
                           ,Address = @Address
                           ,Phone = @Phone
                           ,Email = @Email
                           ,NationalityCountryID = @NationalityCountryID
                           ,ImagePath = @ImagePath
                            where PersonID=@PersonID";

            SqlCommand command = new SqlCommand(query, Connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gender", Gender);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);


            if (!string.IsNullOrEmpty(ThirdName))
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", DBNull.Value);

            // Email
            if (!string.IsNullOrEmpty(Email))
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", DBNull.Value);

            // ImagePath
            if (!string.IsNullOrEmpty(ImagePath))
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", DBNull.Value);

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

        public static bool DeletePerson(int PersonID)
        {
            int AffectedRows = 0;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Delete from People 
                            where PersonID=@PersonID";

            SqlCommand command = new SqlCommand(query, Connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

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

        public static DataTable GetAllPeople()
        {
            DataTable dt = new DataTable();

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT People.PersonID, People.NationalNo, People.FirstName, People.SecondName,
                            People.ThirdName, People.LastName, Gender = 
                            CASE
                                  WHEN People.Gender = 0 THEN 'Male'
	                                WHEN People.Gender = 1 THEN 'Female'
                            END
                            ,People.DateOfBirth,Countries.CountryName as Nationality, People.Phone, People.Email
                            FROM Countries INNER JOIN People ON Countries.CountryID = People.NationalityCountryID";

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

        public static bool IsPersonExist(int PersonID)
        {
            bool isFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "Select x=1 from People where PersonID=@PersonID";

            SqlCommand command = new SqlCommand(query, Connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

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

        public static bool IsPersonExistByNationalNo(string NationalNo)
        {
            bool isFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "Select x=1 from People where NationalNo=@NationalNo";

            SqlCommand command = new SqlCommand(query, Connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);

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
