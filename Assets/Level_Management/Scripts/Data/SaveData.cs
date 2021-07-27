using System.Collections;
using System.Collections.Generic;
using System;

namespace LevelManagement.Data
{
    [Serializable]
    public class SaveData 
    {
        // ** Make sure that there is a public class variable that represents what you want to save ** //

        public string playerName;
        private readonly string defaultPlayerName = "Player";

        public float masterVolume;
        public float sfxVolume;
        public float musicVolume;

        // Store the large hexadecimal string 
        public string hasValue;

        // A constructor to initialize default data
        public SaveData()
        {
            playerName = defaultPlayerName;
            masterVolume = 0f;
            sfxVolume = 0f;
            musicVolume = 0f;
            hasValue = String.Empty;
        }
    } 
}
