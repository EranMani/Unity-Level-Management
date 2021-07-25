using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LevelManagement.Data;

namespace LevelManagement
{
    public class SettingsMenu : Menu<SettingsMenu>
    {
        [SerializeField] private Slider _masterVolumeSlider;
        [SerializeField] private Slider _sfxVolumeSlider;
        [SerializeField] private Slider _musicVolumeSlider;

        private DataManager _dataManager;

        protected override void Awake()
        {
            base.Awake();
            _dataManager = FindObjectOfType<DataManager>();        
        }

        private void Start()
        {
            // Call LoadData after the data manager will finish the process in its awake method
            LoadData();
        }

        public void OnMasterVolumeChanged(float volume)
        {
            if (_dataManager != null)
            {
                // Take the local volume var which we are getting dynamically from the UI slider onValueChange event and store them
                _dataManager.MasterVolume = volume;
            }
        }

        public void OnSFXVolumeChanged(float volume)
        {
            if (_dataManager != null)
            {
                _dataManager.SfxVolume = volume;
            }
        }

        public void OnMusicVolumeChanged(float volume)
        {
            if (_dataManager != null)
            {
                _dataManager.MusicVolume = volume;
            }
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
            if (_dataManager != null)
            {
                _dataManager.Save();
            }
        }

        public void LoadData()
        {
            if (_dataManager == null || _masterVolumeSlider == null || _sfxVolumeSlider == null || _musicVolumeSlider == null)
            {
                return;
            }

            _dataManager.Load();

            _masterVolumeSlider.value = _dataManager.MasterVolume;
            _sfxVolumeSlider.value = _dataManager.SfxVolume;
            _musicVolumeSlider.value = _dataManager.MusicVolume;
        }
    } 
}
