using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InitMarket : MonoBehaviour
{
    [SerializeField] private SkinsManager _skinsManager;
    [SerializeField] private LanguageSettings _languageSettings;

    private void Start()
    {
        _languageSettings = new LanguageSettings();
        Initialize();
        _skinsManager.SaveSkins();
    }

    void Initialize()
    {
        _skinsManager.LoadSkins();

        if (_skinsManager.BoughtSkins.Count == 0)
        {
            foreach (Skin skin in _skinsManager.GeneralSkins)
            {
                _skinsManager.NotBoughtSkins.Add(skin);
            }
            _skinsManager.NotBoughtSkins.RemoveAt(0);
            _skinsManager.BoughtSkins.Add(_skinsManager.GeneralSkins[0]);

            Debug.LogWarning("Bought skin list was null");
        }

        if (_skinsManager.CurrentSkin == null)
        {
            _skinsManager.BoughtSkins[0].SetHave(true);
            _skinsManager.CurrentSkin = _skinsManager.BoughtSkins[0];
            _skinsManager.BoughtSkins[0].BoughtInfo.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _languageSettings.Selected;
            Debug.LogWarning("Current skin was null");
        }

        foreach (Skin skin in _skinsManager.NotBoughtSkins)
        {
            skin.BoughtInfo.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _languageSettings.Bought;
        }

        foreach (Skin skin in _skinsManager.BoughtSkins)
        {
            skin.BoughtInfo.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _languageSettings.Bought;
            skin.BoughtInfo.SetActive(true);
        }

        print(_skinsManager.BoughtSkins.Count);

        _skinsManager.CurrentSkin.BoughtInfo.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _languageSettings.Selected;
    }
}
