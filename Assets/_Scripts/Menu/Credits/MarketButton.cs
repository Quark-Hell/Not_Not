using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine;

public class MarketButton : MonoBehaviour
{
    [SerializeField] private SkinsManager _skinsManger;
    [SerializeField] private LootBox _lootBox;

    [Header("Buy New Skin Icon")]
    [SerializeField] private GameObject _icon;
    [SerializeField] private float _duration;
    [SerializeField] private float _xCenter;
    [SerializeField] private float _xShift;

    [Header("Select Object")]
    [SerializeField] private float _minScale;
    [SerializeField] private float _selectDuration;

    [Header("Animation Delay")]
    [SerializeField] private float _animationDelay;
    private float _elapsed;

    private EventSystem _eventSystem;
    private LanguageSettings _languageSettings;

    private void Awake()
    {
        _eventSystem = EventSystem.current;

        _elapsed = _animationDelay;

        _languageSettings = new LanguageSettings();

        InitializeBoughtInfo();
    }

    private void Update()
    {
        Timer();
    }

    private void InitializeBoughtInfo()
    {
        for (byte i = 0; i < _skinsManger.GeneralSkins.Length;i++)
        {
            _skinsManger.GeneralSkins[i].BoughtInfo.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _languageSettings.Bought;
        }
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

    public void OpenBuyMenu()
    {
        _icon.transform.DOLocalMoveX(_xCenter, _duration).SetEase(Ease.OutBack);
    }

    public void TryBuy()
    {
        if (_lootBox.Price <= Money.Coins && _elapsed == _animationDelay)
        {
            //Hide Icon
            _icon.transform.DOLocalMoveX(_xShift, _duration).SetEase(Ease.InBack);

            Skin skin = _lootBox.GetRandomSkin(_skinsManger.NotBoughtSkins);
            _skinsManger.BoughtSkins.Add(skin);
            _skinsManger.NotBoughtSkins.Remove(skin);

            _elapsed = 0;
        }
    }

    public void CancelBuy()
    {
        if (_elapsed == _animationDelay)
        {
            //Hide Icon
            _icon.transform.DOLocalMoveX(_xShift, _duration).SetEase(Ease.InBack);

            _elapsed = 0;
        }
    }

    public void SelectSkin(int idSkin)
    {
        foreach (Skin skin in _skinsManger.BoughtSkins)
        {
            if (skin.IdSkin == idSkin)
            {
                if (_elapsed == _animationDelay)
                {
                    Transform clickedButton = _eventSystem.currentSelectedGameObject.transform;

                    Tween selectAnimation = clickedButton.DOScale(_minScale, _selectDuration);
                    selectAnimation.SetEase(Ease.InBack);
                    selectAnimation.OnComplete(() => clickedButton.DOScale(1, _selectDuration).SetEase(Ease.OutBack));

                    _skinsManger.CurrentSkin.BoughtInfo.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _languageSettings.Bought;

                    _skinsManger.SetCurrentSkin(skin);

                    clickedButton.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = _languageSettings.Selected;

                    _elapsed = 0;
                }

            }
        }
    }
}
