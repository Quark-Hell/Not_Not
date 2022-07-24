using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LanguagesEnum
{
    English,
    Russian
}

public class LanguageSettings
{
    public LanguagesEnum Languages { get; private set; }

    public string[] NamesOfSides { get; private set; }
    public string[] NamesOfColors { get; private set; }
    public string Negation { get; private set; }

    public string Difficult { get; private set; }

    public string[] DifficultTypes { get; private set; }

    private EnglishLanguage _eng = new EnglishLanguage();
    private RussianLanguage _rus = new RussianLanguage();

    public void SetLanguage(LanguagesEnum toLanguage)
    {
        if (toLanguage == LanguagesEnum.English)
        {
            NamesOfSides = _eng.NamesOfSides;
            NamesOfColors = _eng.NamesOfColors;
            Negation = _eng.Negation;

            Difficult = _eng.Difficult;
            DifficultTypes = _eng.DifficultTypes;
        }
        else if (toLanguage == LanguagesEnum.Russian)
        {
            NamesOfSides = _rus.NamesOfSides;
            NamesOfColors = _rus.NamesOfColors;
            Negation = _rus.Negation;

            Difficult = _rus.Difficult;
            DifficultTypes = _rus.DifficultTypes;
        }

        Languages = toLanguage;
    }
}

public class EnglishLanguage
{
    public readonly string[] NamesOfSides = { "Up", "Down", "Left", "Right" };
    public readonly string[] NamesOfColors = { "Blue", "Red", "Green", "Yellow" };
    public readonly string Negation = "Not";

    public readonly string Difficult = "Difficult:";
    public readonly string[] DifficultTypes = { "Easy", "Medium,", "Hard", "Hard+", "Madness" };
}

public class RussianLanguage
{
    public readonly string[] NamesOfSides = { "Вверх", "Вниз", "Влево", "Вправо" };
    public readonly string[] NamesOfColors = { "Синий", "Красный", "Зелёный", "Жёлтый" };
    public readonly string Negation = "Не";

    public readonly string Difficult = "Сложность:";
    public readonly string[] DifficultTypes = { "Легко", "Средне,", "Сложно", "Очень Сложно", "Безумие" };
}


