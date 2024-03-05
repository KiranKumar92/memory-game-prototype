using System;
using System.Collections;
using MemoryGame.Events;
using UnityEngine;
using UnityEngine.UI;

namespace MemoryGame.UI.FlipCard
{
    [RequireComponent(typeof(FlipCard))]
    public class CardFlipping : MonoBehaviour
    {
        #region Serialize Fields
        [SerializeField] private GameObject backFace;
        [SerializeField] private GameObject frontFace;
        [SerializeField] private Image flipCardButton;
        [Space(5)] 
        [Header("Rotation Speed")] 
        [SerializeField] private float tweenRotationSpeed = 0.2f;

        [Space(2)] [Header("Back rotation Coroutine Speed")]
        [SerializeField] private float coroutineSpeed = 0.5f;
        #endregion

        #region Private Varible
        private bool isFlipping = false; // To prevent multiple flips at once
        private FlipCard currentCard;
        #endregion

        #region Unity Callbacks
        private void OnEnable()
        {
            backFace.SetActive(false);
            frontFace.SetActive(true);
            flipCardButton.raycastTarget = true;
            EventsHandler.FlipCardMatchResult += ActivateCardBackFlip;
            EventsHandler.FlippedMemoryCard += SetButtonInteract;
        }

        private void Start()
        {
            currentCard = GetComponent<FlipCard>();
            currentCard.isBackFliped= false; // Start with the front face visible
            isFlipping = false;
        }
        private void OnDisable()
        {
            EventsHandler.FlipCardMatchResult -= ActivateCardBackFlip;
            EventsHandler.FlippedMemoryCard -= SetButtonInteract;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Set the button interactable based on the current card type
        /// </summary>
        private void SetButtonInteract(Type sentClassType, bool canReset=false)
        {
            // if (canReset)
            // {
            //     flipCardButton.raycastTarget = true;
            // }
            // else
            // {
            //     if (currentCard.cardType == sentClassType)
            //         flipCardButton.raycastTarget  = false;
            // }
            flipCardButton.raycastTarget = true;
            
        }
        
        /// <summary>
        /// Flip the card half rotation if flipping is not active
        /// </summary>
        internal void ActivateCardFrontFlip()
        {
            if (!isFlipping&& !currentCard.isBackFliped)
            {
                EventsHandler.FlippedMemoryCard?.Invoke(currentCard.cardType,false);
                isFlipping = true;
                LeanTween.rotate(gameObject, new Vector3(0, 90, 0), tweenRotationSpeed).setOnComplete(FrontFlip);
            }
        }
        
        /// <summary>
        /// Change the sprite once half rotation is done then complete the remaining rotation
        /// </summary>
        private void FrontFlip()
        {
            if(!currentCard.isBackFliped)
            {
                frontFace.SetActive(false);
                backFace.SetActive(true);
                ChangePara(() =>
                {
                    isFlipping = false;
                    if(!currentCard.onSuccessMatchCompleted)
                        EventsHandler.CurrentFlipCardMatch?.Invoke(currentCard.currentID,currentCard.cardType);
                    
                });
            }
        }
        
        /// <summary>
        /// Activate the back flip if the card is not a match
        /// </summary>
        /// <param name="isMatch"></param>
        private void ActivateCardBackFlip(bool isMatch)
        {
            if(!currentCard.isBackFliped)
                return;
            if (isMatch)
                return;
            if(isFlipping)
                return;
            isFlipping = true;
            StartCoroutine(nameof(iBackFlippingRoutine));
        }

        private IEnumerator iBackFlippingRoutine()
        {
            yield return new WaitForSeconds(coroutineSpeed);
            LeanTween.rotate(gameObject, new Vector3(0, 90, 0), tweenRotationSpeed).setOnComplete(BackFlip);
        }
        private void BackFlip()
        {
            if (!currentCard.isBackFliped) 
                return;
            frontFace.SetActive(true);
            backFace.SetActive(false);
            ChangePara();
            isFlipping = false; // Allow flipping again after the flip animation is complete
            EventsHandler.FlippedMemoryCard?.Invoke(null,true);
        }
        private void ChangePara(Action onComplete=null)
        {
            currentCard.isBackFliped = !(currentCard.isBackFliped);
            LeanTween.rotate(gameObject, new Vector3(0, 0, 0), tweenRotationSpeed).setOnComplete(() =>
            {
                isFlipping = false;
                onComplete?.Invoke();
            });
        }
        #endregion
    }
}
