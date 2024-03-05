using System;
using UnityEngine;

namespace MemoryGame.ScriptableObjectsScripts
{
    [CreateAssetMenu(fileName = "FlipObjectData", menuName = "ScriptableObjects/FlipCard/FlipObjectSO")]

    public class FlipObjectSO:ScriptableObject
    {
        public FlipCardObjectData[] fObjects;
    }

    [Serializable]
    public class FlipCardObjectData
    { 
        public int fObjectID;
        public Sprite fObjectSprite;
    }
}