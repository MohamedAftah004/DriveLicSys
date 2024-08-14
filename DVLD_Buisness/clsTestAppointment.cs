using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Security.Policy;
using System.Xml.Linq;
using DVLD_DataAccess;

namespace DVLD_Buisness
{
    public class clsTestAppointment
    {


        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int TestAppointmentID { get; set; }
        public int TestTypeID { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsLocked { get; set; }


        public clsTestAppointment()
        {
            this.TestAppointmentID = default;
            this.TestTypeID = default;
            this.LocalDrivingLicenseApplicationID = default;
            this.AppointmentDate = default;
            this.PaidFees = default;
            this.CreatedByUserID = default;
            this.IsLocked = default;


            Mode = enMode.AddNew;

        }

        private clsTestAppointment(int TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID, bool IsLocked)
        {
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsLocked = IsLocked;


            Mode = enMode.Update;

        }

        private bool _AddNewTestAppointment()
        {
            //call DataAccess Layer 

            this.TestAppointmentID = clsTestAppointmentData.AddNewTestAppointment(this.TestTypeID, this.LocalDrivingLicenseApplicationID, this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked);

            return (this.TestAppointmentID != -1);

        }

        private bool _UpdateTestAppointment()
        {
            //call DataAccess Layer 

            return clsTestAppointmentData.UpdateTestAppointment(this.TestAppointmentID, this.TestTypeID, this.LocalDrivingLicenseApplicationID, this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked);

        }

        public static clsTestAppointment Find(int TestAppointmentID)
        {
            int TestTypeID = default;
            int LocalDrivingLicenseApplicationID = default;
            DateTime AppointmentDate = default;
            decimal PaidFees = default;
            int CreatedByUserID = default;
            bool IsLocked = default;


            if (clsTestAppointmentData.GetTestAppointmentInfoByID(TestAppointmentID, ref TestTypeID, ref LocalDrivingLicenseApplicationID, ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked))
                return new clsTestAppointment(TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked);
            else
                return null;

        }

        public bool Save()
        {


            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestAppointment())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateTestAppointment();

            }




            return false;
        }

        public static DataTable GetAllTestAppointments() { return clsTestAppointmentData.GetAllTestAppointments(); }

        public static bool DeleteTestAppointment(int TestAppointmentID) { return clsTestAppointmentData.DeleteTestAppointment(TestAppointmentID); }

        public static bool isTestAppointmentExist(int TestAppointmentID) { return clsTestAppointmentData.IsTestAppointmentExist(TestAppointmentID); }

        public static DataTable GetAllTestAppointmentsForSelected_LDLAppID_TestType(int lDLAppID , int testTypeID)
        {
            return clsTestAppointmentData.GetAllTestAppointmentsForSelected_LDLAppID_TestType(lDLAppID, testTypeID);
        }


        public static bool ReplaceLocalIDFromOldLicenseToNewLicense(int NewLDLAppID , int OldLDLAppID)
        {
            return clsTestAppointmentData.ReplaceLocalIDFromOldLicenseToNewLicense(NewLDLAppID, OldLDLAppID);
        }

    }
}
