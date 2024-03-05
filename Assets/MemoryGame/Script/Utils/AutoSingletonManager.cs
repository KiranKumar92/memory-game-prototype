using UnityEngine;
using System;

namespace MemoryGame.Singleton
{
    /// <summary>
    /// Lets you ensure that a class has only one instance, while providing a global access point to this instance.
    /// </summary>
    public abstract class AutoSingletonManager : MonoBehaviour
    {

    }

    public abstract class AutoSingletonManager<T> : AutoSingletonManager where T : AutoSingletonManager
    {
        private static bool Compare<U>(U x, U y) where U : class
        {
            return x == y;
        }

        #region SINGLETON

        private static T _instance = default(T);

        public static T Instance
        {
            get
            {
                if (!Compare<T>(default(T), _instance))
                    return _instance;

                InitInstance(true);
                return _instance;
            }
        }

        #endregion
        
        #region PUBLIC METHODS
        public virtual void Awake() => InitInstance(false);

        public static void InitInstance(bool shouldInitManager)
        {
            Type thisType = typeof(T);

            _instance = FindObjectOfType<T>();

            if (Compare<T>(default(T), _instance))
            {
                _instance = new GameObject(thisType.Name).AddComponent<T>();
            }

            //Won't call InitManager from Awake
            if (shouldInitManager)
            {
                (_instance as AutoSingletonManager<T>).InitManager();
            }
        }

        public virtual void InitManager()
        {
        }

        #endregion
    }
}