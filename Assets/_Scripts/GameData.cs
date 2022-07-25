using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public GameDifficult GameDifficult = new GameDifficult();
    public Health PlayerHealth = new Health();

    public SidesEnum Side;
    public bool OneRightSide;
}
