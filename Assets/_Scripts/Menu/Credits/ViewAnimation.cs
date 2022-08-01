using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ViewAnimation : MonoBehaviour
{
    [SerializeField] private float _animationDuration;
    [SerializeField] private float _yShift;
    [SerializeField] private float _animationDelay;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _music;

    void Start()
    {
        Animation();
    }

    void Animation()
    {
        transform.DOLocalMoveY(_yShift, _animationDuration * Time.deltaTime).SetDelay(_animationDelay);
    }
}
