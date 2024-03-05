using System;
using UnityEngine;

namespace MemoryGame.Events
{
    public class EventsHandler
    {
        //To Enable and Disable Color Over Lifetime for Different Level
        public delegate void ParticleSystemColoring(bool isRequired);
        public static ParticleSystemColoring isColorOverLifeTime;
        
        /// <summary>
        /// Invoked to play audio clip
        /// </summary>
        public delegate void PlayAudio(AudioClip audioClip);
        public static PlayAudio PlayAudioClip;
        
        #region FlipCard
        
        /// <summary>
        /// When card backface is turned this is invoked
        /// </summary>
        public static  Action<int,Type> CurrentFlipCardMatch;

        /// <summary>
        /// Invoked once match comparision is done
        /// </summary>
        public static Action<bool> FlipCardMatchResult;
        
        /// <summary>
        /// When game started it send number of match count need to performed to complete the current level
        /// </summary>
        public static Action<int> FlipCardMatchCount;

        /// <summary>
        /// Once all match is done it is called to start another level or next level
        /// </summary>
        public static Action StartTheFlipCardLevel;

        /// <summary>
        /// This will set the intractability of button if Flipped card is same type as listened card then Set button interact false else true
        /// </summary>
        public static Action<Type, bool> FlippedMemoryCard;

        /// <summary>
        /// This will be called once to increase count when match is done,, to reach the actual match
        /// </summary>
        public static Action OnSuccessMatchIncreaseCount;
        #endregion

        #region Particle Effect
        
        /// <summary>
        /// Invoke to play particle effect
        /// </summary>
        public static Action<Sprite, Transform> PlayParticleEffect;
        
        #endregion
    }
}