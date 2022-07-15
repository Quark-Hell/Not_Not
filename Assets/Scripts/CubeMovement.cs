using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    [Range(0,10f)]
    [SerializeField] private float _speed;

    [SerializeField] private Quaternion _up;
    [SerializeField] private Quaternion _down;
    [SerializeField] private Quaternion _left;
    [SerializeField] private Quaternion _right;

    public bool IsMoving { get; private set; }

    private void Start()
    {
        IsMoving = false;
    }

    public void OnSwipeUp()
    {
        if (IsMoving == false)
        {
            IsMoving = true;
            transform.DORotateQuaternion(_up * transform.rotation, _speed).OnComplete(() => IsMoving = false);
        }
    }

    public void OnSwipeDown()
    {
        if (IsMoving == false)
        {
            IsMoving = true;
            transform.DORotateQuaternion(_down * transform.rotation, _speed).OnComplete(() => IsMoving = false);
        }
    }

    public void OnSwipeLeft()
    {
        if (IsMoving == false)
        {
            IsMoving = true;
            transform.DORotateQuaternion(_left * transform.rotation, _speed).OnComplete(() => IsMoving = false);
        }
    }

    public void OnSwipeRight()
    {
        if (IsMoving == false)
        {
            IsMoving = true;
            transform.DORotateQuaternion(_right * transform.rotation, _speed).OnComplete(() => IsMoving = false);
        }
    }
}
