using UnityEngine.UI;
using TMPro;
using DG.Tweening;
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

    [SerializeField] private StartGame _startGame;

    [Header("Inverting Time Text")]
    [SerializeField] private TextMeshProUGUI _inversionTMP;
    [SerializeField] private float _normalSize;
    [SerializeField] private float _changeScaleDuration;

    [SerializeField] private LooseMenu _looseMenu;

    private void Start()
    {
        _generationSide = new GenerationSide();
        GameData = new GameData();

        //GameData.GameDifficult.GetXP(50);//Cheat
        GameData.PlayerHealth.Hit(-3);

        _timerGUI._timer.EndTimer += WrongSide;

        UpdateText();
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

        GameData.GameDifficult.ChangedToHardPlus += InversionTextAniamtion;
    }

    private void InversionTextAniamtion()
    {
        _inversionTMP.text = LanguageSettings.Inversion;
        _inversionTMP.DOFontSize(_normalSize, _changeScaleDuration).SetEase(Ease.OutBack).OnComplete(() => _inversionTMP.DOFontSize(0, _changeScaleDuration).SetEase(Ease.InBack));
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
            Lose();
            return;
        }

        _cubeEffects.Shake();
        GameData.PlayerHealth.Hit(1);
        HealthTMP.text = GameData.PlayerHealth.HealthPoints.ToString();
        CreateNewSide();
    }

    private void Lose()
    {
        _timerGUI._timer.EndTimer -= WrongSide;
        _timerGUI._timer.EndTimer -= _timerGUI.ResetTimerBar;
        _timerGUI._timer.CompleteTimer();

        _startGame.IsStartingGame = false;

        _loosMenu.SetActive(true);
        _cubeEffects.StopLevitation();
        _cubeEffects.FallCube();

        _looseMenu.Lose(GameData.GameDifficult.XP, 100);
        print("Nope");
    }
}
