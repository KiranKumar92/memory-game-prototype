using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MemoryGame.UI.FlipCard
{
    /// <summary>
    /// Handles the click event on the flip card.
    /// </summary>
    public class FlipCardClickHandler : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, results);
            float minDistance = float.MaxValue;
            GameObject closestObject = null;
            
            foreach (RaycastResult result in results)
            {
                var correctCard = result.gameObject.GetComponent<CardFlipping>();
                if (correctCard != null)
                {
                    // Calculate the distance between the click position and the object's position
                    float distance = Vector2.Distance(result.screenPosition, pointerEventData.position);

                    // Check if this object is closer than the previous closest one
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestObject = result.gameObject;
                    }
                }
            }
            if (closestObject != null)
            {
                var correctCard = closestObject.gameObject.GetComponent<CardFlipping>();
                correctCard.ActivateCardFrontFlip();
            }
        }
    }
}
