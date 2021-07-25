using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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
            string json = JsonUtility.ToJson(data);
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
                    
                    // Read the values from disk into our saved data object
                    JsonUtility.FromJsonOverwrite(json, data);
                }

                return true;
            }

            return false;
        }

        public void Delete()
        {
            File.Delete(GetSaveFilename());
        }
    } 
}
