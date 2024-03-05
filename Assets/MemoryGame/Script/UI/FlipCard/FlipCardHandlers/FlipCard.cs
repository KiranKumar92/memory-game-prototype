using System;
using MemoryGame.Events;
using MemoryGame.SFX.FlipCard;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

namespace MemoryGame.UI.FlipCard
{
    public class FlipCard : MonoBehaviour
    {
        #region Serialize Field
        [SerializeField] private Image matchImage;
        [SerializeField] internal int currentID;
        #endregion

        #region Private Variable for Pooling
        
        private ObjectPool<FlipCard> _flipCardPool;
        private Transform _transform;
        
        #endregion

        #region Internal Variable
        internal Type cardType;
        internal bool isBackFliped;
        internal bool onSuccessMatchCompleted;
        #endregion

        #region Unity Callbacks
        private void OnEnable()
        {
            onSuccessMatchCompleted = false;
            EventsHandler.FlipCardMatchResult += PlaySuccessfulEvent;
        }
        private void OnDisable() => EventsHandler.FlipCardMatchResult -= PlaySuccessfulEvent;
        #endregion

        #region Public Methods
        public void SetProperties(Sprite matchImageSprite,int currentID,Type classType)
        {
            matchImage.sprite = matchImageSprite;
            this.currentID = currentID;
            this.cardType = classType;
        }
        #endregion

        #region Internal methods for Pooling
        
        /// <summary>
        /// Set the pool and transform for the flip card
        /// </summary>
        /// <param name="pool"></param>
        /// <param name="myTransform"></param>
        internal void SetPool(ObjectPool<FlipCard> pool,Transform myTransform)
        {
            this._flipCardPool = pool;
            _transform = myTransform;
        }
        
        /// <summary>
        /// Return the flip card to the pool
        /// </summary>
        internal void ReturnToPool()
        {
            _flipCardPool.Release(this);
            transform.localScale = _transform.localScale;
        }
        #endregion

        #region Private Method
        private void PlaySuccessfulEvent(bool isMatch)
        {
            if (!isBackFliped)
                return;
            if (!isMatch)
            {
                FlipCardAudioController.Instance.PlayOneShootFlipMatchSound(false);
                return;
            }
            
            //Each card should have only one success call
            if (onSuccessMatchCompleted)
                return;
            onSuccessMatchCompleted = true;
            if (cardType == typeof(TargetFlipCard))
            {
                EventsHandler.PlayParticleEffect?.Invoke(matchImage.sprite, null);
                EventsHandler.OnSuccessMatchIncreaseCount?.Invoke();
                FlipCardAudioController.Instance.PlayOneShootFlipMatchSound(true);
            }

        }
        #endregion
    }
}