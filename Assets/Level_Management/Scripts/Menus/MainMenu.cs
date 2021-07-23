using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SampleGame;

namespace LevelManagement
{
    // We get a singleton pattern automatically now because we're deriving from the generic menu 
    public class MainMenu : Menu<MainMenu>
    {
        [SerializeField] private float _playDelay = 0.5f;
        [SerializeField] private TransitionFader startTransitionPrefab;

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
    } 
}
