using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;
using System.Security.Cryptography;

namespace LevelManagement.Data
{
    public class JsonSaver
    {
        private static readonly string _fileName = "saveData1.sav";

        public static string GetSaveFilename()
        {
            // The path is internal to Unity, so it is always using a forward slash even if using windows platform
            return Application.persistentDataPath + "/" + _fileName;
        }

        public void Save(SaveData data)
        {
            // When calculating the hash, make sure the value is set to some consistent default value 
            // Otherwise, it may skew the results of hasing the rest of the data
            data.hasValue = String.Empty;

            string json = JsonUtility.ToJson(data);

            // We have our saved data saving into disk and storing an extra hash value associated with it
            data.hasValue = GetSHA256(json);
            json = JsonUtility.ToJson(data);

            string saveFilename = GetSaveFilename();

            // Once we have the json formatted string, we create a new file stream to prepare for input and output to a file
            FileStream fileStream = new FileStream(saveFilename, FileMode.Create); // Create a new empty file on disk

            // Declare a stream writer as a temporary object to write into the file
            // using syntax tells the program that we are going to dispose of the stream writer once we are finished with it
            // This generates a JSON text object on disk and automatically opens and closes the file for us
            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                writer.Write(json);
            }
        }

        // Loading the file will read what we have stored to disk and then we will replace the values inside of this data object
        public bool Load(SaveData data)
        {
            string loadFilename = GetSaveFilename();
            if (File.Exists(loadFilename))
            {
                // Declare a stream reder as a temporary object to read the file
                using (StreamReader reader = new StreamReader(loadFilename))
                {
                    // Read all input from start to end of the file
                    string json = reader.ReadToEnd();

                    // Load data if the hash values are the same
                    if (CheckData(json))
                    {
                        // Read the values from disk into our saved data object
                        JsonUtility.FromJsonOverwrite(json, data);
                    }
                    else
                    {
                        Debug.Log("JSON_SAVER Load: invalid hash. Aborting file read...");
                    }   
                }

                return true;
            }

            return false;
        }

        // Hash data will produce the same result each time with the same string
        // Change one character will end up with a completely different string
        // Whem someone modifying the data, it will break the hash
        // The hash value saved with the save data must be the same as what the application is recomputing when tries to hash
        // the saved data again
        private bool CheckData(string json)
        {
            // Make temporary saved object to read in the contents of the json string
            SaveData tempSaveData = new SaveData();
            // Read the json string into the temp save data object 
            JsonUtility.FromJsonOverwrite(json, tempSaveData);

            // Store the already saved hash value with the data
            string oldHash = tempSaveData.hasValue;
            // Clear the hash value from the data itself
            tempSaveData.hasValue = String.Empty;

            string tempJson = JsonUtility.ToJson(tempSaveData);
            string newHash = GetSHA256(tempJson);

            // Return true in case the old and new hash are the same and were not modified
            return (oldHash == newHash);
        }

        public void Delete()
        {
            File.Delete(GetSaveFilename());
        }

        public string GetHexStringFromHash(byte[] hash)
        {
            string hexString = String.Empty;

            foreach(byte b in hash)
            {
                // "x2": x=  Convert the byte into a hexadecimal string, 2= two digits
                // Each byte turns into two hex digits, which will be concatenated to make one big hex string
                hexString += b.ToString("x2");
            }

            return hexString;
        }

        private string GetSHA256(string text)
        {
            // Convert the text into an array of bytes
            byte[] textToBytes = Encoding.UTF8.GetBytes(text);

            // Temporary instance that will be used to calculate the hash values
            SHA256Managed mySHA256 = new SHA256Managed();

            byte[] hashValue = mySHA256.ComputeHash(textToBytes);

            // Return hex string
            return GetHexStringFromHash(hashValue);
        }
    } 
}
