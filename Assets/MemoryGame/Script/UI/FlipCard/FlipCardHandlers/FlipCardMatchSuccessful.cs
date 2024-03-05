using System.Collections;
using MemoryGame.Events;
using UnityEngine;

namespace MemoryGame.UI.FlipCard
{
    [RequireComponent(typeof(FlipCard))]
    public class FlipCardMatchSuccessful : MonoBehaviour
    {
        #region Serialize Fields
        [Header("LeanTweenSpeed")]
        [SerializeField] private float fullScaleSpeed = 1f;
        [SerializeField] private float shrinkSpeed = 0.2f;

        #endregion
        
        #region Private Variables
        private FlipCard _currentCard;
        private readonly Vector2 fullScale = new Vector2(1.2f, 1.2f);
        #endregion

        #region Unity Callbacks
        private void Start() => _currentCard = GetComponent<FlipCard>();
        private void OnEnable() =>EventsHandler.FlipCardMatchResult += DisableFlipCardMatchResult;
        private void OnDisable() => EventsHandler.FlipCardMatchResult -= DisableFlipCardMatchResult;
        #endregion

        #region Private Methods
        private void DisableFlipCardMatchResult(bool obj)
        {
            if (_currentCard.isBackFliped && obj)
            {
                StartCoroutine(nameof(iDelayedCall));
                LeanTween.scale(gameObject, fullScale, fullScaleSpeed).setEaseOutBounce().setOnComplete(() =>
                {
                    LeanTween.scale(gameObject, Vector2.zero, shrinkSpeed).setOnComplete(() =>
                    {
                        if (_currentCard.gameObject.activeSelf)
                        {
                            _currentCard.ReturnToPool();
                        }
                    });
                });
            }
        }
        private IEnumerator iDelayedCall()
        {
            yield return new WaitForSeconds(1f);
            _currentCard.isBackFliped = false;
            EventsHandler.FlippedMemoryCard?.Invoke(null,true);
        }
        #endregion
    }
}
