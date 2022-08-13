using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

[Serializable]
public enum LanguagesEnum
{
    English,
    Russian
}

public static class LanguageSettings
{
    public static LanguagesEnum Languages { get; private set; }

    public static string[] NamesOfSides { get; private set; }
    public static string[] NamesOfColors { get; private set; }
    public static string Negation { get; private set; }

    public static string Difficult { get; private set; }

    public static string[] DifficultTypes { get; private set; }

    public static string Bought { get; private set; }
    public static string Selected { get; private set; }

    public static string Music { get; private set; }
    public static string Sound { get; private set; }


    public static string Settings { get; private set; }
    public static string FlashEffect { get; private set; }
    public static string Language { get; private set; }
    public static string Save { get; private set; }

    public static string Inversion { get; private set; }

    public static string GetReady { get; private set; }

    private static EnglishLanguage _eng = new EnglishLanguage();
    private static RussianLanguage _rus = new RussianLanguage();

    public static void SetLanguage(LanguagesEnum toLanguage)
    {
        if (toLanguage == LanguagesEnum.English)
        {
            NamesOfSides = _eng.NamesOfSides;
            NamesOfColors = _eng.NamesOfColors;
            Negation = _eng.Negation;

            Difficult = _eng.Difficult;
            DifficultTypes = _eng.DifficultTypes;

            Bought = _eng.Bought;
            Selected = _eng.Selected;

            Settings = _eng.Settings;

            Music = _eng.Music;
            Sound = _eng.Sound;

            FlashEffect = _eng.FlashEffect;
            Language = _eng.Language;

            Save = _eng.Save;

            Inversion = _eng.Inversion;

            GetReady = _eng.GetReady;
        }
        else if (toLanguage == LanguagesEnum.Russian)
        {
            NamesOfSides = _rus.NamesOfSides;
            NamesOfColors = _rus.NamesOfColors;
            Negation = _rus.Negation;

            Difficult = _rus.Difficult;
            DifficultTypes = _rus.DifficultTypes;

            Bought = _rus.Bought;
            Selected = _rus.Selected;

            Settings = _rus.Settings;

            Music = _rus.Music;
            Sound = _rus.Sound;

            FlashEffect = _rus.FlashEffect;
            Language = _rus.Language;

            Save = _rus.Save;

            Inversion = _rus.Inversion;

            GetReady = _rus.GetReady;
        }

        Languages = toLanguage;
    }

    public static void LoadLanguage()
    {
        if (File.Exists(Application.persistentDataPath + "/SettingsData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/SettingsData.dat", FileMode.Open);
            SettingsData data = (SettingsData)bf.Deserialize(file);
            file.Close();

            Languages = data.Language;
        }
        SetLanguage(Languages);
    }
}

public class EnglishLanguage
{
    public readonly string[] NamesOfSides = { "Up", "Down", "Left", "Right" };
    public readonly string[] NamesOfColors = { "Blue", "Red", "Green", "Yellow" };
    public readonly string Negation = "Not";

    public readonly string Difficult = "Difficult:";
    public readonly string[] DifficultTypes = { "Easy", "Medium", "Hard", "Hard+", "Madness" };

    public readonly string Bought = "Bought";
    public readonly string Selected = "Selected";

    public readonly string Settings = "Settings";

    public readonly string Music = "Music";
    public readonly string Sound = "Sounds";

    public readonly string FlashEffect = "Flash Effect";
    public readonly string Language = "English";

    public readonly string Save = "Save";

    public readonly string Inversion = "Inversion!!!";

    public readonly string GetReady = "Get Ready!";
}

public class RussianLanguage
{
    public readonly string[] NamesOfSides = { "Вверх", "Вниз", "Влево", "Вправо" };
    public readonly string[] NamesOfColors = { "Синий", "Красный", "Зелёный", "Жёлтый" };
    public readonly string Negation = "Не";

    public readonly string Difficult = "Сложность:";
    public readonly string[] DifficultTypes = { "Легко", "Средне,", "Сложно", "Очень Сложно", "Безумие" };

    public readonly string Bought = "Куплен";
    public readonly string Selected = "Выбран";

    public readonly string Settings = "Настройки";

    public readonly string Music = "Музыка";
    public readonly string Sound = "Звуки";

    public readonly string FlashEffect = "Вспышка";
    public readonly string Language = "Русский";

    public readonly string Save = "Сохранить";

    public readonly string Inversion = "Инверсия!!!";

    public readonly string GetReady = "Приготовься!";
}


