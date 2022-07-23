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

public class GameData : MonoBehaviour
{
    public DifficultsEnum difficult { get; private set; }

    public SidesEnum Side;
    public bool OneRightSide;

    private void Start()
    {
        IncreaseDifficult();
    }

    void IncreaseDifficult()
    {
        difficult += 2;
    }
}
