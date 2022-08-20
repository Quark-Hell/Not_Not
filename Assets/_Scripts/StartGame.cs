using DG.Tweening;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private Volume _volume;
    [SerializeField] private CubeEffects _cubeEffects;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Timer _timer;

    public bool IsStartingGame;

    [Header("Black And White Effect")]
    [SerializeField] private float _zero;
    [SerializeField] private float _normal;
    [SerializeField] private float _grayscaleDuration;

    [Header("Get Ready Text")]
    [SerializeField] private TextMeshProUGUI _getReadyTMP;
    [SerializeField] private TextMeshProUGUI _getReady2TMP;
    [SerializeField] private float _normalSize;
    [SerializeField] private float _changeScaleDuration;

    [Header("Game Over Text")]
    [SerializeField] private TextMeshProUGUI _gameOver;


    private void Awake()
    {
        LanguageSettings.LoadLanguage();
        LanguageSettings.SetLanguage(LanguageSettings.Languages);
        Money.LoadMoney();
    }

    private void Start()
    {
        _gameOver.text = LanguageSettings.GameOver;

        _getReadyTMP.text = LanguageSettings.GetReady;
        _getReady2TMP.text = LanguageSettings.GetReady;

        _getReadyTMP.DOFontSize(_normalSize, _changeScaleDuration).SetEase(Ease.OutBack);
        GrayscaleEffect();
    }

    private void GrayscaleEffect()
    {
        DOVirtual.Float(_zero, _normal, _grayscaleDuration, t =>
        {

        }).SetEase(Ease.InSine).OnComplete(() => StartGameAnimations());
    }

    private void StartGameAnimations()
    {
        IsStartingGame = true;

        _gameManager.CreateNewSide();
        _getReadyTMP.DOFontSize(0, _changeScaleDuration).SetEase(Ease.InBack);
        _cubeEffects.Levitation();
    }
}
