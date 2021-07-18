using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement
{
    public class MenuManager : MonoBehaviour
    {
        public Menu mainMenuPrefab;
        public Menu settingsMenuPrefab;
        public Menu creditsScreenPrefab;

        [SerializeField] private Transform _menuParent;

        private Stack<Menu> _menuStack = new Stack<Menu>();

        private void Awake()
        {
            InitializeMenus();
        }

        void InitializeMenus()
        {
            if (_menuParent == null)
            {
                GameObject menuParentObject = new GameObject("Menus");
                _menuParent = menuParentObject.transform;
            }

            Menu[] menuPrefabs = { mainMenuPrefab, settingsMenuPrefab, creditsScreenPrefab };

            foreach (Menu prefab in menuPrefabs)
            {
                if (prefab != null)
                {
                    Menu menuInstance = Instantiate(prefab, _menuParent);
                    if (prefab != mainMenuPrefab)
                    {
                        menuInstance.gameObject.SetActive(false);
                    }
                    else
                    {
                        // Open main menu
                        // This should work only for the main menu
                        OpenMenu(menuInstance);
                    }
                }
            }
        }

        public void OpenMenu(Menu menuInstance)
        {
            if (menuInstance == null)
            {
                Debug.LogWarning("[MENU MANAGER]: OpenMenu ERROR: Invalid menu");
                return;
            }

            // Set the menu at the top of the stack and disable previous ones already in use
            if (_menuStack.Count > 0)
            {
                foreach (Menu menu in _menuStack)
                {
                    menu.gameObject.SetActive(false);
                }
            }

            menuInstance.gameObject.SetActive(true);
            // Using the stack to have some kind of history to keep track of the previous menu
            _menuStack.Push(menuInstance);
        }

        public void CloseMenu()
        {
            if (_menuStack.Count == 0)
            {
                Debug.LogWarning("[MENU MANAGER]: CloseMenu ERROR: No menus in stack!");
                return;
            }

            // Remove menu object from the top of the stack and deactivate it
            Menu topMenu = _menuStack.Pop();
            topMenu.gameObject.SetActive(false);

            if (_menuStack.Count > 0)
            {
                // Return the menu object at the top of the stack and activate it
                Menu nextMenu = _menuStack.Peek();
                nextMenu.gameObject.SetActive(true);
            }


        }

     
    } 
}
