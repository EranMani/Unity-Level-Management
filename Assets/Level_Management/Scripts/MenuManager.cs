using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

namespace LevelManagement
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private MainMenu mainMenuPrefab;
        [SerializeField] private SettingsMenu settingsMenuPrefab;
        [SerializeField] private CreditsScreen creditsScreenPrefab;
        [SerializeField] private GameMenu gameMenuPrefab;
        [SerializeField] private PauseMenu pauseMenuPrefab;
        [SerializeField] private WinScreen winScreenPrefab;
        [SerializeField] private LevelSelectorMenu levelSelectMenuPrefab;

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
            DontDestroyOnLoad(_menuParent);

            // Find out which fields contain menu prefabs to generate a collection of them at runtime
            // Get the system type of THIS menu manager by assigning - this.GetType()
            // By using GetFields, we get information about each field
            // BindingFlags will allow to search for the proper menu fields. These fields are called as an instance,
            // they are private (not public) and declared within the menu manager instead of getting fields from inheritance for example
            // The fields should only be specific to the menu manager, and not mono behaviour
            BindingFlags myFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;
            FieldInfo[] fields = this.GetType().GetFields(myFlags);

            foreach (FieldInfo field in fields)
            {
                // Get the prefab which is stored in the field. Since we get the MENU component, we need to cast the result to MENU
                Menu prefab = field.GetValue(this) as Menu;

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
