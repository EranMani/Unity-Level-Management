using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement
{
    public class CreditsScreen : Menu
    {
        private static CreditsScreen _instance;
        public static CreditsScreen Instance { get => _instance; }

        private void Awake()
        {
            // In case the manager already initialized, delete the duplicate one
            // Keep only one instance of the manager at all times
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        private void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }

        public override void OnBackPressed()
        {
            // Add extra logic before calling base

            base.OnBackPressed();

            // Add extra logic after calling base
        }
    } 
}
