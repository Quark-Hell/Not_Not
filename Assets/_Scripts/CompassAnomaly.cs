using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CompassAnomaly : MonoBehaviour
{
    [SerializeField] AnimationCurve _RotateCurve;
    [SerializeField] private float _arrowSpeed;
    [SerializeField] private float _minFrequencyChangingDirection;
    [SerializeField] private float _maxFrequencyChangingDirection;

    private float _frequency;

    private float _punchDirection;
    private float _elased;
    void Start()
    {
        _elased = 0;
        _punchDirection = GetNewPunchDirection();
        _frequency = Random.Range(_minFrequencyChangingDirection,_maxFrequencyChangingDirection);
        ArrowMovement();
    }

    private void Update()
    {
        Timer();
        ArrowMovement();
    }

    float GetNewPunchDirection()
    {
        _frequency = Random.Range(_minFrequencyChangingDirection, _maxFrequencyChangingDirection);

        // 0 = false, 1 = true
        bool isNegative = Random.Range(0, 2) == 1;

        float punch = Random.Range(15, 30);
        if (isNegative)
        {
            punch = -punch;
        }

        return punch;
    }

    void Timer()
    {
        if (_elased + _arrowSpeed * Time.deltaTime <= _frequency)
        {
            _elased += Time.deltaTime;
        }
        else
        {
            _elased = 0;
            _punchDirection = GetNewPunchDirection();
        }
    }

    void ArrowMovement()
    {
        transform.Rotate(0, 0, _punchDirection * _arrowSpeed * Time.deltaTime * _RotateCurve.Evaluate(_elased / _frequency));

        //transform.DOPunchRotation(new Vector3(0, 0, _punchDirection), _arrowSpeed * Time.deltaTime * _RotateCurve.Evaluate(1)).OnComplete(() => ArrowMovement());
    }
}
