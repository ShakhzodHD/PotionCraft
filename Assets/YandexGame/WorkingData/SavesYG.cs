
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Ваши сохранения

        public int goldAmount;
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

        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            _openStands[0] = true;

            _levelSpeedMovement = 0;
            _levelCapacity = 0;
            _levelSpeedAction = 0;

            _countBuyZone = 0;
            _valueCurrentBuyZone = 50;

            _completeTutorial = false;
        }
    }
}
