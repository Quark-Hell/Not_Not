using System.Collections;
using System.Collections.Generic;
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

    public bool IsStartingGame { get; private set;}

    [Header("Black And White Effect")]
    [SerializeField] private float _zero;
    [SerializeField] private float _normal;
    [SerializeField] private float _grayscaleDuration;
    private ColorAdjustments _colorAdjustments;

    [Header("Get Ready Text")]
    [SerializeField] private TextMeshProUGUI _getReady;
    [SerializeField] private float normalSize;
    [SerializeField] private float _changeScaleDuration;

    void Start()
    {
        _getReady.DOFontSize(normalSize, _changeScaleDuration).SetEase(Ease.OutBack);
        GrayscaleEffect();
    }

    private void GrayscaleEffect()
    {
        if (_volume.profile.TryGet<ColorAdjustments>(out _colorAdjustments))
        {
            ClampedFloatParameter tmp = _colorAdjustments.saturation;

            DOVirtual.Float(_zero, _normal, _grayscaleDuration, t =>
            {
                tmp.value = t;
                _colorAdjustments.saturation = tmp;
            }).SetEase(Ease.InSine).OnComplete(() => StartGameAnimations()) ;
        }
    }

    private void StartGameAnimations()
    {
        IsStartingGame = true;

        _gameManager.CreateNewSide();
        _getReady.DOFontSize(0, _changeScaleDuration).SetEase(Ease.InBack);
        _cubeEffects.Levitation();
    }
}
