using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class SpawnSkin : MonoBehaviour
{
    [SerializeField] private Transform _cubeParent;
    [SerializeField] private AssetReferenceGameObject[] _skins;
    private int _currentSkinID;

    void Awake()
    {
        Addressables.InstantiateAsync(LoadSkins(), _cubeParent);
    }

    public AssetReferenceGameObject LoadSkins()
    {
        if (File.Exists(Application.persistentDataPath + "/Skins.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/Skins.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

            _currentSkinID = data.CurrentSkinID;
            //TODO: Select skin by id
            return _skins[_currentSkinID - 1];
        }

        return _skins[0];
    }
}
