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

    [SerializeField] private float maxEasyXP = 20;
    [SerializeField] private float maxMediumXP = 50;
    [SerializeField] private float maxHardXP = 100;
    [SerializeField] private float maxHardPlusXP = 300;

    public int XP { get; private set; }

    public void GetXP(int xp)
    {
        XP += xp;

        if (XP < maxEasyXP)
        {
            Difficult = DifficultsEnum.Easy;
            return;
        }

        if (XP < maxMediumXP)
        {
            Difficult = DifficultsEnum.Medium;
            return;
        }

        if (XP < maxHardXP)
        {
            Difficult = DifficultsEnum.Hard;
            return;
        }

        if (XP < maxHardPlusXP)
        {
            Difficult = DifficultsEnum.HardPlus;
            return;
        }

        Difficult = DifficultsEnum.Madness;
        return;
    }
}
