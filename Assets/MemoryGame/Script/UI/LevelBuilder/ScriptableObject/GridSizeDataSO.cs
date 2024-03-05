using System;
using UnityEngine;

namespace MemoryGame.UI.GridBuilder
{
    [CreateAssetMenu(fileName = "GridSizeDataSO", menuName = "ScriptableObjects/LevelBuilder/GridSizeDataSO")]
    public class GridSizeDataSO : ScriptableObject
    {
        public GridSizeData[] levelData;
    }
    
    [Serializable]
    public class GridSizeData
    {
        public int rows;
        public int columns;
        public Vector2 gridSize = new Vector2(200, 200);
        
        public int GetNumberOfElements() => (rows * columns)/2;
    }
}