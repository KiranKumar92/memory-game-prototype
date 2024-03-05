using MemoryGame.UI.FlipCard;
using UnityEngine;
using UnityEngine.Pool;

namespace MemoryGame.Pooling
{
    public class FlipCardPooling : MonoBehaviour
    {
        #region Serialize Field
        [SerializeField] private FlipCard flipCardPrefab;
        #endregion

        #region Private Varible
        private ObjectPool<FlipCard> _flipCardPool;
        #endregion

        #region Properties
        public ObjectPool<FlipCard> FlipCardPool => _flipCardPool;
        
        #endregion

        #region Unity Callbacks

        private void OnEnable() => _flipCardPool = new ObjectPool<FlipCard>(Create, TakeFromPool, ReturnBackToPool, DestroyThePooledObject, true, 8, 10);

        #endregion

        #region Private Methods

        /// <summary>
        /// Create the object for the pool.
        /// </summary>
        /// <returns></returns>
        private FlipCard Create()
        {
            var flipCard = Instantiate(flipCardPrefab,transform);
            flipCard.SetPool(_flipCardPool,flipCardPrefab.transform);
            return flipCard;
        }
        
        /// <summary>
        /// Get the object from the pool.
        /// </summary>
        /// <param name="obj"></param>
        private void TakeFromPool(FlipCard obj) => obj.gameObject.SetActive(true);

        /// <summary>
        /// Return the object back to the pool.
        /// </summary>
        /// <param name="obj"></param>
        private void ReturnBackToPool(FlipCard obj)
        {
            obj.transform.SetParent(transform);
            obj.gameObject.SetActive(false);
        }
        
        /// <summary>
        /// Destroy the pooled object.
        /// </summary>
        /// <param name="obj"></param>
        private void DestroyThePooledObject(FlipCard obj) => Destroy(obj.gameObject);
        #endregion
    }
}
