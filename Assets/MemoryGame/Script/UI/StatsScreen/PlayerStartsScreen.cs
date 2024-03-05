using MemoryGame.Events;
using UnityEngine;
using UnityEngine.UI;

namespace MemoryGame.UI.PlayerStarts
{
    public class PlayerStartsScreen
    {
        [SerializeField] Text _matchersCountText, _turnsCountText;
        private int _matchersCount = 0, _turnsCount = 0;

        #region Unity Callbacks

        private void OnEnable()
        {
            EventsHandler.FlipCardMatchResult += UpdateUI;
        }

        private void OnDisable()
        {
            EventsHandler.FlipCardMatchResult -= UpdateUI;
        }

        private void UpdateUI(bool result)
        {
            _matchersCountText.text = $"Matches\n {_turnsCount++}";
            if (result)
            {
                _matchersCountText.text = $"Matches\n {_turnsCount++}";
            }
        }

    #endregion
    }
}