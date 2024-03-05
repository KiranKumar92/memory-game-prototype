using System;
using MemoryGame.Events;
using UnityEngine;

namespace MemoryGame.UI.FlipCard
{
    [RequireComponent(typeof(FlipCard))]
    public class FlipCardComparison : MonoBehaviour
    {
        #region Private Variable
        private FlipCard _currentCard;
        #endregion
        
        #region Unity Callbacks
        private void OnEnable()
        {
            EventsHandler.CurrentFlipCardMatch += CompareCard;
            EventsHandler.FlippedMemoryCard += SetBlockCard;
        }
        private void Start()
        {
            _currentCard = GetComponent<FlipCard>();
        }
        private void OnDisable()
        {
            EventsHandler.CurrentFlipCardMatch -= CompareCard;
            EventsHandler.FlippedMemoryCard -= SetBlockCard;
        }
        #endregion

        #region Private Method
        private void CompareCard(int cardID, Type type)
        {
            if(_currentCard.onSuccessMatchCompleted)
                return;
            if (!_currentCard.isBackFliped) 
                return;
            if (cardID == _currentCard.currentID)
            {
                if (_currentCard.cardType == type)
                    return;
                EventsHandler.FlipCardMatchResult?.Invoke(true);
            }
            else
            {
                EventsHandler.FlipCardMatchResult?.Invoke(false);
            }
        }
        private void SetBlockCard(Type arg1, bool canRest)
        {
            if (canRest)
            {
                _currentCard.onSuccessMatchCompleted = false;
            }
        }
        #endregion
    }
}