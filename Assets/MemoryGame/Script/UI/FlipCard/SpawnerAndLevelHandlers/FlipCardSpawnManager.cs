using System;
using System.Collections;
using System.Collections.Generic;
using MemoryGame.Events;
using MemoryGame.Pooling;
using MemoryGame.ScriptableObjectsScripts;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace MemoryGame.UI.FlipCard
{
    public class FlipCardSpawnManager : MonoBehaviour
    {
        #region Serial Fields
        [SerializeField] private GameObject memoryPanel;
        [SerializeField] private FlipCardPooling flipCardPooling;
        [SerializeField] private FlipObjectSO flipLettersSo;
        [SerializeField] private FlipObjectSO flipObjectSo;
        [Space(3)]
        [Header("Set the level")]
        [SerializeField] private int level = 1;
        #endregion

        #region Private Variable
        private IFlipCard _myCard;
        /// <summary>
        /// Maintain the Unique index for Fetching flip cards elements
        /// </summary>
        private readonly HashSet<int> _uniqueFlipCardIndex=new();

        private readonly List<FlipCard> _uniqueFlipCardSet1 = new();
        private readonly List<FlipCard> _uniqueFlipCardSet2 = new();
        

        /// <summary>
        /// Letter container Grid layout reference
        /// </summary>
        private GridLayoutGroup _memoryGrid;

        #endregion

        #region UnityCallbacks
        private void OnEnable()
        {
            _memoryGrid = memoryPanel.GetComponent<GridLayoutGroup>();
            EventsHandler.StartTheFlipCardLevel += LevelTermHandler;
            StartCoroutine(DelayedTermCall());
        }

        private void OnDisable()
        {
            EventsHandler.StartTheFlipCardLevel -= LevelTermHandler;
            level = 1;
            ClearPooledObjects(_uniqueFlipCardSet2);
            ClearPooledObjects(_uniqueFlipCardSet1);
            
        }
        #endregion
        
        #region Private Methods
        
        /// <summary>
        /// Destroying the pooled object once we move out of this scene
        /// </summary>
        /// <param name="currentPool"></param>
        private void ClearPooledObjects(List<FlipCard> currentPool)
        {
            foreach (var variable in currentPool)
            {
                Destroy(variable.gameObject);
            }
        }

        private IEnumerator DelayedTermCall()
        {
            yield return new WaitForSeconds(0.7f);
            LevelTermHandler();
        }
        [ContextMenu(nameof(LevelTermHandler))]
        private void LevelTermHandler()
        {
          
            LevelHandler(level);
        }
        
        // TODO: Refactor this method with a builder pattern
        private void LevelHandler(int term)
        {
            GridLayoutEnableDisable(true);

            switch (term)
            {
                case 1:
                    EventsHandler.FlipCardMatchCount?.Invoke(1);
                    GridAlignment(TextAnchor.MiddleCenter, new Vector2(350, 350), 200);
                    FlipCardGetter(1);
                    break;
                case 2:
                case 3:
                case 4:
                    EventsHandler.FlipCardMatchCount?.Invoke(2);
                    GridAlignment(TextAnchor.MiddleCenter, new Vector2(250, 250));
                    FlipCardGetter(2);
                    break;
                default:
                    EventsHandler.FlipCardMatchCount?.Invoke(4);
                    GridAlignment(TextAnchor.MiddleCenter, new Vector2(250, 250));
                    FlipCardGetter(4);
                    break;
            }
            level++;
        }
        
        /// <summary>
        /// Align the grid based on the number of element spawning
        /// </summary>
        /// <param name="anchor"> From where start spawning</param>
        /// <param name="cellSize">To increase and Decrease the child element size</param>
        /// <param name="leftPadding">This is Optional para</param>
        private void GridAlignment(TextAnchor anchor, Vector2 cellSize, int leftPadding=0,int topPading=0)
        {
            _memoryGrid.childAlignment = _memoryGrid.childAlignment= anchor;
            _memoryGrid.cellSize =_memoryGrid.cellSize= cellSize;
            _memoryGrid.padding.left = leftPadding;
        }
        private void FlipCardGetter(int term)
        {
           _uniqueFlipCardSet1.Clear();
           _uniqueFlipCardSet2.Clear();
           _uniqueFlipCardIndex.Clear();
            for (var    i = 0;    i < term;    i++)
            {
                GetLettersCard();
            }
            _uniqueFlipCardIndex.Clear();
            for (var    i = 0;    i < term;    i++)
            {
                GetObjectsCard();
            }
            _uniqueFlipCardIndex.Clear();
            SpawnFlipCards();
        }
        
        [ContextMenu(nameof(TestRandom))]
        private void TestRandom()
        {
            var currentElement = GetRandomUniqueElement(_uniqueFlipCardIndex);
        }
        private void GetLettersCard()
        {
            var currentLetter = GetRandomUniqueElement(_uniqueFlipCardIndex);
            _myCard = new TargetFlipCard(currentLetter.fObjectSprite, currentLetter.fObjectID, flipCardPooling);
            var currentCard=_myCard.GetCurrentCard();
            _uniqueFlipCardSet1.Add(currentCard);
        }

        private void GetObjectsCard()
        {
            var flipCardObjects = flipObjectSo.fObjects;
            for (int i = 0; i < flipCardObjects.Length; i++)
            {
                for (int    j = 0;    j < _uniqueFlipCardSet1.Count;    j++)
                {
                    if (flipCardObjects[i].fObjectID == _uniqueFlipCardSet1[j].currentID)
                    {
                        if (_uniqueFlipCardIndex.Contains((int)flipCardObjects[i].fObjectID))
                        {
                            break;
                        }
                        
                        _myCard = new ObjectFlipCard(flipCardObjects[i].fObjectSprite, flipCardObjects[i].fObjectID,flipCardPooling);
                        var currentCard = _myCard.GetCurrentCard();
                       
                        
                        //Store Required elements in list and HasSet
                        _uniqueFlipCardIndex.Add((int)flipCardObjects[i].fObjectID);
                        _uniqueFlipCardSet2.Add(currentCard);
                        
                    }
                }
            }
        }

        private void SpawnFlipCards()
        {
            SpawnLetters(() => SpawnObjects( () =>
            {
                ShuffleUIElements();
                GridLayoutEnableDisable(false);
            }));
        }
 
        private void SpawnLetters(Action onComplete)
        {
            StartCoroutine(SequentialSpawn(_uniqueFlipCardSet1, memoryPanel.transform,onComplete));
        }
        private void SpawnObjects(Action onComplete)
        {
            _uniqueFlipCardSet2.Shuffle();
            StartCoroutine(SequentialSpawn(_uniqueFlipCardSet2, memoryPanel.transform, onComplete));
        }
        private IEnumerator SequentialSpawn(List<FlipCard> objects, Transform parent, Action onComplete = null)
        {
            foreach (var obj in objects)
            {
                obj.transform.SetParent(parent);
                obj.gameObject.SetActive(true);
                obj.transform.localScale = Vector3.zero;
                LeanTween.scale(obj.gameObject, Vector3.one, 0.5f).setEaseOutQuint();
                // StartCoroutine(ScaleObjectsRoutine(obj.transform, Vector3.zero, Vector3.one));

                // Increase the delay for the next object
                yield return new WaitForSeconds(0.3f);
            }

            // Check if there is a callback to be executed
            if (onComplete != null)
            {
                onComplete.Invoke();
            }
        }
        
        public void ShuffleUIElements()
        {
            List<RectTransform> shuffledElements = new List<RectTransform>(_memoryGrid.GetComponentsInChildren<RectTransform>());
            ShuffleList(shuffledElements);

            for (int i = 0; i < shuffledElements.Count; i++)
            {
                // Randomly choose a position that is not already occupied
                int newIndex = Random.Range(i, shuffledElements.Count);
                SwapElements(shuffledElements, i, newIndex);
            }
        }

        private void SwapElements(List<RectTransform> elements, int indexA, int indexB)
        {
            RectTransform temp = elements[indexA];
            elements[indexA] = elements[indexB];
            elements[indexB] = temp;
        }

        private void ShuffleList<T>(List<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int randomIndex = Random.Range(i, list.Count);
                T temp = list[i];
                list[i] = list[randomIndex];
                list[randomIndex] = temp;
            }
        }
        private void GridLayoutEnableDisable(bool isShouldActive)
        {
            _memoryGrid.enabled = isShouldActive;
        }
        
        private FlipCardObjectData GetRandomUniqueElement(HashSet<int> usedIndices)
        {
            var scriptableObjectLength = flipLettersSo.fObjects.Length;
            if (scriptableObjectLength == 0)
                return null;

            if (usedIndices.Count == scriptableObjectLength)
            {
                usedIndices.Clear();
            }
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, scriptableObjectLength);
            } while (usedIndices.Contains(randomIndex));
        
            usedIndices.Add(randomIndex);
            return flipLettersSo.fObjects[randomIndex];
        }
        
        #endregion
        
    }
}
