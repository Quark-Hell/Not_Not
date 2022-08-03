using TMPro;
using UnityEngine;

public class InitSettings : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _musicTMP;
    [SerializeField] private TextMeshProUGUI _soundTMP;

    [SerializeField] private TextMeshProUGUI _flashEffectTMP;
    [SerializeField] private TextMeshProUGUI _saveTMP;

    public void InitLanguage()
    {
        _musicTMP.text = LanguageSettings.Music;
        _soundTMP.text = LanguageSettings.Sound;

        _flashEffectTMP.text = LanguageSettings.FlashEffect;
        _saveTMP.text = LanguageSettings.Save;
    }
}
