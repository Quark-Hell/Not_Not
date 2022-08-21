using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;
using UnityEngine;

public class LooseMenu : MonoBehaviour
{
    [Header("Get Reward Animation")]
    [SerializeField] private TextMeshProUGUI _rewardTMP;
    [SerializeField] private float _rewardAnimationDuration;

    [Header("Show Scores Animation")]
    [SerializeField] private TextMeshProUGUI _scoresTMP;
    [SerializeField] private float _scoresAnimationDuration;

    [Header("Reward Scatter")]
    [SerializeField] [Range(0,100)] private int _rewardScatterPercent;
    [SerializeField] private Transform _coin;
    [SerializeField] private float _shift;
    private float _startXLocalCoinPos;

    public int _reward { get; private set; }

    private void Start()
    {
        _startXLocalCoinPos = _coin.transform.localPosition.x;

        _rewardTMP.text = LanguageSettings.Reward;
        _scoresTMP.text = LanguageSettings.Scores;

        GetReward(160);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void GetReward(int xp)
    {
        _reward = Random.Range((xp - (xp * _rewardScatterPercent / 100)) / 2, xp - (xp * _rewardScatterPercent / 100));
        print((xp - (xp * _rewardScatterPercent / 100)) / 2 + " min");
        print(xp - (xp * _rewardScatterPercent / 100) + " max");

        GetMoneyAnimation();
    }

    private void GetMoneyAnimation()
    {
        DOVirtual.Float(0, (float)_reward, _rewardAnimationDuration, money => {
            _rewardTMP.text = LanguageSettings.Reward + ((int)money).ToString();

            _coin.transform.localPosition = new Vector3(
                _startXLocalCoinPos + (_shift * ((int)money).ToString().Length) - _shift, 
                _coin.transform.localPosition.y, 
                _coin.transform.localPosition.z);

        }).SetEase(Ease.OutCirc);

        Money.GiveMoney(_reward);
        //Money.SaveMoney();
    }
}
