using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    [Range(0,10f)]
    public float _speed;

    public Quaternion Up;
    public Quaternion Down;
    public Quaternion Left;
    public Quaternion Right;

    public int MaxSwipeToUp;
    public int MaxSwipeToDown;
    public int MaxSwipeToLeft;
    public int MaxSwipeToRight;

    public int CountSwipeToUp { get; private set; }
    public int CountSwipeToDown { get; private set; }
    public int CountSwipeToLeft { get; private set; }
    public int CountSwipeToRight { get; private set; }

    public bool IsMoving { get; private set; }

    private void Start()
    {
        IsMoving = false;
    }

    private void DoRatate(Quaternion rotate)
    {
        if (IsMoving == false)
        {
            IsMoving = true;
            transform.DORotateQuaternion(rotate * transform.rotation, _speed).OnComplete(() => IsMoving = false);
        }
    }

    public void OnSwipeUp()
    {
        if (CountSwipeToUp < MaxSwipeToUp || CountSwipeToUp < 0)
        {
            CountSwipeToUp++;
            CountSwipeToDown--;

            DoRatate(Up);
        }
    }

    public void OnSwipeDown()
    {
        if (CountSwipeToDown < MaxSwipeToDown || MaxSwipeToDown < 0)
        {
            CountSwipeToDown++;
            CountSwipeToUp--;

            DoRatate(Down);
        }
    }

    public void OnSwipeLeft()
    {
        if (CountSwipeToLeft < MaxSwipeToLeft || MaxSwipeToLeft < 0)
        {
            CountSwipeToLeft++;
            CountSwipeToRight--;

            DoRatate(Left);
        }
    }

    public void OnSwipeRight()
    {
        if (CountSwipeToRight < MaxSwipeToRight || MaxSwipeToRight < 0)
        {
            CountSwipeToRight++;
            CountSwipeToLeft--;

            DoRatate(Right);
        }
    }
}
