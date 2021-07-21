using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelManagement
{
    public class LevelLoader : MonoBehaviour
    {
        // Note: static methods require the usage of static variables
        // A static variable will not show up in the Unity inspector
        // By making a variable - static - it becomes a globle variable which can be accessed only within the script
        private static int mainMenuIndex = 0;

        public static void LoadLevel(string levelName)
        {
            // Scene name validation
            if (Application.CanStreamedLevelBeLoaded(levelName))
            {
                SceneManager.LoadScene(levelName);
            }
            else
            {
                Debug.LogWarning("LEVEL_LOADER LoadLevel Error: invalid scene specified!");
            }
        }

        public static void LoadLevel(int levelIndex)
        {
            // Scene index validation
            if (levelIndex >= 0 && levelIndex < SceneManager.sceneCountInBuildSettings)
            {
                // In case the next level has the same index as the main menu, open the menu
                if (levelIndex == mainMenuIndex)
                {
                    MainMenu.Open();
                }

                SceneManager.LoadScene(levelIndex);
            }
            else
            {
                Debug.LogWarning("LEVEL_LOADER LoadLevel Error: invalid scene specified!");
            }

        }

        public static void ReloadLevel()
        {
            string currentScene = SceneManager.GetActiveScene().name;
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            SceneManager.LoadScene(currentSceneIndex);
        }

        public static void LoadNextLevel()
        {
            int nextSceneIndex = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
            LoadLevel(nextSceneIndex);
        }

        public static void LoadMainMenuLevel()
        {
            LoadLevel(mainMenuIndex);
        }
    } 

}
