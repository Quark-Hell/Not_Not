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
        DoRatate(Up);
    }

    public void OnSwipeDown()
    {
        DoRatate(Down);
    }

    public void OnSwipeLeft()
    {
        DoRatate(Left);
    }

    public void OnSwipeRight()
    {
        DoRatate(Right);
    }
}
