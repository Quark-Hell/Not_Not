using DG.Tweening;

public class Timer
{
    public float Duration { get; private set; }
    public float Elapsed { get; private set; }

    public delegate void EndTimerHandler();
    public event EndTimerHandler? EndTimer;

    private Tween _timerTween;

    public void SetTimerDuration(float duration)
    {
        Duration = duration;
    }

    public void ResetTimer()
    {
        _timerTween.Kill();
        SetTimer();
    }

    public void CompleteTimer()
    {
        OnCompleteTimer();
        _timerTween.Complete();
    }

    private void SetTimer()
    {
        _timerTween = DOVirtual.Float(0, Duration, Duration, t =>
        {
            Elapsed = t;
        }).SetEase(Ease.Linear).OnComplete(() => OnCompleteTimer());
    }

    private void OnCompleteTimer()
    {
        EndTimer?.Invoke();
    }
}
