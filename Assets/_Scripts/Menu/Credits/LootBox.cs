using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine;

public class LootBox : MonoBehaviour
{
    [SerializeField] private SkinsManager _skinsManger;

    [SerializeField] private int price;
    public int Price { get; private set; }

    [SerializeField] private GameObject Box;
    [SerializeField] private float _duration;

    [SerializeField] private float _showSkinDuration;

    [SerializeField] private AnimationClip _animationClip;
    [SerializeField] private Animation _animation;

    [SerializeField] private GameObject _skinPreview;

    private void Awake()
    {
        Price = price;
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
        _skinPreview.transform.DOLocalMoveY(endPos, _showSkinDuration).SetEase(Ease.OutElastic);
    }
}
