using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor.VersionControl;
using UnityEngine;

namespace Content.Scripts.Mouse
{
    [CreateAssetMenu(fileName = "New PointerConfig", menuName = "ScriptableObjects/New PointerConfig", order = 1)]
    public class PointerConfig : ScriptableObject
    {
        public KeyCode PrimaryKey;
        public KeyCode SecondaryKey;
        public int HandleDelay;

        public List<PointerViewData> CursorViewData = new List<PointerViewData>();
    }

    [Serializable]
    public struct PointerViewData
    {
        public MouseViewState State;
        public Texture2D View;
    }
}