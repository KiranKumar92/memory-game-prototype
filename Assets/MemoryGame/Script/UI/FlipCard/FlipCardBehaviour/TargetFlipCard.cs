using MemoryGame.Pooling;
using UnityEngine;

namespace MemoryGame.UI.FlipCard
{
    public class TargetFlipCard : BaseFlipCard
    {
        #region Constructor
        /// <summary>
        /// Constructor for the TargetFlipCard
        /// </summary>
        /// <param name="matchImageSprite"></param>
        /// <param name="currentID"></param>
        /// <param name="flipCardPooling"></param>
        public TargetFlipCard(Sprite matchImageSprite,int currentID, FlipCardPooling flipCardPooling)
        {
            FlipCardPooling = flipCardPooling;
            FlipCard = FlipCardPooling.FlipCardPool.Get();
            FlipCard.gameObject.SetActive(false);
            FlipCard.SetProperties(matchImageSprite,currentID,typeof(TargetFlipCard));
        }

        #endregion
    }
}
