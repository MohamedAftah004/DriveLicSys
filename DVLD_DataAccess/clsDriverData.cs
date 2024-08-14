using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsDriverData
    {


        public static bool GetDriverInfoByID(int DriverID, ref int PersonID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Drivers WHERE DriverID = @DriverID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    DriverID = (int)reader["DriverID"];
                    PersonID = (int)reader["PersonID"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    CreatedDate = (DateTime)reader["CreatedDate"];

                }
                else
                {
                    isFound = false;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                clsMisc.LogExceptionOnEventViewr(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally { connection.Close(); }

            return isFound;

        }

        public static bool GetDriverInfoByPersonID(ref int DriverID, int PersonID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Drivers WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    DriverID = (int)reader["DriverID"];
                    PersonID = (int)reader["PersonID"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    CreatedDate = (DateTime)reader["CreatedDate"];

                }
                else
                {
                    isFound = false;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                clsMisc.LogExceptionOnEventViewr(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally { connection.Close(); }

            return isFound;

        }

        public static int AddNewDriver(int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {

            int ID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Drivers VALUES (@PersonID, @CreatedByUserID, @CreatedDate)
        SELECT SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@PersonID", PersonID);

            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            command.Parameters.AddWithValue("@CreatedDate", CreatedDate);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();


                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    ID = insertedID;
                }
            }

            catch (Exception ex)
            {
                clsMisc.LogExceptionOnEventViewr(ex.Message, System.Diagnostics.EventLogEntryType.Error);

            }

            finally
            {
                connection.Close();
            }


            return ID;

        }
        public static bool UpdateDriver(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE Drivers
	SET	PersonID = @PersonID,
	CreatedByUserID = @CreatedByUserID,
	CreatedDate = @CreatedDate	WHERE DriverID = @DriverID";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@DriverID", DriverID);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            command.Parameters.AddWithValue("@CreatedDate", CreatedDate);


            try { connection.Open(); rowsAffected = command.ExecuteNonQuery(); }
            catch (Exception ex)
            {
                clsMisc.LogExceptionOnEventViewr(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally { connection.Close(); }

            return (rowsAffected > 0);

        }
        public static bool DeleteDriver(int DriverID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "DELETE Drivers WHERE DriverID = @DriverID";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                clsMisc.LogExceptionOnEventViewr(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally { connection.Close(); }


            return (rowsAffected > 0);

        }

        public static bool IsDriverExist(int DriverID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT Found=1 FROM Drivers WHERE DriverID= @DriverID";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isFound = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex)
            {
                clsMisc.LogExceptionOnEventViewr(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally { connection.Close(); }


            return isFound;

        }

        public static bool IsDriverExistByPersonID(int PersonID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT Found=1 FROM Drivers WHERE PersonID= @PersonID";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isFound = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex)
            {
                clsMisc.LogExceptionOnEventViewr(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally { connection.Close(); }


            return isFound;

        }


        public static DataTable GetAllDrivers()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Drivers_View";
            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) dt.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                clsMisc.LogExceptionOnEventViewr(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally { connection.Close(); }


            return dt;
        }


        public static DataTable GetLocalLicenseHistory(int DriverID)
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT LicenseID as 'Lic.ID' , 
                                    ApplicationID as 'App.ID' ,
                                    ClassName as 'Class Name',
                                    IssueDate as 'Issue Date' ,
                                    ExpirationDate as 'Expiration Date',
                                    IsActive as 'Is Active'
                            FROM DriverLicenseHistory_View 
                            WHERE DriverID = @DriverID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                    dt.Load(reader);
                else
                reader.Close();
            }
            catch (Exception ex)
            {
                clsMisc.LogExceptionOnEventViewr(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally { connection.Close(); }


            return dt;
        }


        public static DataTable GetInternationalLicenseHistory(int DriverID)
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT InternationalLicenseID as 'Int.License ID' , 
                                    ApplicationID as 'Application ID' ,
                                    IssuedUsingLocalLicenseID as 'L.License ID',
                                    IssueDate as 'Issue Date' ,
                                    ExpirationDate as 'Expiration Date',
                                    IsActive as 'Is Active'
                            FROM InternationalLicenses 
                            WHERE DriverID = @DriverID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) 
                    dt.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                clsMisc.LogExceptionOnEventViewr(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally { connection.Close(); }


            return dt;
        }



    }
}
