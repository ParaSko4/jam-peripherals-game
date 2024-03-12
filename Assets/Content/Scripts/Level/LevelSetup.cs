using System.Collections.Generic;
using UnityEngine;

namespace Content.Scripts.Level
{
    [CreateAssetMenu(fileName = "New LevelSetup", menuName = "ScriptableObjects/New Level Setup", order = 1)]
    public class LevelSetup :ScriptableObject
    {
        public List<Level> Levels = new List<Level>();
    }
}
