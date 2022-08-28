using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;
using UnityEngine;

public class LooseMenu : MonoBehaviour
{
    [Header("Show Loose Menu")]
    [SerializeField] private GameObject _loseMenu;
    [SerializeField] private float _xCenter;
    [SerializeField] private float _showLoseMenuDuration;
    
    [Header("Get Reward Animation")]
    [SerializeField] private TextMeshProUGUI _rewardTMP;
    [SerializeField] private float _rewardAnimationDuration;

    [Header("Show Scores Animation")]
    [SerializeField] private TextMeshProUGUI _scoresTMP;
    [SerializeField] private float _scoresAnimationDuration;

    [Header("Show Best Score Animation")]
    [SerializeField] private TextMeshProUGUI _bestScoresTMP;
    [SerializeField] private float _bestScoresAnimationDuration;

    [Header("Reward Scatter")]
    [SerializeField] [Range(0,100)] private int _rewardScatterPercent;
    [SerializeField] private Transform _coin;
    [SerializeField] private float _shift;
    private float _startXLocalCoinPos;

    public int _reward { get; private set; }

    private void Start()
    {
        _startXLocalCoinPos = _coin.transform.localPosition.x;

        _rewardTMP.text = LanguageSettings.Reward + "0";
        _scoresTMP.text = LanguageSettings.Scores + "0";
        _bestScoresTMP.text = LanguageSettings.Best + "0";
    }


    public void Lose(int xp, int bestXP)
    {
        _loseMenu.transform.DOLocalMoveX(_xCenter, _showLoseMenuDuration).SetEase(Ease.OutBack).OnComplete(() => ShowStatics(xp, bestXP));
    }

    private void ShowStatics(int xp, int bestXP)
    {
        GetReward(xp);
        ShowScoresAnimation(xp);
        ShowBestScoresAnimation(bestXP);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void GetReward(int xp)
    {
        _reward = Random.Range((xp - (xp * _rewardScatterPercent / 100)) / 2, xp - (xp * _rewardScatterPercent / 100));

        GetMoneyAnimation();

        Money.GiveMoney(_reward);
        //Money.SaveMoney();
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
    }

    private void ShowScoresAnimation(int xp)
    {
        DOVirtual.Float(0, (float)xp, _scoresAnimationDuration, scores => {
            _scoresTMP.text = LanguageSettings.Scores + ((int)scores).ToString();
        }).SetEase(Ease.OutCirc);
    }

    private void ShowBestScoresAnimation(int xp)
    {
        DOVirtual.Float(0, (float)xp, _bestScoresAnimationDuration, scores => {
            _bestScoresTMP.text = LanguageSettings.Best + ((int)scores).ToString();
        }).SetEase(Ease.OutCirc);
    }
}
