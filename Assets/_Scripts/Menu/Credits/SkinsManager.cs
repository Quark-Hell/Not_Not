using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkinsManager : MonoBehaviour
{
    public Skin[] GeneralSkins;

    public List<Skin> BoughtSkins = new List<Skin>();
    public List<Skin> NotBoughtSkins = new List<Skin>();

    public Skin CurrentSkin { get; private set; }

    private LanguageSettings _languageSettings;

    private void Start()
    {
        foreach (Skin skin in GeneralSkins)
        {
            NotBoughtSkins.Add(skin);
        }
        NotBoughtSkins.RemoveAt(0);

        if (CurrentSkin == null)
        {
            _languageSettings = new LanguageSettings();

            BoughtSkins.Add(GeneralSkins[0]);
            CurrentSkin = BoughtSkins[0];
            BoughtSkins[0].BoughtInfo.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _languageSettings.Selected;
        }
    }

    public void SetCurrentSkin(Skin skin)
    {
        CurrentSkin = skin;
    }
}
