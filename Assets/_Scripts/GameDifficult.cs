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
    
    [SerializeField] private float MediumXP = 20;
    [SerializeField] private float HardXP = 50;
    [SerializeField] private float HardPlusXP = 100;
    [SerializeField] private float MadnessXP = 150;

    [SerializeField] private float MediumTimer = 1.5f;
    [SerializeField] private float HardTimer = 1.25f;
    [SerializeField] private float HardPlusTimer = 1;
    [SerializeField] private float Madness = 0.5f;

    public int XP { get; private set; }

    public void GetXP(int xp, TimerGUI timerGUI)
    {
        XP += xp;

        if (XP == MediumXP)
        {
            Difficult = DifficultsEnum.Medium;
            timerGUI._timer.SetTimerDuration(MediumTimer);
            timerGUI._timer.ResetTimer();
            return;
        }

        if (XP == HardXP)
        {
            Difficult = DifficultsEnum.Hard;
            timerGUI._timer.SetTimerDuration(HardTimer);
            timerGUI._timer.ResetTimer();
            return;
        }

        if (XP == HardPlusXP)
        {
            Difficult = DifficultsEnum.HardPlus;
            timerGUI._timer.SetTimerDuration(HardPlusTimer);
            timerGUI._timer.ResetTimer();
            return;
        }

        if (XP == MadnessXP)
        {
            Difficult = DifficultsEnum.Madness;
            timerGUI._timer.SetTimerDuration(Madness);
            timerGUI._timer.ResetTimer();
            return;
        }
    }
}
