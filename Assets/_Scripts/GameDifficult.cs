using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DifficultsEnum
{
    Easy,
    Medium,
    Hard,
    HardPlus,
    Madness
}

public class GameDifficult
{
    public DifficultsEnum Difficult { get; private set; }
    [SerializeField] private TimerGUI _timerGUI;    
    
    [SerializeField] private float MediumXP = 50;
    [SerializeField] private float HardXP = 100;
    [SerializeField] private float HardPlusXP = 150;
    [SerializeField] private float MadnessXP = 300;

    [SerializeField] private float MediumTimer = 1.5f;
    [SerializeField] private float HardTimer = 1.25f;
    [SerializeField] private float HardPlusTimer = 1;
    [SerializeField] private float Madness = 0.5f;

    public int XP { get; private set; }

    public void GetXP(int xp)
    {
        XP += xp;

        if (XP == MediumXP)
        {
            Difficult = DifficultsEnum.Medium;
            _timerGUI._timer.SetTimerDuration(MediumTimer);
            _timerGUI._timer.ResetTimer();
            return;
        }

        if (XP == HardXP)
        {
            Difficult = DifficultsEnum.Hard;
            _timerGUI._timer.SetTimerDuration(HardTimer);
            _timerGUI._timer.ResetTimer();
            return;
        }

        if (XP == HardPlusXP)
        {
            Difficult = DifficultsEnum.HardPlus;
            _timerGUI._timer.SetTimerDuration(HardPlusTimer);
            _timerGUI._timer.ResetTimer();
            return;
        }

        if (XP == MadnessXP)
        {
            Difficult = DifficultsEnum.Madness;
            _timerGUI._timer.SetTimerDuration(Madness);
            _timerGUI._timer.ResetTimer();
            return;
        }
    }
}
