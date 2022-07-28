using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinsManager : MonoBehaviour
{
    public Skin[] GeneralSkins;

    public List<Skin> BoughtSkins = new List<Skin>();
    public List<Skin> NotBoughtSkins = new List<Skin>();

    public Skin CurrentSkin { get; private set; }

    private void Start()
    {
        foreach (Skin skin in GeneralSkins)
        {
            NotBoughtSkins.Add(skin);
        }
    }

    public void SetCurrentSkin(Skin skin)
    {
        CurrentSkin = skin;
    }
}
