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
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SaveData.dat");
        SaveData data = new SaveData();

        data.Coins = Coins;

        bf.Serialize(file, data);
        file.Close();
    }

    public static void LoadMoney()
    {
        if (File.Exists(Application.persistentDataPath + "/SaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/SaveData.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

            Debug.Log(data.Coins + " Coins");
            Coins = data.Coins;
        }
    }
}
