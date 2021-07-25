using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement.Data
{
    // External objects wont be able to access the data in SaveData package directly
    // The other objects always need to interface with the data using the data manager
    // Each piece of data from the same data needs a corresponding public property in this class
    public class DataManager : MonoBehaviour
    {
        private SaveData _saveData;
        private JsonSaver _jsonSaver;

        // Expose saved data from SaveData package to objects in scene to GET and SET values
        public float MasterVolume { 
            get { return _saveData.masterVolume; } 
            set { _saveData.masterVolume = value; }
        }

        public float SfxVolume
        {
            get { return _saveData.sfxVolume; }
            set { _saveData.sfxVolume = value; }
        }

        public float MusicVolume
        {
            get { return _saveData.musicVolume; }
            set { _saveData.musicVolume = value; }
        }

        public string PlayerName
        {
            get { return _saveData.playerName; }
            set { _saveData.playerName = value; }
        }

        private void Awake()
        {
            // Initialize default data
            _saveData = new SaveData();

            // Initialize JSON saver class
            _jsonSaver = new JsonSaver();
        }

        public void Save()
        {
            _jsonSaver.Save(_saveData);
        }

        public void Load()
        {
            _jsonSaver.Load(_saveData);
        }
    } 

    
}
