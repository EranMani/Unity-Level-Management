using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace LevelManagement
{
    public class WinScreen : Menu<WinScreen>
    {
        public void OnNextLevelPressed()
        {
            // Close the screen and go back to the main menu
            base.OnBackPressed();

            LevelLoader.LoadNextLevel();
        }

        public void OnRestartPressed()
        {
            base.OnBackPressed();
            LevelLoader.ReloadLevel();
        }

        public void OnMainMenuPressed()
        {
            LevelLoader.LoadMainMenuLevel();
            MainMenu.Open();
        }
    } 
}
