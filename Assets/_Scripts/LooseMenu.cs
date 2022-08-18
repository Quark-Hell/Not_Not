using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;
using UnityEngine;

public class LooseMenu : MonoBehaviour
{
    [Header("Get Reward Animation")]
    [SerializeField] private TextMeshProUGUI _moneyTMP;
    [SerializeField] private float _rewardAnimationDuration;

    public int _reward { get; private set; }

    [Header("Reward Scatter")]
    [SerializeField] [Range(0,100)] private int _rewardScatterPercent;

    private void Start()
    {
        GetReward(60);
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

        print(_reward);

        GetMoneyAnimation();
    }

    private void GetMoneyAnimation()
    {

        DOVirtual.Float(0, (float)_reward, _rewardAnimationDuration, money => {
            _moneyTMP.text = ((int)money).ToString();
        }).SetEase(Ease.OutCirc);

        Money.GiveMoney(_reward);
        //Money.SaveMoney();
    }
}
