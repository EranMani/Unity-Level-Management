using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SampleGame;

namespace LevelManagement
{
    public class MainMenu : Menu
    {
        private static MainMenu _instance;
        public static MainMenu Instance { get => _instance; }

        public void OnPlayPressed()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.LoadNextLevel();
            }
        }

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

        public void OnSettingsPressed()
        {
            if (MenuManager.Instance != null && SettingsMenu.Instance != null)
            {
                MenuManager.Instance.OpenMenu(SettingsMenu.Instance);
            }
        }

        public void OnCreditsPressed()
        {
            if (MenuManager.Instance != null && CreditsScreen.Instance != null)
            {
                MenuManager.Instance.OpenMenu(CreditsScreen.Instance);
            }
        }

        public override void OnBackPressed()
        {
            // Apply the method behaviour from the base class
            // base.OnBackPressed();

            Application.Quit();
        }
    } 
}
