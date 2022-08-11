using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class LootBox : MonoBehaviour
{
    [SerializeField] private SkinsManager _skinsManger;
    [SerializeField] private MarketButton _marketButton;

    [Header("Loot Box")]
    [SerializeField] private GameObject Box;
    [SerializeField] private float _duration;
    [SerializeField] private int _price;

    private Vector3 _startBoxPosition;
    private Quaternion _startBoxRotation;
    private Vector3 _startBoxScale;

    public int Price { get; private set; }

    [Header("Skin Icon")]
    [SerializeField] private GameObject _skinPreview;
    [SerializeField] private float _showSkinDuration;
    private Vector3 _startSkinPreviewPosition;
    private Vector3 _startSkinPreviewScale;

    [Header("Animation")]
    [SerializeField] private Animator _animator;

    [Header("Check Mark")]
    [SerializeField] private GameObject _checkMark;
    [SerializeField] private Vector3 _endScaleCheckMark;
    [SerializeField] private float _checkMarkDuration;
    private Vector3 _startCheckMarkScale;

    [Header("Money")]
    [SerializeField] private TextMeshProUGUI _moneyTMP;
    [SerializeField] private float _moneyTextDuration;

    [Header("Close Loot Box")]
    [SerializeField] private float _closeDuration;

    [Header("Animation Delay")]
    [SerializeField] private float _animationDelay;
    private float _elapsed;

    [Header("Cover")]
    [SerializeField] private GameObject _cover;
    [SerializeField] private float _showCoverDuration;
    [SerializeField] private float _zeroAlpha;
    [SerializeField] private float _normalAlpha;

    private Skin _skinBuff;

    private void Awake()
    {
        Price = _price;

        _startBoxPosition = Box.transform.position;
        _startBoxRotation = Box.transform.rotation;
        _startBoxScale = Box.transform.localScale;

        _startCheckMarkScale = _checkMark.transform.lossyScale;

        _startSkinPreviewPosition = _skinPreview.transform.position;
        _startSkinPreviewScale = _skinPreview.transform.localScale;
    }

    private void Update()
    {
        Timer();
    }

    private void Timer()
    {
        if (_elapsed + Time.deltaTime < _animationDelay)
        {
            _elapsed += Time.deltaTime;
        }
        else
        {
            _elapsed = _animationDelay;
        }
    }

    public Skin GetRandomSkin(List<Skin> skins)
    {
        int id = Random.Range(0, skins.Count);
        Skin skin = skins[id];
        _skinPreview.GetComponent<Image>().sprite = skin.Icon;

        OpenBox();

        _skinBuff = skin;
        return skin;
    }

    private void OpenBox()
    {
        _animator = GetComponent<Animator>();

        TakeMoneyAnimation();

        float endPos = Box.transform.position.y - 4.9f;
        Box.transform.DOLocalMoveY(endPos, _duration).SetEase(Ease.OutBack).OnComplete(() => _animator.SetTrigger("Open"));
    }

    private void ShowNewSkin()
    {
        _skinPreview.SetActive(true);
    }

    private void NewSkinAnimation()
    {
        float endPos = _skinPreview.transform.position.y + 2f;
        _skinPreview.transform.DOLocalMoveY(endPos, _showSkinDuration).SetEase(Ease.OutElastic).OnComplete(() => CheckMarkAnimation());
    }

    private Tween _checkMarkAnim;
    private void CheckMarkAnimation()
    {
        _checkMarkAnim = _checkMark.transform.DOScale(_endScaleCheckMark, _checkMarkDuration).SetEase(Ease.OutElastic);
    }

    private void TakeMoneyAnimation()
    {
        DOVirtual.Float(Money.Coins, Money.Coins - Price, _moneyTextDuration, money => {
            _moneyTMP.text = ((int)money).ToString();
        }).SetEase(Ease.OutCirc);

        Money.TakeMoney(Price);
    }

    public void Close()
    {
        if (_elapsed == _animationDelay)
        {
            _checkMarkAnim.Complete();

            Box.transform.DOScale(Vector3.zero, _closeDuration).SetEase(Ease.InBack);
            _checkMark.transform.DOScale(Vector3.zero, _closeDuration).SetEase(Ease.InBack);
            _skinPreview.transform.DOScale(Vector3.zero, _closeDuration).SetEase(Ease.InBack).OnComplete(() => EndAnimation());

            _skinBuff.BoughtInfo.SetActive(true);
            _skinBuff = null;

            _cover.GetComponent<Image>().DOFade(_zeroAlpha / 255, _showCoverDuration).SetEase(Ease.InCirc);
            _marketButton.IsBuying = false;

            _elapsed = 0;
        }
    }

    private void EndAnimation()
    {
        _animator.SetTrigger("Close");

        _skinPreview.SetActive(false);
        _skinPreview.transform.position = _startSkinPreviewPosition;
        _skinPreview.transform.localScale = _startSkinPreviewScale;

        _checkMark.transform.localScale = _startCheckMarkScale;

        Box.transform.position = _startBoxPosition;
        Box.transform.rotation = _startBoxRotation;
        Box.transform.localScale = _startBoxScale;

        _cover.SetActive(false);

        DOTween.Complete(_checkMark);
    }
}
