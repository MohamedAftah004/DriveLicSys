using System;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using DVLD_DataAccess;

namespace DVLD_Buisness
{
    public class clsTestType
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int TestTypeID { get; set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }
        public decimal TestTypeFees { get; set; }


        public clsTestType()
        {
            this.TestTypeID = default;
            this.TestTypeTitle = default;
            this.TestTypeDescription = default;
            this.TestTypeFees = default;


            Mode = enMode.AddNew;

        }

        private clsTestType(int TestTypeID, string TestTypeTitle, string TestTypeDescription, decimal TestTypeFees)
        {
            this.TestTypeID = TestTypeID;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeDescription = TestTypeDescription;
            this.TestTypeFees = TestTypeFees;


            Mode = enMode.Update;

        }


        public static bool UpdateTestType(int testTypeID , string typeTitle , string typeDesciption , decimal typeFees)
        {
            //call DataAccess Layer 

            return clsTestTypeData.UpdateTestType(testTypeID , typeTitle, typeDesciption , typeFees);

        }

        public static clsTestType Find(int TestTypeID)
        {
            string TestTypeTitle = "";
            string TestTypeDescription = "";
            decimal TestTypeFees = 0;


            if (clsTestTypeData.GetTestTypeInfoByID(TestTypeID, ref TestTypeTitle, ref TestTypeDescription, ref TestTypeFees))
                return new clsTestType(TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees);
            else
                return null;

        }




        public static DataTable GetAllTestTypes() { return clsTestTypeData.GetAllTestTypes(); }

        public static bool DeleteTestType(int TestTypeID) { return clsTestTypeData.DeleteTestType(TestTypeID); }

        public static bool isTestTypeExist(int TestTypeID) { return clsTestTypeData.IsTestTypeExist(TestTypeID); }


    }
}
