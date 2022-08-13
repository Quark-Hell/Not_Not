using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Slider _musicVolume;
    [SerializeField] private Slider _soundVolume;
    [SerializeField] private Toggle _flashEffect;
    [SerializeField] private TMP_Dropdown _languageSelection;

    [Header("Back To Menu")]
    [SerializeField] private Image _blindImage;
    [SerializeField][Range(0, 255)] private float _maxAlpha;
    [SerializeField][Range(0, 3)] private float _blindingDuration;
    [SerializeField] private AudioClip _blindingAudio;
    [SerializeField] private AudioSource _audioSource;

    [Header("Animation Delay")]
    [SerializeField] private float _animationDelay;
    private float _elapsed;

    [SerializeField] private InitSettings _initSettings;

    private LanguagesEnum _languages;


    private void Awake()
    {
        LoadSettings();
        SaveSettings();
        _initSettings.InitLanguage();
    }

    private void Start()
    {
        _elapsed = _animationDelay;
    }

    private void Update()
    {
        Timer();
    }

    private void Timer()
    {
        if (_elapsed + Time.deltaTime < _animationDelay)
        {
            _elapsed += Time.deltaTime;
        }
        else
        {
            _elapsed = _animationDelay;
        }
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
        FileStream file = File.Create(Application.persistentDataPath + "/SettingsData.dat");
        SettingsData data = new SettingsData();

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
        if (File.Exists(Application.persistentDataPath + "/SettingsData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/SettingsData.dat", FileMode.Open);
            SettingsData data = (SettingsData)bf.Deserialize(file);
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

    public void Back()
    {
        if (_elapsed == _animationDelay)
        {
            BlindEffect().OnComplete(() => LoadLevel("Menu"));

            _elapsed = 0;
        }
    }

    public Tween BlindEffect()
    {
        Tween blindTween;
        Material mat = Instantiate(_blindImage.material);

        blindTween = mat.DOFade(_maxAlpha / 255, _blindingDuration);
        blindTween.SetEase(Ease.InOutSine);

        if (_audioSource != null)
        {
            _audioSource.clip = _blindingAudio;
            _audioSource.Play();
        }

        _blindImage.material = mat;
        return blindTween;
    }

    public void LoadLevel(string scene)
    {
        DOTween.Clear();
        SceneManager.LoadScene(scene);
    }
}
