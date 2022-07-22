using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public class MainMenuCubeAnimation : MonoBehaviour
{
    [System.Serializable]
    public class Piece
    {
        public Transform Object;
        public Vector3 Direction;
        public float duration;
        public float Delay;
    }

    [SerializeField] private Piece[] _piece;
    [SerializeField] private AnimationCurve _movingCurve;

    const float T = 0.05f;


    void Start()
    {
        CreateTween();
    }

    private List<Sequence> _sequence = new List<Sequence>();

    void CreateTween()
    {
        _sequence.Clear();

        for (byte i = 0; i < _piece.Length; i++)
        {
            Vector3 endPos = _piece[i].Object.localPosition + _piece[i].Direction * T;

            Sequence s = DOTween.Sequence();
            _sequence.Add(s);

            s.SetDelay(5f);
            s.Append(_piece[i].Object.DOLocalMove(endPos, _piece[i].duration));
            s.AppendInterval(0.5f);
            s.SetLoops(-1, LoopType.Yoyo);
            s.SetEase(_movingCurve);
        }
    }
}
