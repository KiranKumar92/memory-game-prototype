using MemoryGame.Events;
using UnityEngine;

public class ParticleEffectHandler : MonoBehaviour
{
    #region Declaration

    [SerializeField] private AudioClip succesAudio;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private Material _material;
    [SerializeField] private ParticleSystem.MinMaxGradient effectDefaultColor;
    
   
   #endregion

   #region Unity Callbacks
   private void OnEnable()
   {
      if(_particleSystem!=null)
         _particleSystem.Stop();
      EventsHandler.isColorOverLifeTime += SetColourOverLifeTime;
      EventsHandler.PlayParticleEffect += PlayEffect;
   }

   private void OnDisable()
   {
      EventsHandler.isColorOverLifeTime -= SetColourOverLifeTime;
      var effectMain = _particleSystem.main;
      effectMain.startColor = effectDefaultColor;
      EventsHandler.PlayParticleEffect -= PlayEffect;
   }
   #endregion
   #region Private Methods

   private void SetColourOverLifeTime(bool isRequired=true)
   {
      if(isRequired) return;
      var effectMain = _particleSystem.main;
      effectMain.startColor = Color.white;
   }

   
   #endregion
   #region Public Methods

   public void  PlayEffect(Sprite sprite,Transform myCurrentPostion=null)
   {
      if (myCurrentPostion != null) transform.position = myCurrentPostion.position;
      _material.mainTexture = sprite.texture;
      if (_particleSystem.isPlaying)
      {
         _particleSystem.Stop(true);
         _particleSystem.Clear();
      }
      _particleSystem.Simulate(1);
      _particleSystem.Play();
        EventsHandler.PlayAudioClip?.Invoke(succesAudio);
   }
    
   #endregion
}