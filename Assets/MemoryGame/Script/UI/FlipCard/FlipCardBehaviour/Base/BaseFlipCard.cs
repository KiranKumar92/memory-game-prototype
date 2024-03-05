using MemoryGame.Pooling;
using UnityEngine;

namespace MemoryGame.UI.FlipCard
{
    public class BaseFlipCard : IFlipCard
    {
        #region Properties
        public FlipCard FlipCard { get; set; }
        public FlipCardPooling FlipCardPooling { get; set; }
        #endregion

        #region IFlipCard Implementation
        
        /// <summary>
        /// IFlipCard interface method to get the current card
        /// </summary>
        /// <returns></returns>
        public virtual FlipCard GetCurrentCard() => FlipCard;
        
        #endregion
    }
}