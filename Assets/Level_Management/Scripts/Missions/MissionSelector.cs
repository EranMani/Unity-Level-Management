using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement.Missions
{
    public class MissionSelector : MonoBehaviour
    {
        #region INSPECTOR   
        [SerializeField] protected MissionsList _missionsList;
        #endregion

        #region PROTECTED
        protected int _currentIndex = 0;
        #endregion

        #region PROPERTIES
        public int CurrentIndex => _currentIndex;
        #endregion

        public void ClampIndex()
        {
            if (_missionsList.TotalMissions == 0)
            {
                Debug.LogWarning("MISSIONS_SELECTOR ClampIndex: missing mission setup!");
                return;
            }

            if (_currentIndex >= _missionsList.TotalMissions)
            {
                _currentIndex = 0;
            }

            if (_currentIndex < 0)
            {
                _currentIndex = _missionsList.TotalMissions - 1;
            }
        }

        public void SetIndex(int index)
        {
            _currentIndex = index;
            ClampIndex();
        }

        public void IncrementIndex()
        {
            SetIndex(_currentIndex + 1); 
        }

        public void DecrementIndex()
        {
            SetIndex(_currentIndex - 1);
        }

        public MissionSpecs GetMission(int index)
        {
            return _missionsList.GetMissions(index);
        }

        public MissionSpecs GetCurrentMission()
        {
            return _missionsList.GetMissions(_currentIndex);
        }
    } 
}
