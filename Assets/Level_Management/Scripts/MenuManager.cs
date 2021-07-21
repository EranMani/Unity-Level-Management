using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement
{
    public class MenuManager : MonoBehaviour
    {
        public MainMenu mainMenuPrefab;
        public SettingsMenu settingsMenuPrefab;
        public CreditsScreen creditsScreenPrefab;
        public GameMenu gameMenuPrefab;
        public PauseMenu pauseMenuPrefab;
        public WinScreen winScreenPrefab;

        [SerializeField] private Transform _menuParent;

        private Stack<Menu> _menuStack = new Stack<Menu>();

        private static MenuManager _instance;

        public static MenuManager Instance { get => _instance; }

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
                InitializeMenus();
                // Make the menu manager itself persistent through scenes
                DontDestroyOnLoad(gameObject);
            }         
        }

        private void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }


        void InitializeMenus()
        {
            if (_menuParent == null)
            {
                GameObject menuParentObject = new GameObject("Menus");
                _menuParent = menuParentObject.transform;
            }

            // Make the parent objects of the menus persistent throughout scenes
            // This will also make the children of that parent persistent
            // NOTE: Object is the base class for everything 
            //Object.DontDestroyOnLoad(_menuParent);
            DontDestroyOnLoad(_menuParent);

            Menu[] menuPrefabs = { mainMenuPrefab, settingsMenuPrefab, creditsScreenPrefab, gameMenuPrefab, pauseMenuPrefab, winScreenPrefab };

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
