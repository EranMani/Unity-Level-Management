using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SampleGame;

namespace LevelManagement
{
    // We get a singleton pattern automatically now because we're deriving from the generic menu 
    public class MainMenu : Menu<MainMenu>
    {
        public void OnPlayPressed()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.LoadNextLevel();
            }

            GameMenu.Open();
        }

        public void OnSettingsPressed()
        {
            SettingsMenu.Open();
        }

        public void OnCreditsPressed()
        {
            CreditsScreen.Open();
        }

        public override void OnBackPressed()
        {
            // Apply the method behaviour from the base class
            // base.OnBackPressed();

            Application.Quit();
        }
    } 
}
