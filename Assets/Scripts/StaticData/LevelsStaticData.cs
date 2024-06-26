﻿using System.Collections.Generic;
using UnityEngine;

namespace Flags.StaticData
{
    [CreateAssetMenu(fileName = "LevelsStaticData", menuName = "StaticData/Levels", order = 0)]
    public class LevelsStaticData : ScriptableObject
    {
        public List<LevelConfig> LevelConfig;
    }
}