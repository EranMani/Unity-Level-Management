﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement
{
    public class SettingsMenu : Menu<SettingsMenu>
    {
        public override void OnBackPressed()
        {
            // Add extra logic before calling base

            base.OnBackPressed();

            // Add extra logic after calling base
        }
    } 
}
