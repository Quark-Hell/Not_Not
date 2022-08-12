using UnityEngine.AddressableAssets;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class SpawnSkin : MonoBehaviour
{
    [SerializeField] private Transform _cubeParent;
    [SerializeField] private AssetReferenceGameObject[] _skins;
    [SerializeField] private Light _light;

    private int _currentSkinID;

    void Awake()
    {
        Addressables.InstantiateAsync(LoadSkins(), _cubeParent);

    }

    public AssetReferenceGameObject LoadSkins()
    {
        if (File.Exists(Application.persistentDataPath + "/SkinsData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/SkinsData.dat", FileMode.Open);
            SkinsData data = (SkinsData)bf.Deserialize(file);
            file.Close();

            _currentSkinID = data.CurrentSkinID;
            print(_currentSkinID);

            LightControl();

            //TODO: Select skin by id
            return _skins[_currentSkinID - 1];
        }

        return _skins[0];
    }

    void LightControl()
    {
        if (_currentSkinID == 2)
        {
            _light.intensity = 300;
        }

        if (_currentSkinID == 4)
        {
            _light.intensity = 30;
        }
    }
}
