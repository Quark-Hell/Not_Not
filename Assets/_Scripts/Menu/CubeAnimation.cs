using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine;


public class CubeAnimation : MonoBehaviour
{
    [System.Serializable]
    public class Piece
    {
        public Transform Object;
        public Vector3 Direction;
        public float duration;
        public float Delay;

        public Vector3 StartLocalPosition { get; set; }

        Piece()
        {
            if (Object != null)
            {
                StartLocalPosition = Object.transform.localPosition;
            }
        }
    }

    [Header("Pieces Animation")]
    [SerializeField] private Piece[] _piece;
    [SerializeField] private AnimationCurve _movingCurve;
    [SerializeField] [Range(0,10)] private float _prependInterval;
    [SerializeField] [Range(0, 1)] private float _appendInterval;

    [Header("Levitation Animation")]
    [SerializeField] private GameObject _cube;
    [SerializeField] private float _levitationSpeed;
    [SerializeField] private float _levitationAmplitude;

    [Header("Animation")]
    private Sequence _idleAnimation;
    private Tween _levitationAnimation;

    const float T = 0.05f;


    void Start()
    {
        IdleAnimation();
        Levitation();
    }

    void IdleAnimation()
    {
        _idleAnimation = DOTween.Sequence();

        _idleAnimation.Append(_piece[0].Object.DOLocalMove(Vector3.zero, 0));

        for (byte i = 0; i < _piece.Length; i++)
        {
            Vector3 endPos = _piece[i].Object.localPosition + _piece[i].Direction * T;
            _idleAnimation.Join(_piece[i].Object.DOLocalMove(endPos, _piece[i].duration));
        }

        _idleAnimation.PrependInterval(_prependInterval);
        _idleAnimation.AppendInterval(_appendInterval);
        _idleAnimation.SetLoops(-1, LoopType.Yoyo);
        _idleAnimation.SetEase(_movingCurve);
    }

   void Levitation()
    {
        float endPos = _cube.transform.position.y + _levitationAmplitude;

        _levitationAnimation = _cube.transform.DOMoveY(endPos,_levitationSpeed * Time.deltaTime);
        _levitationAnimation.SetEase(Ease.InOutSine);
        _levitationAnimation.SetLoops(-1, LoopType.Yoyo);
    }

    private AsyncOperation async;
    public void Interact(string scene)
    {
        _idleAnimation.Kill();

        for (byte i = 0; i < _piece.Length; i++)
        {

        }
            //_idleAnimation.Append(_piece[i].Object.DOLocalMove(endPos, _piece[i].duration));
        _idleAnimation.SetEase(_movingCurve);
        //SceneManager.LoadScene(scene);
        //_idleAnimation.OnStepComplete(() => LoadLevel(scene));
    }

    private void LoadLevel(string scene)
    {
        DOTween.KillAll();
        SceneManager.LoadScene(scene);
    }
}
