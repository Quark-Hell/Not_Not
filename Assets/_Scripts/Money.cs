using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Money
{
    public static int Coins { get; private set; }

    public static void GiveMoney(int coins)
    {
        Coins += coins;
    }

    public static void TakeMoney(int coins)
    {
        Coins -= coins;
    }
}
