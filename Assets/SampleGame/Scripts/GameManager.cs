using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.ThirdPerson;

namespace SampleGame
{
    public class GameManager : MonoBehaviour
    {
        // reference to player
        private ThirdPersonCharacter _player;

        // reference to goal effect
        private GoalEffect _goalEffect;

        // reference to player
        private Objective _objective;

        private bool _isGameOver;
        public bool IsGameOver { get { return _isGameOver; } }

        [SerializeField] private string nextLevelName;
        [SerializeField] private int nextLevelIndex;


        // initialize references
        private void Awake()
        {
            _player = Object.FindObjectOfType<ThirdPersonCharacter>();
            _objective = Object.FindObjectOfType<Objective>();
            _goalEffect = Object.FindObjectOfType<GoalEffect>();
        }

        // end the level
        public void EndLevel()
        {
            if (_player != null)
            {
                // disable the player controls
                ThirdPersonUserControl thirdPersonControl =
                    _player.GetComponent<ThirdPersonUserControl>();

                if (thirdPersonControl != null)
                {
                    thirdPersonControl.enabled = false;
                }

                // remove any existing motion on the player
                Rigidbody rbody = _player.GetComponent<Rigidbody>();
                if (rbody != null)
                {
                    rbody.velocity = Vector3.zero;
                }

                // force the player to a stand still
                _player.Move(Vector3.zero, false, false);
            }

            // check if we have set IsGameOver to true, only run this logic once
            if (_goalEffect != null && !_isGameOver)
            {
                _isGameOver = true;
                _goalEffect.PlayEffect();
                LoadNextLevel();
            }
        }

        private void LoadLevel(string levelName)
        {
            // Scene name validation
            if (Application.CanStreamedLevelBeLoaded(levelName))
            {
                SceneManager.LoadScene(levelName);
            }
            else
            {
                Debug.LogWarning("GAME_MANAGER LoadLevel Error: invalid scene specified!");
            }
        }

        private void LoadLevel(int levelIndex)
        {
            // Scene index validation
            if (levelIndex >= 0 && levelIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(levelIndex);
            }
            else
            {
                Debug.LogWarning("GAME_MANAGER LoadLevel Error: invalid scene specified!");
            }
            
        }

        public void ReloadLevel()
        {
            string currentScene = SceneManager.GetActiveScene().name;
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            SceneManager.LoadScene(currentSceneIndex);
        }

        public void LoadNextLevel()
        {
            //int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            //int nextSceneIndex = currentSceneIndex + 1;
            //int totalScenesCount = SceneManager.sceneCountInBuildSettings;

            //// Modulus operator:
            //// 2 % 2 = 0
            //// 0 % 2 = 0
            //// 1 % 2 = 1
            //nextSceneIndex = nextSceneIndex % totalScenesCount;

            int nextSceneIndex = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;

            SceneManager.LoadScene(nextSceneIndex);
        }

        // check for the end game condition on each frame
        private void Update()
        {
            if (_objective != null && _objective.IsComplete)
            {
                EndLevel();
            }
        }

    }
}