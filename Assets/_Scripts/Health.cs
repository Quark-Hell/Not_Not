using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health 
{
    public int HealthPoints { get; private set; }
    public bool GameIsOver { get; private set; }

    public void Hit(int damage)
    {
        HealthPoints -= damage;
        if (HealthPoints <= 0)
            GameIsOver = true;
    }
}
