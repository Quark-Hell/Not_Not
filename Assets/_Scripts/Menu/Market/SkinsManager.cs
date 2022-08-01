using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class SkinsManager : MonoBehaviour
{
    public Skin[] GeneralSkins;

    public List<Skin> BoughtSkins = new List<Skin>();
    public List<Skin> NotBoughtSkins = new List<Skin>();

    public Skin CurrentSkin;

    public void SaveSkins()
    {
        //Save in file
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SaveData.dat");
        SaveData data = new SaveData();

        data.BoughtSkinsID.Clear();
        data.NotBoughtSkinsID.Clear();

        for (int i = 0; i < BoughtSkins.Count; i++)
        {
            data.BoughtSkinsID.Add(BoughtSkins[i].IdSkin);
        }
        for (int i = 0; i < NotBoughtSkins.Count; i++)
        {
            data.NotBoughtSkinsID.Add(NotBoughtSkins[i].IdSkin);
        }

        for (int i = 0; i < BoughtSkins.Count; i++)
        {
            if (BoughtSkins[i].IdSkin == CurrentSkin.IdSkin)
            {
                data.CurrentSkinID = BoughtSkins[i].IdSkin;
                break;
            }
        }
        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadSkins()
    {
        if (File.Exists(Application.persistentDataPath + "/SaveData.dat"))
        {
            BoughtSkins.Clear();
            NotBoughtSkins.Clear();

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/SaveData.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

            print(data.BoughtSkinsID.Count);

            for (int i = 0; i < GeneralSkins.Length; i++)
            {
                for (int k = 0; k < data.BoughtSkinsID.Count; k++)
                {
                    if (GeneralSkins[i].IdSkin == data.BoughtSkinsID[k])
                    {
                        BoughtSkins.Add(GeneralSkins[i]);
                        break;
                    }
                }
                for (int k = 0; k < data.NotBoughtSkinsID.Count; k++)
                {
                    if (GeneralSkins[i].IdSkin == data.NotBoughtSkinsID[k])
                    {
                        NotBoughtSkins.Add(GeneralSkins[i]);
                        break;
                    }
                }
            }

            print(BoughtSkins.Count);

            for (int i = 0; i < BoughtSkins.Count; i++)
            {
                if (BoughtSkins[i].IdSkin == data.CurrentSkinID)
                {
                    CurrentSkin = BoughtSkins[i];
                    break;
                }
            }
        }
    }
}
