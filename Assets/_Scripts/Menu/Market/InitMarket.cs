using TMPro;
using UnityEngine;

public class InitMarket : MonoBehaviour
{
    [SerializeField] private SkinsManager _skinsManager;

    [Header("Buy Icon")]
    [SerializeField] private TextMeshProUGUI _buyIconInfoTMP;
    [SerializeField] private TextMeshProUGUI _buyIconYesTMP;
    [SerializeField] private TextMeshProUGUI _buyIconNoTMP;

    private void Awake()
    {
        LanguageSettings.LoadLanguage();
        Money.LoadMoney();
    }

    private void Start()
    {
        _buyIconInfoTMP.text = LanguageSettings.BuyIcon;
        _buyIconYesTMP.text = LanguageSettings.Yes;
        _buyIconNoTMP.text = LanguageSettings.No;

        _skinsManager.LoadSkins();

        Initialize();
        _skinsManager.SaveSkins();
    }

    void Initialize()
    {
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
            _skinsManager.BoughtSkins[0].BoughtInfo.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = LanguageSettings.Selected;
            Debug.LogWarning("Current skin was null");
        }

        foreach (Skin skin in _skinsManager.NotBoughtSkins)
        {
            skin.BoughtInfo.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = LanguageSettings.Bought;           
        }

        foreach (Skin skin in _skinsManager.BoughtSkins)
        {
            skin.BoughtInfo.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = LanguageSettings.Bought;
            skin.BoughtInfo.SetActive(true);
        }

        _skinsManager.CurrentSkin.BoughtInfo.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = LanguageSettings.Selected;
    }
}
