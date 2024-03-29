using System;
using MemoryGame.Singleton;
using UnityEngine;

namespace MemoryGame.SFX.FlipCard
{
    [RequireComponent(typeof(AudioSource))]
    public class FlipCardAudioController : AutoSingletonManager<FlipCardAudioController>
    {
        #region Serial Field
        [SerializeField] private AudioClip matchSound, matchFailSound;
        #endregion
        
        #region Private Variable
        private AudioSource _flipCardAudioSource;
        #endregion

        #region Unity Callbacks
        private void Start() =>_flipCardAudioSource = GetComponent<AudioSource>();
        #endregion

        #region Internal Methods
        internal void PlayOneShootFlipMatchSound(bool isMatch)
        {
            _flipCardAudioSource.clip = isMatch ? matchSound : matchFailSound;
            _flipCardAudioSource.Play();
        }
        #endregion
    }
}
