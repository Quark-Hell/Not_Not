using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skin : MonoBehaviour
{
    public bool IsHave { get; private set; }

    [SerializeField] private int idSkin;
    public int IdSkin { get; private set; }

    public GameObject BoughtInfo;
    public Sprite Icon;

    public void Awake()
    {
        IdSkin = idSkin;
    }

    public void SetHave(bool have)
    {
        IsHave = have;
    }
}
