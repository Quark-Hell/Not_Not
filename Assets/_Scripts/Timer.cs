using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private Image TimerBar;
    public bool IsRunning;

    public float TimeToFall;
    private float _elapsed;

    void Start()
    {
        _elapsed = TimeToFall;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsRunning)
        ChangeTimerBar();
    }


    public void ResetTimerBar()
    {
        TimerBar.fillAmount = 1;
        _elapsed = TimeToFall;
    }

    private void ChangeTimerBar()
    {
        if (_elapsed - Time.deltaTime >=0)
        {
            _elapsed -= Time.deltaTime;
        }
        else
        {
            _elapsed = 0;
        }

        TimerBar.fillAmount = Mathf.Lerp(0, 1, _elapsed / TimeToFall);
    }
}
