using UnityEngine;
using MemoryGame.Constants;

namespace MemoryGame.Events
{
    public class PlyerData
    {

        public void SavePlayerProgress(int levelNumber)
        {
            PlayerPrefs.SetInt(PlayerPrefConstants.PLAYER_LEVE, levelNumber);
        }

        public int GetLastPLayedLevel()
        {
            return PlayerPrefs.GetInt(PlayerPrefConstants.PLAYER_LEVE, 0);
        }
        
    }
}
