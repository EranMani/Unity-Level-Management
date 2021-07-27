using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SampleGame;

namespace LevelManagement
{
    // Abstract: since we don't want to instantiate a standalone menu object
    // Derive the T from the Menu class that we've already defined down here
    // Limit the generic type T to this menu class
    // We can constrain T to a class a struct or an interface. Constrain T to the same class we're actually defining
    public abstract class Menu<T> : Menu where T : Menu<T>
    {
        private static T _instance;
        public static T Instance { get => _instance; }

        // Protected means outside objects can't see the method but derived classes can
        protected virtual void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = (T)this;
            }
        }

        protected virtual void OnDestroy()
        {
            _instance = null;
        }

        public static void Open()
        {
            // Check if the selected menu instance and the menu manager are not null
            if (MenuManager.Instance != null && Instance != null)
            {
                // Open the given menu instance
                MenuManager.Instance.OpenMenu(Instance);
            }
        }
    }

    // Each menu must contain a canvas object 
    [RequireComponent(typeof(Canvas))]
    public abstract class Menu : MonoBehaviour
    {
        public virtual void OnBackPressed()
        {
            if (MenuManager.Instance != null)
            {
                MenuManager.Instance.CloseMenu();
            }
        }
    }
}
