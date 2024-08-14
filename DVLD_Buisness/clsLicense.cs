using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
    public class clsLicense
    {


        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int LicenseClass { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public decimal PaidFees { get; set; }
        public bool IsActive { get; set; }
        public byte IssueReason { get; set; }
        public int CreatedByUserID { get; set; }


        public clsLicense()
        {
            this.LicenseID = default;
            this.ApplicationID = default;
            this.DriverID = default;
            this.LicenseClass = default;
            this.IssueDate = default;
            this.ExpirationDate = default;
            this.Notes = default;
            this.PaidFees = default;
            this.IsActive = default;
            this.IssueReason = default;
            this.CreatedByUserID = default;


            Mode = enMode.AddNew;

        }

        private clsLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClass, DateTime IssueDate, DateTime ExpirationDate, string Notes, decimal PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
        {
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseClass = LicenseClass;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;


            Mode = enMode.Update;

        }

        private bool _AddNewLicense()
        {
            //call DataAccess Layer 

            this.LicenseID = clsLicenseData.AddNewLicense(this.ApplicationID, this.DriverID, this.LicenseClass, this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive, this.IssueReason, this.CreatedByUserID);

            return (this.LicenseID != -1);

        }

        private bool _UpdateLicense()
        {
            //call DataAccess Layer 

            return clsLicenseData.UpdateLicense(this.LicenseID, this.ApplicationID, this.DriverID, this.LicenseClass, this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive, this.IssueReason, this.CreatedByUserID);

        }

        public static clsLicense Find(int LicenseID)
        {
            int ApplicationID = default;
            int DriverID = default;
            int LicenseClass = default;
            DateTime IssueDate = default;
            DateTime ExpirationDate = default;
            string Notes = default;
            decimal PaidFees = default;
            bool IsActive = default;
            byte IssueReason = default;
            int CreatedByUserID = default;


            if (clsLicenseData.GetLicenseInfoByID(LicenseID, ref ApplicationID, ref DriverID, ref LicenseClass, ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))
                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID);
            else
                return null;

        }

        public static clsLicense FindOrdinaryLicense(int LicenseID)
        {
            int ApplicationID = default;
            int DriverID = default;
            int LicenseClass = default;
            DateTime IssueDate = default;
            DateTime ExpirationDate = default;
            string Notes = default;
            decimal PaidFees = default;
            bool IsActive = default;
            byte IssueReason = default;
            int CreatedByUserID = default;


            if (clsLicenseData.FindOrdinaryClassLicenseInfoByID(LicenseID, ref ApplicationID, ref DriverID, ref LicenseClass, ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))
                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID);
            else
                return null;

        }

        public static clsLicense FindBuAppID(int AppID)
        {
            int LicenseID = -1;
            int DriverID = -1;
            int LicenseClass = -1;
            DateTime IssueDate = DateTime.MinValue;
            DateTime ExpirationDate = DateTime.MinValue;
            string Notes = "";
            decimal PaidFees = 0;
            bool IsActive = false;
            byte IssueReason = 0;
            int CreatedByUserID = -1;


            if (clsLicenseData.GetLicenseInfoByApplicationID(ref LicenseID, AppID, ref DriverID, ref LicenseClass, ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))
                return new clsLicense(LicenseID, AppID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID);
            else
                return null;

        }

        public bool Save()
        {


            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicense())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateLicense();

            }




            return false;
        }

        public static DataTable GetAllLicenses() { return clsLicenseData.GetAllLicenses(); }

        public static bool DeleteLicense(int LicenseID) { return clsLicenseData.DeleteLicense(LicenseID); }

        public static bool isLicenseExist(int LicenseID) { return clsLicenseData.IsLicenseExist(LicenseID); }

        public static bool IsExpiredLicense(int LicenseID)
        {
            return clsLicenseData.IsLicenseExpiared(LicenseID);
        }

    }
}
