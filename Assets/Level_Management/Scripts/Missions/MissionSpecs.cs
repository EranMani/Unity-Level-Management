using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace LevelManagement.Missions
{
    // Show C# object in the inspector. This can be applied to scripts without mono-behavior
    [Serializable]
    public class MissionSpecs 
    {
        // Below, best practice for data encapsulation: by using private/protected and properties for them
        #region INSPECTOR
        [SerializeField] protected string _name;
        [SerializeField] [Multiline] protected string _description;
        [SerializeField] protected string _sceneName;
        [SerializeField] protected string _id;
        [SerializeField] protected Sprite _image;
        #endregion

        // Similar to decalring a GET property, but shorter
        #region PROPERTIES
        public string Name => _name;
        public string Description => _description;
        public string SceneName => _sceneName;
        public string Id => _id;
        public Sprite Image => _image;
        #endregion
    }
}
