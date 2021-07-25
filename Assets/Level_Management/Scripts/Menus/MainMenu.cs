using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SampleGame;
using LevelManagement.Data;
using UnityEngine.UI;

namespace LevelManagement
{
    // We get a singleton pattern automatically now because we're deriving from the generic menu 
    public class MainMenu : Menu<MainMenu>
    {
        private DataManager _dataManager;

        [SerializeField] private float _playDelay = 0.5f;
        [SerializeField] private TransitionFader startTransitionPrefab;
        [SerializeField] private InputField _playerNameInputField;

        protected override void Awake()
        {
            base.Awake();
            _dataManager = FindObjectOfType<DataManager>();
        }

        private void Start()
        {
            LoadData();       
        }

        public void OnPlayPressed()
        {
            StartCoroutine(OnPlayPressedRoutine());
        }

        private IEnumerator OnPlayPressedRoutine()
        {
            TransitionFader.PlayTransition(startTransitionPrefab);
            LevelLoader.LoadNextLevel();
            yield return new WaitForSeconds(_playDelay);
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

        public void OnPlayerNameValueChanged(string name)
        {
            if (_dataManager != null)
            {
                _dataManager.PlayerName = name;
            }
        }

        public void OnPlayerNameEndEdit()
        {
            if (_dataManager != null)
            {
                _dataManager.Save();
            }
        }

        private void LoadData()
        {
            if (_dataManager == null && _playerNameInputField == null)
            {
                return;
            }

            _dataManager.Load();

            _playerNameInputField.text = _dataManager.PlayerName;
        }
    } 
}
