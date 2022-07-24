using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameData GameData { get; private set; }
    private GenerationSide _generationSide;
    [SerializeField] private LightsManager _lightsManager;

    [SerializeField] private TextMeshProUGUI DifficultTMP;

    [SerializeField] private TextMeshProUGUI CipherTMP;
    private string CipherSide;

    [SerializeField] private Timer _timer;
    private LanguageSettings _languageSettings;

    private void Start()
    {
        _generationSide = new GenerationSide();
        _languageSettings = new LanguageSettings();
        GameData = new GameData();

        GameData.GameDifficult.GetXP(50);

        _languageSettings.SetLanguage(LanguagesEnum.Russian);
        CreateNewSide();
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
        DifficultTMP.text = _languageSettings.Difficult + " " + GetDifficultByIndex(indexDifficult).ToString();

        CipherTMP.text = CipherSide;
    }

    public void CreateNewSide()
    {
        CipherSide = _generationSide.GenerateSide(GameData.GameDifficult.Difficult, _lightsManager.lightColor, _languageSettings, out GameData.Side, out GameData.OneRightSide);

        if (GameData.GameDifficult.Difficult == DifficultsEnum.Hard)
        {
            _lightsManager.ChangeArrowColors();
        }
        else
        {
            _lightsManager.ResetArrowColors();
        }

        print(GameData.Side);
        print(GameData.GameDifficult.XP);

        UpdateText();
        _timer.ResetTimerBar();
    }
    public void Loos()
    {
        print("Nope");
    }
}
