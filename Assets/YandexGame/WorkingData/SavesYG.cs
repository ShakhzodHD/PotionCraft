
using UnityEngine.UI;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // Tech saves (dont remove)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Saves

        public int _goldAmount;
        public bool[] _openPlants = new bool[3];
        public bool[] _openCrafts = new bool[4]; 
        public bool[] _openStands = new bool[4];
        public bool[] _openRecruit = new bool[5];

        public bool[] _openDecor = new bool[4];

        public int _levelSpeedMovement;
        public int _levelCapacity;
        public int _levelSpeedAction;

        public int _countBuyZone;
        public int _valueCurrentBuyZone;

        public bool _completeTutorial;

        public bool[] _buttonEffectsUnlocked = new bool[3];
        public int _activeButtonIndex;

        public bool _isWings;

        public int _levelSpawn;

        //metrics
        public bool[] _isFirstClickButtons = new bool[3]; //oder buttonts: custom, decor, recipe 

        // Init
        public SavesYG()
        {
            _goldAmount = 100;

            _openStands[0] = true;

            _levelSpeedMovement = 0;
            _levelCapacity = 0;
            _levelSpeedAction = 0;

            _countBuyZone = 0;
            _valueCurrentBuyZone = 50;

            _completeTutorial = false;

            _activeButtonIndex = -1;

            _isWings = false;

            _levelSpawn = 0;

            for (int i = 0; i< _isFirstClickButtons.Length; i++)
            {
                _isFirstClickButtons[i] = true;
            }
        }
    }
}
