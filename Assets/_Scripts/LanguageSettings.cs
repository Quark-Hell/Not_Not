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

    private EnglishLanguage _eng = new EnglishLanguage();
    private RussianLanguage _rus = new RussianLanguage();

    public void SetLanguage(LanguagesEnum toLanguage)
    {
        if (toLanguage == LanguagesEnum.English)
        {
            NamesOfSides = _eng.NamesOfSides;
            NamesOfColors = _eng.NamesOfColors;
            Negation = _eng.Negation;
        }
        else if (toLanguage == LanguagesEnum.Russian)
        {
            NamesOfSides = _rus.NamesOfSides;
            NamesOfColors = _rus.NamesOfColors;
            Negation = _rus.Negation;
        }

        Languages = toLanguage;
    }
}

public class EnglishLanguage
{
    public readonly string[] NamesOfSides = { "Up", "Down", "Left", "Right" };
    public readonly string[] NamesOfColors = { "Blue", "Red", "Green", "Yellow" };
    public readonly string Negation = "Not";
}

public class RussianLanguage
{
    public readonly string[] NamesOfSides = { "Вверх", "Вниз", "Влево", "Вправо" };
    public readonly string[] NamesOfColors = { "Синий", "Красный", "Зелёный", "Вправо" };
    public readonly string Negation = "Не";
}


