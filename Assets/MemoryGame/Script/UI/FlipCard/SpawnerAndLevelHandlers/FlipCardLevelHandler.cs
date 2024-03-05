using System.Collections;
using MemoryGame.Events;
using UnityEngine;

namespace MemoryGame.UI.FlipCard
{
    public class FlipCardLevelHandler : MonoBehaviour
    {
        #region Private Variable

        private int _actualCount;
        private int _currentMatchCount;

        #endregion
        
        #region Unity Callbacks
        private void OnEnable()
        {
            EventsHandler.FlipCardMatchCount += SetActualMatchCount;
            EventsHandler.OnSuccessMatchIncreaseCount += CompareCurrentCount;
        }
        private void OnDisable()
        {
            EventsHandler.FlipCardMatchCount -= SetActualMatchCount;
            EventsHandler.OnSuccessMatchIncreaseCount -= CompareCurrentCount;
            _currentMatchCount = 0;
            _actualCount = 0;
        }
        #endregion

        #region Private Methods
        private void SetActualMatchCount(int actualCount)
        {
            _actualCount = actualCount;
        }

        private void CompareCurrentCount()
        {
            _currentMatchCount++;
            if (_currentMatchCount == _actualCount)
            {
                _currentMatchCount = 0;
                _actualCount = 0;
                StartCoroutine(StartAnotherLevelRoutine());
            }
        }

        private IEnumerator StartAnotherLevelRoutine()
        {
            yield return new WaitForSeconds(2f);
            EventsHandler.StartTheFlipCardLevel?.Invoke();
        }

        #endregion
    }
}