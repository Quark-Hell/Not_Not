using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MarketButton : MonoBehaviour
{
    [SerializeField] private SkinsManager _skinsManger;
    [SerializeField] private LootBox _lootBox;
    [SerializeField] private Money _money;

    [Header("Buy New Skin Icon")]
    [SerializeField] private GameObject _icon;
    [SerializeField] private float _duration;
    [SerializeField] private float _xCenter;
    [SerializeField] private float _xShift;

    public void OpenBuyMenu()
    {
        _icon.transform.DOLocalMoveX(_xCenter, _duration).SetEase(Ease.OutBack);
    }

    public void TryBuy()
    {
        _icon.transform.DOLocalMoveX(_xShift, _duration).SetEase(Ease.InBack);

        Skin skin = _lootBox.GetRandomSkin(_skinsManger.NotBoughtSkins);

        _skinsManger.BoughtSkins.Add(skin);

        _skinsManger.NotBoughtSkins.Remove(skin);

        //if (_lootBox.Price <= _money.Coins)
        //{
            //TODO
        //}
    }

    public void SelectSkin(int idSkin)
    {
        foreach (Skin skin in _skinsManger.BoughtSkins)
        {
            if (skin.IdSkin == idSkin)
            {
                _skinsManger.SetCurrentSkin(skin);
                print(skin.IdSkin);
            }
        }
    }
}
