using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CreditsButton : MonoBehaviour
{
    [SerializeField] private Image _blindImage;
    [SerializeField] [Range(0, 255)] private float _maxAlpha;
    [SerializeField] [Range(0, 3)] private float _blindingDuration;
    [SerializeField] private AudioClip _blindingAudio;
    [SerializeField] private AudioSource _audioSource;

    public void BackToMenu()
    {
        BlindEffect(false).OnComplete(() => LoadLevel("Menu"));
    }

    public Tween BlindEffect(bool Fade)
    {
        Tween blindTween;
        Material mat = Instantiate(_blindImage.material);

        blindTween = mat.DOFade(_maxAlpha / 255, _blindingDuration);
        blindTween.SetEase(Ease.InOutSine);

        if (Fade)
        {
            blindTween.OnComplete(() => mat.DOFade(0, _blindingDuration));
        }

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
