using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SampleGame;

namespace LevelManagement
{
    // Each menu must contain a canvas object 
    [RequireComponent(typeof(Canvas))]
    public class Menu : MonoBehaviour
    {
        public void OnPlayPressed()
        {
            GameManager gameManager = Object.FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.LoadNextLevel();
            }
        }

        public void OnSettingsPressed()
        {
            MenuManager menuManager = Object.FindObjectOfType<MenuManager>();
            Menu settingsMenu = transform.parent.Find("Settings_Menu(Clone)").GetComponent<Menu>();
            if (menuManager != null && settingsMenu != null)
            {
                menuManager.OpenMenu(settingsMenu);
            }
        }

        public void OnCreditsPressed()
        {
            MenuManager menuManager = Object.FindObjectOfType<MenuManager>();
            Menu creditsMenu = transform.parent.Find("Credits_Screen(Clone)").GetComponent<Menu>();
            if (menuManager != null && creditsMenu != null)
            {
                menuManager.OpenMenu(creditsMenu);
            }                
        }

        public void OnBackPressed()
        {
            MenuManager menuManager = Object.FindObjectOfType<MenuManager>();
            if (menuManager != null)
            {
                menuManager.CloseMenu();
            }
        }
    }
}
