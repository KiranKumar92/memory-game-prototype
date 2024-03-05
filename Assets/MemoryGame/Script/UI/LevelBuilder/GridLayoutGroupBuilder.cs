using UnityEngine;
using UnityEngine.UI;

namespace MemoryGame.UI.GridBuilder
{
    public class GridLayoutGroupBuilder
    {
        #region Private Variables
        private GameObject _gridObject;
        private GridLayoutGroup _gridLayoutGroup;
        private RectTransform _rectTransform;
        private GridSizeData _gridSizeData;
        #endregion

        #region Constructor

        public GridLayoutGroupBuilder(GridSizeData gridSizeData, GameObject gridObject)
        {
            _gridSizeData = gridSizeData;
            _gridObject = gridObject;
            Initialize();
        }

        #endregion
        
        #region Private Methods

        private void Initialize()
        {
            _gridLayoutGroup = _gridObject.GetComponent<GridLayoutGroup>();
            _rectTransform = _gridObject.GetComponent<RectTransform>();

            _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _gridLayoutGroup.constraintCount = _gridSizeData.columns;
            _gridLayoutGroup.cellSize = _gridSizeData.gridSize;
            _rectTransform.pivot = new Vector2(0.5f, 0.5f);
            _rectTransform.localScale = Vector3.one;
        }
        
        #endregion

        #region public Methods
        
        public GameObject Build()
        {
            return _gridObject;
        }
        
        #endregion

        
    }
}