using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using TMPro;
using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    [SerializeField] private Slider _musicVolume;
    [SerializeField] private Slider _soundVolume;

    [SerializeField] private Toggle _flashEffect;
    [SerializeField] private TMP_Dropdown _languageSelection;

    [SerializeField] private InitSettings _initSettings;

    private LanguagesEnum _languages;


    private void Awake()
    {
        LoadSettings();
        _initSettings.InitLanguage();
    }

    public void ChangeLanguage()
    {
        switch (_languageSelection.value)
        {
            case 0:
                _languages = LanguagesEnum.English;
                break;

            case 1:
                _languages = LanguagesEnum.Russian;
                break;
        }
    }

    public void SaveSettings()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SaveData.dat");
        SaveData data = new SaveData();

        LanguageSettings.SetLanguage(_languages);
        data.Language = _languages;

        data.MusicVolume = _musicVolume.normalizedValue;
        data.SoundVolume = _soundVolume.normalizedValue;

        data.FlashEffect = _flashEffect.isOn;

        bf.Serialize(file, data);
        file.Close();

        LoadSettings();
        _initSettings.InitLanguage();
    }

    private void LoadSettings()
    {
        if (File.Exists(Application.persistentDataPath + "/SaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/SaveData.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

            _languages = data.Language;
            LanguageSettings.SetLanguage(_languages);

            if (_languages == LanguagesEnum.English)
            {
                _languageSelection.value = 0;
            }
            else if (_languages == LanguagesEnum.Russian)
            {
                _languageSelection.value = 1;
            }

            _musicVolume.normalizedValue = data.MusicVolume;
            _soundVolume.normalizedValue = data.SoundVolume;

            _flashEffect.isOn = data.FlashEffect;
        }
    }
}
