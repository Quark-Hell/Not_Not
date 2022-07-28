using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money
{
    public int Coins { get; private set; }

    public void GiveMoney(int coins)
    {
        Coins += coins;
    }

    public void TakeMoney(int coins)
    {
        Coins -= coins;
    }
}
