using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine;


public class CubeEffects : MonoBehaviour
{
    [System.Serializable]
    public class Piece
    {
        public Transform Object;
        public Vector3 Direction;

        public Vector3 StartLocalPosition { get; set; }

        Piece()
        {
            if (Object != null)
            {
                StartLocalPosition = Object.transform.localPosition;
            }
        }
    }

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private MenuManager _menuManager;

    [Header("Effects")]
    [SerializeField] private bool _hasIdleAnimation;
    [SerializeField] private bool _hasLevitationAnimation;

    [Header("Pieces Animation")]
    [SerializeField] private Piece[] _piece;
    [SerializeField] [Range(0,10)] private float _startInterval;
    [SerializeField] [Range(0, 3)] private float _endInterval;
    [SerializeField][Range(0, 3)] private float _idleDuration;

    [Header("Levitation Animation")]
    [SerializeField] private GameObject _cube;
    [SerializeField] private float _levitationSpeed;
    [SerializeField] private float _levitationAmplitude;
    private float _startYPos;

    [Header("Blind Animation")]
    [SerializeField] private Image _blindImage;
    [SerializeField] [Range(0, 255)] private float _maxAlpha;
    [SerializeField] [Range(0, 3)] private float _blindingDuration;
    [SerializeField] private AudioClip _blindingAudio;

    [Header("Shake Animation")]
    [SerializeField] private float _shakeDuration;
    [SerializeField] private AudioClip _shakeAudio;

    [Header("Fall Cube")]
    [SerializeField] private float _fallRange;
    [SerializeField] private float _fallDuration;

    [Header("Animation")]
    private Sequence _idleAnimation;
    private Tween _levitationAnimation;
    private Tween _shakeAnimation;

    const float T = 0.05f;

    void Start()
    {
        if(_hasIdleAnimation)
        IdleAnimation();

        if(_hasLevitationAnimation)
        Levitation();
    }

    public void IdleAnimation()
    {
        _idleAnimation = DOTween.Sequence();
        _idleAnimation.PrependInterval(_startInterval);

        Vector3 endPos = _piece[0].Object.localPosition + _piece[0].Direction * T;
        _idleAnimation.Append(_piece[0].Object.DOLocalMove(endPos, _idleDuration));

        for (byte i = 1; i < _piece.Length; i++)
        {
            endPos = _piece[i].Object.localPosition + _piece[i].Direction * T;
            _idleAnimation.Join(_piece[i].Object.DOLocalMove(endPos, _idleDuration));
        }

        _idleAnimation.AppendInterval(_endInterval);
        _idleAnimation.SetLoops(-1, LoopType.Yoyo);
        _idleAnimation.SetEase(Ease.InOutSine);
    }

    public void Levitation()
    {
        _startYPos = _cube.transform.position.y;
        float endPos = _cube.transform.position.y + _levitationAmplitude;

        _levitationAnimation = _cube.transform.DOMoveY(endPos,_levitationSpeed);
        _levitationAnimation.SetEase(Ease.InOutSine);
        _levitationAnimation.SetLoops(-1, LoopType.Yoyo);
    }

    public void StopLevitation()
    {
        _levitationAnimation.Kill();
    }

    public void FallCube()
    {
        _levitationAnimation = _cube.transform.DOMoveY(_fallRange, _fallDuration);
        _levitationAnimation.SetEase(Ease.InBack);
    }

    public void OpenCube()
    {
        _levitationAnimation.Pause();

        _idleAnimation.Kill();
        _idleAnimation = DOTween.Sequence();

        for (byte i = 0; i < _piece.Length; i++)
        {
            Vector3 endPos = _piece[i].StartLocalPosition + _piece[i].Direction * T;
            _idleAnimation.Join(_piece[i].Object.DOLocalMove(endPos, _blindingDuration));
        }

        _idleAnimation.SetEase(Ease.InOutSine);

        _menuManager.AmbientSource.DOFade(0, _blindingDuration);
    }

    public Tween BlindEffect(bool Fade)
    {
        Tween blindTween;
        Material mat = Instantiate(_blindImage.material);

        blindTween = mat.DOFade(_maxAlpha / 255, _blindingDuration);
        blindTween.SetEase(Ease.InOutSine);

        if (Fade)
        {
            blindTween.OnComplete(() => mat.DOFade(0, _blindingDuration));
        }

        if (_audioSource != null)
        {
            _audioSource.clip = _blindingAudio;
            _audioSource.Play();
        }

        _blindImage.material = mat;
        return blindTween;
    }

    public void LoadLevel(string scene)
    {
        DOTween.Clear();
        SceneManager.LoadScene(scene);
    }

    public void RandomChangeColorBackround(Color32[] colorList, RawImage background)
    {
        int index = Random.Range(0, colorList.Length);
        background.color = colorList[index];
    }

    public void Shake()
    {
        if (_shakeAnimation == null)
        {
            _levitationAnimation.Pause();
            _audioSource.PlayOneShot(_shakeAudio);
            _shakeAnimation = _cube.transform.DOShakePosition(_shakeDuration).OnComplete(() => ShakeComplete());
        }
    }

    private void ShakeComplete()
    {
        _levitationAnimation.Play();
        _shakeAnimation.Kill();
        _shakeAnimation = null;
    }
}
