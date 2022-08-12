using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
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

    public static void SaveMoney()
    {
        //Save in file
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/MoneyData.dat");
        MoneyData data = new MoneyData();

        data.Coins = Coins;

        Debug.Log(data.Coins + " Coins" + " Save");

        bf.Serialize(file, data);
        file.Close();
    }

    public static void LoadMoney()
    {
        if (File.Exists(Application.persistentDataPath + "/MoneyData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MoneyData.dat", FileMode.Open);
            MoneyData data = (MoneyData)bf.Deserialize(file);
            file.Close();

            Debug.Log(data.Coins + " Coins");
            Coins = data.Coins;
        }
    }
}
