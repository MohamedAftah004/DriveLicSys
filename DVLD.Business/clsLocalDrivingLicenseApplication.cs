using System;
using System.Data;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Xml.Linq;
using DVLD.DataAccess;
using static System.Net.Mime.MediaTypeNames;
using static DVLD.Business.clsTestType;


namespace DVLD.Business
{
    public   class clsLocalDrivingLicenseApplication : clsApplication

    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int LocalDrivingLicenseApplicationID { set; get; }
        public int LicenseClassID { set; get; }
        public clsLicenseClass LicenseClassInfo;
        public string PersonFullName   
        {
            get { 
                return clsPerson.Find(ApplicantPersonID).FullName; 
            }   
            
        }

        public clsLocalDrivingLicenseApplication()

        {
            this.LocalDrivingLicenseApplicationID = -1;
            this.LicenseClassID = -1;
            
           
            Mode = enMode.AddNew;

        }

        private clsLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int ApplicationID, int ApplicantPersonID, 
            DateTime ApplicationDate, int ApplicationTypeID,
             enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
             float PaidFees, int CreatedByUserID, int LicenseClassID)

        {
            this.LocalDrivingLicenseApplicationID= LocalDrivingLicenseApplicationID; ;
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = (int) ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.LicenseClassID = LicenseClassID;
            this.LicenseClassInfo = clsLicenseClass.Find(LicenseClassID);
            Mode = enMode.Update;
        }

        private bool _AddNewLocalDrivingLicenseApplication()
        {
            
            
            this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationData.AddNewLocalDrivingLicenseApplication
                (
                this.ApplicationID, this.LicenseClassID);

            return (this.LocalDrivingLicenseApplicationID != -1);
        }

        private bool _UpdateLocalDrivingLicenseApplication()
        {
            

            return clsLocalDrivingLicenseApplicationData.UpdateLocalDrivingLicenseApplication
                (
                this.LocalDrivingLicenseApplicationID ,this.ApplicationID, this.LicenseClassID);
           
        }

        public static clsLocalDrivingLicenseApplication  FindByLocalDrivingAppLicenseID(int LocalDrivingLicenseApplicationID)
        {
            
            int ApplicationID=-1, LicenseClassID=-1;

            bool IsFound = clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationInfoByID
                (LocalDrivingLicenseApplicationID, ref ApplicationID, ref LicenseClassID);


            if (IsFound)
            { 
               
                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);

                
                return new clsLocalDrivingLicenseApplication(
                    LocalDrivingLicenseApplicationID, Application.ApplicationID, 
                    Application.ApplicantPersonID,
                                     Application.ApplicationDate, Application.ApplicationTypeID,
                                    (enApplicationStatus)Application.ApplicationStatus, Application.LastStatusDate,
                                     Application.PaidFees, Application.CreatedByUserID,LicenseClassID);
            }
            else
                return null;
          

        }

        public static clsLocalDrivingLicenseApplication FindByApplicationID(int ApplicationID)
        {
            
            int LocalDrivingLicenseApplicationID = -1, LicenseClassID = -1;

            bool IsFound = clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationInfoByApplicationID 
                (ApplicationID, ref LocalDrivingLicenseApplicationID, ref LicenseClassID);


            if (IsFound)
            {
                
                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);

                
                return new clsLocalDrivingLicenseApplication(
                    LocalDrivingLicenseApplicationID, Application.ApplicationID,
                    Application.ApplicantPersonID,
                                     Application.ApplicationDate, Application.ApplicationTypeID,
                                    (enApplicationStatus)Application.ApplicationStatus, Application.LastStatusDate,
                                     Application.PaidFees, Application.CreatedByUserID, LicenseClassID);
            }
            else
                return null;


        }

        public bool  Save()
        {
          
           
           
            base.Mode = (clsApplication.enMode) Mode;
          if (!base.Save()) 
                return false ;


          
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLocalDrivingLicenseApplication())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateLocalDrivingLicenseApplication();

            }

            return false;
        }

        public bool SaveJustLocal()
        {

            
            

            
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLocalDrivingLicenseApplication())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateLocalDrivingLicenseApplication();

            }

            return false;
        }

        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingLicenseApplicationData.GetAllLocalDrivingLicenseApplications();
        }

        public  bool Delete()
        {
            bool IsLocalDrivingApplicationDeleted = false;
            bool IsBaseApplicationDeleted = false;
            
            IsLocalDrivingApplicationDeleted = clsLocalDrivingLicenseApplicationData.DeleteLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID);
           
            if (!IsLocalDrivingApplicationDeleted)
                return false;
            
            IsBaseApplicationDeleted = base.Delete();
            return IsBaseApplicationDeleted;

        }


    }
}
