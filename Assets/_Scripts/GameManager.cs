using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Color32[] ColorListForBackround;
    public RawImage Background;

    public GameData GameData { get; private set; }
    public CubeEffects _cubeEffects;
    private GenerationSide _generationSide;
    [SerializeField] private LightsManager _lightsManager;

    [SerializeField] private TextMeshProUGUI DifficultTMP;

    [SerializeField] private TextMeshProUGUI ScoreTMP;

    [SerializeField] private TextMeshProUGUI CipherTMP;
    private string CipherSide;

    [SerializeField] private TextMeshProUGUI HealthTMP;

    public TimerGUI _timerGUI;

    [SerializeField] private GameObject _loosMenu;

    [Header("Black And White Effect")]
    [SerializeField] private StartGame _startGame;
    [SerializeField] private float _zero;
    [SerializeField] private float _normal;
    [SerializeField] private float _grayscaleDuration;

    private void Start()
    {
        _generationSide = new GenerationSide();
        GameData = new GameData();

        //GameData.GameDifficult.GetXP(50);//Cheat
        GameData.PlayerHealth.Hit(-3);

        LanguageSettings.LoadLanguage();
        LanguageSettings.SetLanguage(LanguageSettings.Languages);

        Money.LoadMoney();

        _timerGUI._timer.EndTimer += WrongSide;
    }

    private int GetIndexOfDifficult(DifficultsEnum sidesEnum)
    {
        System.Array values = System.Enum.GetValues(typeof(DifficultsEnum));
        return System.Array.IndexOf(DifficultsEnum.GetValues(sidesEnum.GetType()), sidesEnum);
    }

    private DifficultsEnum GetDifficultByIndex(int index)
    {
        System.Array values = System.Enum.GetValues(typeof(DifficultsEnum));
        return (DifficultsEnum)values.GetValue(index);
    }

    private void UpdateText()
    {
        int indexDifficult = GetIndexOfDifficult(GameData.GameDifficult.Difficult);
        DifficultTMP.text = LanguageSettings.Difficult + " " + LanguageSettings.DifficultTypes[indexDifficult];

        if (GameData.GameDifficult.Difficult >= DifficultsEnum.Hard)
        {
            _lightsManager.ChangeTextColors(CipherTMP);
        }

        ScoreTMP.text = GameData.GameDifficult.XP.ToString();

        CipherTMP.text = CipherSide;
    }

    public void CreateNewSide()
    {
        CipherSide = _generationSide.GenerateSide(GameData.GameDifficult.Difficult, _lightsManager.lightColor, out GameData.Side, out GameData.OneRightSide);

        if (GameData.GameDifficult.Difficult >= DifficultsEnum.Hard)
        {
            _lightsManager.ChangeArrowColors();
        }
        else
        {
            _lightsManager.ResetArrowColors();
        }

        print(GameData.Side);

        UpdateText();
        _timerGUI.ResetTimerBar();
    }

    public void WrongSide()
    {
        if (GameData.PlayerHealth.HealthPoints <= 0)
        {
            Loos();
            return;
        }

        _cubeEffects.Shake();
        GameData.PlayerHealth.Hit(1);
        HealthTMP.text = GameData.PlayerHealth.HealthPoints.ToString();
        CreateNewSide();
    }

    private void Loos()
    {
        _timerGUI._timer.EndTimer -= WrongSide;
        _timerGUI._timer.EndTimer -= _timerGUI.ResetTimerBar;
        _timerGUI._timer.CompleteTimer();

        _startGame.IsStartingGame = false;

        _loosMenu.SetActive(true);
        _cubeEffects.StopLevitation();
        _cubeEffects.FallCube();
        print("Nope");
    }
}
