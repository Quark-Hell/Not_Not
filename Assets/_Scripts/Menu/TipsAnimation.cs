using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine;

public class TipsAnimation : MonoBehaviour
{
    [SerializeField] private Transform _upArrow;
    [SerializeField] private Transform _downArrow;
    [SerializeField] private Transform _leftArrow;
    [SerializeField] private Transform _rightArrow;

    [SerializeField] private float _amplitude;
    [SerializeField] private float _duration;
    [SerializeField] private int _vibratio;

    [Header("Fade Animation")]
    [SerializeField] private float _fadeDuration;
    [Range(0, 1)][SerializeField] private float _showAlpha;
    [Range(0, 1)][SerializeField] private float _hideAlpha;
    [Range(0, 3)][SerializeField] private float _delay;

    [SerializeField] CubeButtons _cubeButtons;

    private Sequence[] _arrowsAnim = new Sequence[4];

    void Start()
    {
        CreateSequence();
    }

    bool IsDelaying(Sequence sequence, float delay)
    {
        if (sequence.Duration(false) * sequence.ElapsedDirectionalPercentage() - delay >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void CreateSequence()
    {
        for (byte i = 0; i < _arrowsAnim.Length; i++)
        {
            _arrowsAnim[i] = DOTween.Sequence();
        }

        _arrowsAnim[0].Append(_upArrow.DOPunchPosition(Vector3.up * _amplitude, _duration, _vibratio));
        _arrowsAnim[1].Join(_downArrow.DOPunchPosition(Vector3.down * _amplitude, _duration, _vibratio));
        _arrowsAnim[2].Join(_leftArrow.DOPunchPosition(Vector3.left * _amplitude, _duration, _vibratio));
        _arrowsAnim[3].Join(_rightArrow.DOPunchPosition(Vector3.right * _amplitude, _duration, _vibratio));

        for (byte i = 0; i < _arrowsAnim.Length; i++)
        {
            _arrowsAnim[i].SetEase(Ease.InOutSine);
            _arrowsAnim[i].SetDelay(_delay);
            _arrowsAnim[i].SetLoops(-1, LoopType.Restart);
        }
    }

    public void ArrowFade()
    {
        switch (_cubeButtons.CurrentButton)
        {
            case Vector2 v when v == Vector2.zero:
                _upArrow.gameObject.GetComponent<Image>().DOFade(_showAlpha, _fadeDuration);
                _downArrow.gameObject.GetComponent<Image>().DOFade(_showAlpha, _fadeDuration);
                _leftArrow.gameObject.GetComponent<Image>().DOFade(_showAlpha, _fadeDuration);
                _rightArrow.gameObject.GetComponent<Image>().DOFade(_showAlpha, _fadeDuration);
                break;

            case Vector2 v when v == Vector2.up:
                _upArrow.gameObject.GetComponent<Image>().DOFade(_showAlpha, _fadeDuration);
                _downArrow.gameObject.GetComponent<Image>().DOFade(_hideAlpha, _fadeDuration);
                _leftArrow.gameObject.GetComponent<Image>().DOFade(_hideAlpha, _fadeDuration);
                _rightArrow.gameObject.GetComponent<Image>().DOFade(_hideAlpha, _fadeDuration);
                break;

            case Vector2 v when v == Vector2.down:
                _upArrow.gameObject.GetComponent<Image>().DOFade(_hideAlpha, _fadeDuration);
                _downArrow.gameObject.GetComponent<Image>().DOFade(_showAlpha, _fadeDuration);
                _leftArrow.gameObject.GetComponent<Image>().DOFade(_hideAlpha, _fadeDuration);
                _rightArrow.gameObject.GetComponent<Image>().DOFade(_hideAlpha, _fadeDuration);
                break;

            case Vector2 v when v == Vector2.left:
                _upArrow.gameObject.GetComponent<Image>().DOFade(_hideAlpha, _fadeDuration);
                _downArrow.gameObject.GetComponent<Image>().DOFade(_hideAlpha, _fadeDuration);
                _leftArrow.gameObject.GetComponent<Image>().DOFade(_showAlpha, _fadeDuration);
                _rightArrow.gameObject.GetComponent<Image>().DOFade(_hideAlpha, _fadeDuration);
                break;

            case Vector2 v when v == Vector2.right:
                _upArrow.gameObject.GetComponent<Image>().DOFade(_hideAlpha, _fadeDuration);
                _downArrow.gameObject.GetComponent<Image>().DOFade(_hideAlpha, _fadeDuration);
                _leftArrow.gameObject.GetComponent<Image>().DOFade(_hideAlpha, _fadeDuration);
                _rightArrow.gameObject.GetComponent<Image>().DOFade(_showAlpha, _fadeDuration);
                break;
        }
    }

    public void ArrowMove()
    {
        switch (_cubeButtons.CurrentButton)
        {
            case Vector2 v when v == Vector2.zero:
                //play all
                foreach (Sequence s in _arrowsAnim)
                {
                    s.Restart();
                    s.Play();
                    s.OnStepComplete(() => s.Restart());

                }
                break;

            case Vector2 v when v == Vector2.down:
                foreach (Sequence s in _arrowsAnim)
                {
                    //Stop all except up arrow
                    if (s != _arrowsAnim[1]) 
                    {
                        if (IsDelaying(s, _delay)) { s.OnStepComplete(() => s.Pause()); }
                        else { s.Pause(); }
                    }
                }
                break;

            case Vector2 v when v == Vector2.up:
                foreach (Sequence s in _arrowsAnim)
                {
                    //Stop all except down arrow
                    if (s != _arrowsAnim[0])
                    {
                        if (IsDelaying(s, _delay)) { s.OnStepComplete(() => s.Pause()); }
                        else { s.Pause(); }
                    }
                }
                break;

            case Vector2 v when v == Vector2.left:
                foreach (Sequence s in _arrowsAnim)
                {
                    //Stop all except right arrow
                    if (s != _arrowsAnim[2])
                    {
                        if (IsDelaying(s, _delay)) { s.OnStepComplete(() => s.Pause()); }
                        else { s.Pause(); }
                    }
                }
                break;

            case Vector2 v when v == Vector2.right:
                foreach (Sequence s in _arrowsAnim)
                {
                    //Stop all except left arrow
                    if (s != _arrowsAnim[3])
                    {
                        if (IsDelaying(s, _delay)) { s.OnStepComplete(() => s.Pause()); }
                        else { s.Pause(); }
                    }
                }
                break;
        }
    }
}
