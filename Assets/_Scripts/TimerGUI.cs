using UnityEngine.UI;
using UnityEngine;

public class TimerGUI : MonoBehaviour
{
    [SerializeField] private Image TimerBar;
    [SerializeField] private float _timerDiration;

    public Timer _timer { get; private set; }

    private void Start()
    {
        _timer = new Timer();

        _timer.SetTimerDuration(_timerDiration);
        _timer.EndTimer += ResetTimerBar;
    }

    private void Update()
    {
        ChangeTimerBar();
    }

    public void ResetTimerBar()
    {
        TimerBar.fillAmount = 1;
        _timer.ResetTimer();
    }

    private void ChangeTimerBar()
    {
        TimerBar.fillAmount = 1 - (_timer.Elapsed / _timer.Duration);
    }
}
