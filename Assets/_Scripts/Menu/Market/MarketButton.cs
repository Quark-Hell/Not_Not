using UnityEngine.Purchasing;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;
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
    [HideInInspector] public bool IsBuying;

    [Header("Select Object")]
    [SerializeField] private float _normalScale;
    [SerializeField] private float _minScale;
    [SerializeField] private float _selectDuration;

    [Header("Animation Delay")]
    [SerializeField] private float _animationDelay;
    private float _elapsed;

    [Header("Pages")]
    [SerializeField] private int _currentPage;
    [SerializeField] private GameObject[] _pages;
    [SerializeField] private GameObject _leftArrow;
    [SerializeField] private GameObject _rightArrow;
    [SerializeField] [Range(0, 255)] private float _normalAplha;
    [SerializeField] [Range(0, 255)] private float _hideAlpha;
    [SerializeField] private float _fadeDuration;
    [SerializeField] private float _pageXCenter;
    [SerializeField] private float _pageXShift;
    [SerializeField] private float _pageShiftDuration;

    [Header("Purchase")]
    [SerializeField] private GameObject _purchaseMenu;
    [SerializeField] private float _showPurchaseDuration;
    [SerializeField] private float _purchaseYCenter;
    [SerializeField] private float _purchaseYShift;

    [Header("Cover")]
    [SerializeField] private GameObject _cover;
    [SerializeField] private float _showCoverDuration;
    [SerializeField] private float _zeroAlpha;
    [SerializeField] private float _normalAlpha;

    [Header("Money")]
    [SerializeField] private TextMeshProUGUI _moneyTMP;

    private EventSystem _eventSystem;

    private void Start()
    {
        PurchaseManager.OnPurchaseConsumable += MoneyPurchase;

        _moneyTMP.text = Money.Coins.ToString();

        _eventSystem = EventSystem.current;

        _elapsed = _animationDelay;
    }

    private void Update()
    {
        Timer();
    }

    public void NextPage()
    {
        if (IsBuying)
            return;

        if (_currentPage + 1 < _pages.Length && _elapsed == _animationDelay)
        {
            _pages[_currentPage].transform.DOMoveX(_pageXShift, _pageShiftDuration).SetEase(Ease.InOutBack);
            _pages[_currentPage + 1].transform.DOMoveX(_pageXCenter, _pageShiftDuration).SetEase(Ease.InOutBack);

            _currentPage++;
            HideArrow();

            _elapsed = 0;
        }
    }

    public void PreviousPage()
    {
        if (IsBuying)
            return;

        if (_currentPage - 1 >= 0 && _elapsed == _animationDelay)
        {
            _pages[_currentPage].transform.DOMoveX(-_pageXShift, _pageShiftDuration).SetEase(Ease.InOutBack);
            _pages[_currentPage - 1].transform.DOMoveX(_pageXCenter, _pageShiftDuration).SetEase(Ease.InOutBack);

            _currentPage--;
            HideArrow();

            _elapsed = 0;
        }
    }

    private void HideArrow()
    {
        int lastPage = _pages.Length;
        if (_currentPage == 0)
        {
            _leftArrow.GetComponent<Image>().DOFade(_hideAlpha / 255, _fadeDuration);
            _rightArrow.GetComponent<Image>().DOFade(_normalAplha / 255, _fadeDuration);
        }
        else if(_currentPage == _pages.Length - 1)
        {
            _rightArrow.GetComponent<Image>().DOFade(_hideAlpha / 255, _fadeDuration);
            _leftArrow.GetComponent<Image>().DOFade(_normalAplha / 255, _fadeDuration);
        }
        else
        {
            _leftArrow.GetComponent<Image>().DOFade(_normalAplha / 255, _fadeDuration);
            _rightArrow.GetComponent<Image>().DOFade(_normalAplha / 255, _fadeDuration);
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
        if (_elapsed == _animationDelay)
        {
            _icon.transform.DOLocalMoveX(_xCenter, _duration).SetEase(Ease.OutBack);

            _cover.GetComponent<Image>().DOFade(_normalAlpha / 255, _showCoverDuration).SetEase(Ease.OutCirc);
            _cover.SetActive(true);

            _elapsed = 0;
        }
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

            _skinsManger.SaveSkins();
            Money.SaveMoney();

            IsBuying = true;

            _elapsed = 0;
        }
    }

    public void CancelBuy()
    {
        if (_elapsed == _animationDelay)
        {
            //Hide Icon
            _icon.transform.DOLocalMoveX(_xShift, _duration).SetEase(Ease.InBack);

            _cover.GetComponent<Image>().DOFade(_zeroAlpha / 255, _showCoverDuration).SetEase(Ease.OutCirc).OnComplete(() => _cover.SetActive(false));

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
                    selectAnimation.OnComplete(() => clickedButton.DOScale(_normalScale, _selectDuration).SetEase(Ease.OutBack));

                    _skinsManger.CurrentSkin.BoughtInfo.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = LanguageSettings.Bought;

                    _skinsManger.CurrentSkin = skin;
                    _skinsManger.SaveSkins();

                    clickedButton.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = LanguageSettings.Selected;

                    _elapsed = 0;

                    return;
                }
            }
        }
    }

    public void OpenPurchaseMenu()
    {
        if (_elapsed == _animationDelay)
        {
            _purchaseMenu.transform.DOLocalMoveY(_purchaseYCenter, _showPurchaseDuration).SetEase(Ease.OutBack);

            _cover.GetComponent<Image>().DOFade(_normalAlpha / 255, _showCoverDuration).SetEase(Ease.OutCirc);
            _cover.SetActive(true);

            _elapsed = 0;
        }
    }

    public void ClosePurchaseMenu()
    {
        if (_elapsed == _animationDelay)
        {
            _purchaseMenu.transform.DOLocalMoveY(_purchaseYShift, _showPurchaseDuration).SetEase(Ease.InBack);

            _cover.GetComponent<Image>().DOFade(_zeroAlpha / 255, _showCoverDuration).SetEase(Ease.InCirc).OnComplete(() => _cover.SetActive(false));

            _elapsed = 0;
        }
    }

    private void MoneyPurchase(PurchaseEventArgs args)
    {
        switch (args.purchasedProduct.definition.id)
        {
            case "FirstPack":
                BuyMoney(3000);
                break;

            case "SecondPack":
                BuyMoney(7500);
                break;

            case "ThirdPack":
                BuyMoney(15000);
                break;
        }
    }

    public void BuyMoney(int money)
    {
        Money.GiveMoney(money);
        Money.SaveMoney();

        print(Money.Coins);
        _moneyTMP.text = Money.Coins.ToString();
    }
}
