using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement
{
    public class SettingsMenu : Menu<SettingsMenu>
    {
        public void OnMasterVolumeChanged(float volume)
        {
           
        }

        public void OnSFXVolumeChanged(float volume)
        {

        }

        public void OnMusicVolumeChanged(float volume)
        {

        }

        public override void OnBackPressed()
        {
            // Add extra logic before calling base

            base.OnBackPressed();

            // Add extra logic after calling base
        }
    } 
}
