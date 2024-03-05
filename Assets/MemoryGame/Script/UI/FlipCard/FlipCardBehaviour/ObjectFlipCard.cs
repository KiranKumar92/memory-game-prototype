using MemoryGame.Pooling;
using UnityEngine;

namespace MemoryGame.UI.FlipCard
{
    public class ObjectFlipCard : BaseFlipCard
    {
        #region Constructor
        /// <summary>
        /// Constructor for the ObjectFlipCard
        /// </summary>
        /// <param name="matchImageSprite"></param>
        /// <param name="currentID"></param>
        /// <param name="flipCardPooling"></param>
        public ObjectFlipCard(Sprite matchImageSprite,int currentID, FlipCardPooling flipCardPooling)
        {
            FlipCardPooling = flipCardPooling;
            FlipCard = FlipCardPooling.FlipCardPool.Get();
            FlipCard.gameObject.SetActive(false);
            FlipCard.SetProperties(matchImageSprite,currentID,typeof(ObjectFlipCard));
        }
        #endregion
    }
}
