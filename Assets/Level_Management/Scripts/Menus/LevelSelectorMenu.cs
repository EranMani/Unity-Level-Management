using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LevelManagement.Missions;

namespace LevelManagement
{
    // This class will fill in the data for the user interface by using the scriptable object for missions
    [RequireComponent(typeof(MissionSelector))]
    public class LevelSelectorMenu : Menu<LevelSelectorMenu>
    {
        #region INSPECTOR
        [SerializeField] protected Text _nameText;
        [SerializeField] protected Text _descriptionText;
        [SerializeField] protected Image _previewImage;

        [SerializeField] private float _playDelay = 0.5f;
        [SerializeField] private TransitionFader startTransitionPrefab;
        #endregion

        #region PROTECTED
        protected MissionSelector _missionSelector;
        protected MissionSpecs _currentMission;
        #endregion

        protected override void Awake()
        {
            base.Awake();
            _missionSelector = GetComponent<MissionSelector>();

            // Run for the first time
            UpdateInfo();
        }

        private void OnEnable()
        {
            // Whenever this menu is enabled (menu system disable and enable menus), make sure to update the info
            UpdateInfo();
        }

        // This method will be invoked whenever we start up the application for the first time, open the menu or change
        // the current index of the mission selector
        public void UpdateInfo()
        {
            _currentMission = _missionSelector.GetCurrentMission();

            // Use '?' mark to check if var is null, instead of checking with if statement
            _nameText.text = _currentMission?.Name;
            _descriptionText.text = _currentMission?.Description;
            _previewImage.sprite = _currentMission?.Image;
            _previewImage.color = Color.white;
        }

        public void OnNextPressed()
        {
            _missionSelector.IncrementIndex();
            UpdateInfo();
        }

        public void OnPreviousPressed()
        {
            _missionSelector.DecrementIndex();
            UpdateInfo();
        }

        public void OnPlayPressed()
        {
            StartCoroutine(PlayMissionRoutine(_currentMission?.SceneName));
        }

       
        private IEnumerator PlayMissionRoutine(string sceneName)
        {
            TransitionFader.PlayTransition(startTransitionPrefab);
            LevelLoader.LoadLevel(sceneName);
            yield return new WaitForSeconds(_playDelay);
            GameMenu.Open();
        }
    }


}
