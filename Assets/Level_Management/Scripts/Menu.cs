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
            if (GameManager.Instance != null)
            {
                GameManager.Instance.LoadNextLevel();
            }
        }

        public void OnSettingsPressed()
        {
            Menu settingsMenu = transform.parent.Find("Settings_Menu(Clone)").GetComponent<Menu>();
            if (MenuManager.Instance != null && settingsMenu != null)
            {
                MenuManager.Instance.OpenMenu(settingsMenu);
            }
        }

        public void OnCreditsPressed()
        {
            Menu creditsMenu = transform.parent.Find("Credits_Screen(Clone)").GetComponent<Menu>();
            if (MenuManager.Instance != null && creditsMenu != null)
            {
                MenuManager.Instance.OpenMenu(creditsMenu);
            }                
        }

        public void OnBackPressed()
        {
            if (MenuManager.Instance != null)
            {
                MenuManager.Instance.CloseMenu();
            }
        }
    }
}
