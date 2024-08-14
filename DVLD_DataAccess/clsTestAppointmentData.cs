using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVLD_DataAccess.clsCountryData;
using System.Net;
using System.Security.Policy;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;

namespace DVLD_DataAccess
{
    public class clsTestAppointmentData
    {


        public static bool GetTestAppointmentInfoByID(int TestAppointmentID, ref int TestTypeID, ref int LocalDrivingLicenseApplicationID, ref DateTime AppointmentDate, ref decimal PaidFees, ref int CreatedByUserID, ref bool IsLocked)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            try
            {

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TestTypeID = (int)reader["TestTypeID"];
                    LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    PaidFees = (decimal)reader["PaidFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsLocked = (bool)reader["IsLocked"];

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
        public static int AddNewTestAppointment(int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID, bool IsLocked)
        {

            int ID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO TestAppointments VALUES (@TestTypeID, @LocalDrivingLicenseApplicationID, @AppointmentDate, @PaidFees, @CreatedByUserID, @IsLocked)
        SELECT SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);

            command.Parameters.AddWithValue("@PaidFees", PaidFees);

            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            command.Parameters.AddWithValue("@IsLocked", IsLocked);


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
        public static bool UpdateTestAppointment(int TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID, bool IsLocked)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE TestAppointments
	SET	TestTypeID = @TestTypeID,
	LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID,
	AppointmentDate = @AppointmentDate,
	PaidFees = @PaidFees,
	CreatedByUserID = @CreatedByUserID,
	IsLocked = @IsLocked	WHERE TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);

            command.Parameters.AddWithValue("@PaidFees", PaidFees);

            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            command.Parameters.AddWithValue("@IsLocked", IsLocked);


            try { connection.Open(); rowsAffected = command.ExecuteNonQuery(); }
            catch (Exception ex)
            {
                clsMisc.LogExceptionOnEventViewr(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally { connection.Close(); }

            return (rowsAffected > 0);

        }

        public static bool ReplaceLocalIDFromOldLicenseToNewLicense( int NewLocalDrivingLicenseApplicationID , int OldLocalDrivingLicenseApplicationID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update TestAppointments
                            SET LocalDrivingLicenseApplicationID = @NewLocalDrivingLicenseApplicationID
                            WHERE LocalDrivingLicenseApplicationID = @OldLocalDrivingLicenseApplicationID";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@NewLocalDrivingLicenseApplicationID", NewLocalDrivingLicenseApplicationID);

            command.Parameters.AddWithValue("@OldLocalDrivingLicenseApplicationID", OldLocalDrivingLicenseApplicationID);

            try { connection.Open(); rowsAffected = command.ExecuteNonQuery(); }
            catch (Exception ex)
            {
                clsMisc.LogExceptionOnEventViewr(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally { connection.Close(); }

            return (rowsAffected > 0);

        }


        public static bool DeleteTestAppointment(int TestAppointmentID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "DELETE TestAppointments WHERE TestAppointmentID = @TestAppointmentID";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

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

        public static bool IsTestAppointmentExist(int TestAppointmentID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT Found=1 FROM TestAppointments WHERE TestAppointmentID= @TestAppointmentID";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

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

        public static DataTable GetAllTestAppointments()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM TestAppointments";
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

        //load Table TestAppointment -> selected -> LDLAppID/TestType
        public static DataTable GetAllTestAppointmentsForSelected_LDLAppID_TestType(int lDLAppID , int testTypeID)
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select TestAppointmentID ,
                                    AppointmentDate  ,
                                    PaidFees ,
                                    IsLocked 
                            FROM TestAppointments
                            WHERE LocalDrivingLicenseApplicationID = @LDLAppID  and TestTypeID = @TestTypeID";
            //string query = "select * from TestAppointments where LocalDrivingLicenseApplicationID = @LDLAppID  and TestTypeID = @TestTypeID";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LDLAppID", lDLAppID);
            command.Parameters.AddWithValue("@TestTypeID", testTypeID);


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


    }
}
