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

    [Header("Pieces Animation")]
    [SerializeField] private Piece[] _piece;
    [SerializeField] private AnimationCurve _movingCurve;

    [Header("Levitation Animation")]
    [SerializeField] private GameObject _cube;
    [SerializeField] private float _levitationSpeed;
    [SerializeField] private float _levitationAmplitude;

    const float T = 0.05f;


    void Start()
    {
        PiecesAnimation();
        Levitation();
    }

    private List<Sequence> _sequence = new List<Sequence>();

    void PiecesAnimation()
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

   void Levitation()
    {
        float endPos = _cube.transform.position.y + _levitationAmplitude;

        Tween tween = _cube.transform.DOMoveY(endPos,_levitationSpeed * Time.deltaTime);
        tween.SetEase(Ease.InOutSine);
        tween.SetLoops(-1, LoopType.Yoyo);
    }
}
