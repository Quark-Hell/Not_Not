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
    public bool IsStartingGame { get; private set;}

    [Header("Black And White Effect")]
    [SerializeField] private float _zero;
    [SerializeField] private float _normal;
    [SerializeField] private float _grayscaleDuration;
    private ColorAdjustments _colorAdjustments;

    void Start()
    {
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

        _cubeEffects.Levitation();
    }
}
