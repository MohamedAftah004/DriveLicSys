using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.Business;
using System.Diagnostics;
using Microsoft.Win32;
using System.Security.Cryptography;
using System.Text;

namespace DVLD.UI.Classes
{
    internal static class clsGlobal
    {
        public static clsUser CurrentUser;

        public static bool RememberUsernameAndPassword(string Username, string Password)
        {

            try
            {
                
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();


                
                string filePath = currentDirectory + "\\data.txt";

                
                if (Username == "" && File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;

                }

                
                string dataToSave = Username + "#//#" + Password;

                
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    
                    writer.WriteLine(dataToSave);

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }

        }

        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            
            try
            {
                
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();

                
                string filePath = currentDirectory + "\\data.txt";

                
                if (File.Exists(filePath))
                {
                    
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            Console.WriteLine(line); 
                            string[] result = line.Split(new string[] { "#//#" }, StringSplitOptions.None);

                            Username = result[0];
                            Password = result[1];
                        }
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }

        }


        public static string Encryption(string s1)
        {
            string afterEnc = "";
            foreach (char c in s1)
            {
                afterEnc += $"{(char)(c + 3)}{(char)(c - 4)}";
            }
            return afterEnc;
        }

        public static string Decryption(string s1)
        {
            string afterDec = "";
            string decryption = "";
            foreach (char c in s1)
            {
                afterDec += $"{(char)(c - 3)}";
            }

            for (int i = 0; i < afterDec.Length; i++)
            {
                if (i % 2 == 0)
                {
                    decryption += $"{afterDec[i]}";
                }
            }

            return decryption;
        }


        public static bool RememberUsernameAndPasswordOnRegistry(string username, string password)
        {

            string keyPath = @"HKEY_CURRENT_USER\Software\DVLD";
            string fileNmae = "LastLoginData";

            string valueData = $"{Encryption(username)}~~~{Encryption(password)}";

            try
            {
                Registry.SetValue(keyPath, fileNmae, valueData, RegistryValueKind.String);
                return true;
            }
            catch
            {
                MessageBox.Show("Error at saving login data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        public static bool GetStoredCredentialFromRegistry(ref string username, ref string password)
        {

            string keyPath = @"HKEY_CURRENT_USER\Software\DVLD";
            string fileNmae = "LastLoginData";


            try
            {
                string value = Registry.GetValue(keyPath, fileNmae, null) as string;

                if (value != null)
                {

                    string[] dataAfterSplited = value.Split(new string[] { "~~~" }, StringSplitOptions.None);
                    username = Decryption(dataAfterSplited[0]);
                    password = Decryption(dataAfterSplited[1]);


                    return true;

                }

            }
            catch
            {
                MessageBox.Show("Error at saving login data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }



        
        public static void LogExceptionOnEventViewr( string errorMessage, EventLogEntryType eventType = EventLogEntryType.Error)
        {
            string sourceName = "DVLD";

            if (!EventLog.SourceExists(sourceName))
            {
                EventLog.CreateEventSource(sourceName, "Application");
            }

            EventLog.WriteEntry(sourceName, errorMessage , eventType);

        }

        

        public static string ComputeHash(string input)
        {

            using (SHA256 sha256 = SHA256.Create())
            {

                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }

        }

    }
}
