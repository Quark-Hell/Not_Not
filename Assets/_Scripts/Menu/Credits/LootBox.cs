using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class LootBox : MonoBehaviour
{
    [SerializeField] private SkinsManager _skinsManger;

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
    private Vector3 _startSkinPreviewScale;

    [Header("Animation")]
    [SerializeField] private AnimationClip _animationClip;
    [SerializeField] private Animation _animation;

    [Header("Check Mark")]
    [SerializeField] private GameObject _checkMark;
    [SerializeField] private Vector3 _endScaleCheckMark;
    [SerializeField] private float _checkMarkDuration;
    private Vector3 _startCheckMarkScale;

    [Header("Money")]
    [SerializeField] private Money _money;
    [SerializeField] private TextMeshProUGUI _moneyTMP;
    [SerializeField] private float _moneyTextDuration;

    [Header("Close Loot Box")]
    [SerializeField] private float _closeDuration;

    private void Awake()
    {
        Price = _price;
        _money = new Money();

        _startBoxPosition = Box.transform.position;
        _startBoxRotation = Box.transform.rotation;
        _startBoxScale = Box.transform.localScale;

        _startCheckMarkScale = _checkMark.transform.lossyScale;
        _startSkinPreviewScale = _skinPreview.transform.localScale;


        _money.GiveMoney(1500);
    }

    public Skin GetRandomSkin(List<Skin> skins)
    {
        int id = Random.Range(0, skins.Count);
        Skin skin = skins[id];
        _skinPreview.GetComponent<Image>().sprite = skin.Icon;

        OpenBox();

        return skin;
    }

    private void OpenBox()
    {
        _animation = GetComponent<Animation>();
        _animation.clip = _animationClip;

        TakeMoneyAnimation();

        float endPos = Box.transform.position.y - 4.9f;
        Box.transform.DOLocalMoveY(endPos, _duration).SetEase(Ease.OutBack).OnComplete(() => _animation.Play());
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

    private void CheckMarkAnimation()
    {
        _checkMark.transform.DOScale(_endScaleCheckMark, _checkMarkDuration).SetEase(Ease.OutElastic);
    }

    private void TakeMoneyAnimation()
    {
        DOVirtual.Float(_money.Coins, _money.Coins - Price, _moneyTextDuration, money => {
            _moneyTMP.text = ((int)money).ToString();
        }).SetEase(Ease.OutCirc);

        _money.TakeMoney(_money.Coins - Price);
    }

    public void Close()
    {
        Box.transform.DOScale(Vector3.zero, _closeDuration).SetEase(Ease.InBack);
        _checkMark.transform.DOScale(Vector3.zero, _closeDuration).SetEase(Ease.InBack);
        _skinPreview.transform.DOScale(Vector3.zero, _closeDuration).SetEase(Ease.InBack).OnComplete(() => EndAnimation());
    }

    private void EndAnimation()
    {
        _skinPreview.SetActive(false);
        _skinPreview.transform.localScale = _startSkinPreviewScale;

        _checkMark.transform.localScale = _startCheckMarkScale;

        Box.transform.position = _startBoxPosition;
        Box.transform.rotation = _startBoxRotation;
        Box.transform.localScale = _startBoxScale;

        _animation.Rewind();
    }
}
