using System.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class GameData
{
    public GameDifficult GameDifficult = new GameDifficult();
    public Health PlayerHealth = new Health();

    public SidesEnum Side;
    public bool OneRightSide;

    public bool FlashEffect { get; private set; }

    public float MusicVolume { get; private set; }
    public float SoundVolume { get; private set; }

    public void ResetData()
    {
        if (File.Exists(Application.persistentDataPath + "/SaveData.dat"))
        {
            File.Delete(Application.persistentDataPath + "/SaveData.dat");
        }
    }
}

[Serializable]
public class SettingsData
{
    public LanguagesEnum Language;

    public bool FlashEffect;

    public float MusicVolume;
    public float SoundVolume;
}

[Serializable]
public class MoneyData
{
    public int Coins;
}

[Serializable]
public class SkinsData
{
    public List<int> BoughtSkinsID = new List<int>();
    public List<int> NotBoughtSkinsID = new List<int>();

    public int CurrentSkinID;
}
