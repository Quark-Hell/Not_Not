using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameData _gameData;
    [SerializeField] private LightsManager _lightsManager;
    [SerializeField] private Timer _timer;

    [SerializeField] private TextMeshProUGUI CipherTMP;
    private string CipherSide;
    private GenerationSide _generationSide;
    private LanguageSettings _languageSettings;

    private void Start()
    {
        _generationSide = new GenerationSide();
        _languageSettings = new LanguageSettings();

        _languageSettings.SetLanguage(LanguagesEnum.Russian);
        CreateNewSide();
    }

    public void CreateNewSide()
    {
        print(_gameData.difficult);
        CipherSide = _generationSide.GenerateSide(_gameData.difficult, _lightsManager.lightColor, _languageSettings, out _gameData.Side, out _gameData.OneRightSide);
        CipherTMP.text = CipherSide;

        if (_gameData.difficult >= DifficultsEnum.Hard)
        {
            _lightsManager.ChangeArrowMaterials();
        }

        print(_gameData.Side);

        _timer.ResetTimerBar();
    }
    public void Loos()
    {
        print("Nope");
    }
}
