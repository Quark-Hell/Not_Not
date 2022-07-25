using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags]
public enum SidesEnum
{
    Up = (1 << 0),
    Down = (1 << 1),
    Left = (1 << 2),
    Right = (1 << 3)
}

public class GenerationSide
{
    private int IndexOfSelectedSide;

    private DifficultsEnum _difficult;
    private LightColor[] _lightColor;
    private LanguageSettings _languageSettings;

    private bool IsOneSide()
    {
        if (_difficult == DifficultsEnum.Easy)
        {
            return true;
        }

        // 0 = false, 1 = true
        return Random.Range(0, 2) == 1;
    }

    private void InitializeSideEnum(out SidesEnum side, out bool isOneRightSide)
    {
        isOneRightSide = IsOneSide();

        //get index of selected value
        System.Array values = System.Enum.GetValues(typeof(SidesEnum));
        IndexOfSelectedSide = Random.Range(0, values.Length);

        //clearing side enum
        side = new SidesEnum();

        if (isOneRightSide)
        {
            side = (SidesEnum)values.GetValue(IndexOfSelectedSide);
            return;
        }

        for (byte i = 0; i < values.Length; i++)
        {
            if (i != IndexOfSelectedSide)
            {
                side |= (SidesEnum)values.GetValue(i);
            }
        }
    }

    private string CihpherText(bool isOneRightSide)
    {
        //Expamle output: "Right" or "Not Not Right"
        if (isOneRightSide)
        {
            // 0 = false, 1 = true
            bool hasAdditionalNegation = Random.Range(0, 2) == 1;
            //Expamle output: "Right"
            if (hasAdditionalNegation)
            {
                return _languageSettings.NamesOfSides[IndexOfSelectedSide];
            }
            //Expamle output: "Not Not Right"
            return _languageSettings.Negation + " " + _languageSettings.Negation + " " + _languageSettings.NamesOfSides[IndexOfSelectedSide];
        }
        //Expamle output: "Not Right"
        else
        {
            return _languageSettings.Negation + " " + _languageSettings.NamesOfSides[IndexOfSelectedSide];
        }
    }
    private string CihpherColor(bool isOneRightSide)
    {
        int randColor = Random.Range(0, _lightColor.Length);
        //Expamle output: "Red" or "Not Not Red"
        if (isOneRightSide)
        {
            _lightColor[randColor].IsSelected = true;

            // 0 = false, 1 = true
            bool hasAdditionalNegation = Random.Range(0, 2) == 1;

            //Expamle output: "Red"
            if (hasAdditionalNegation)
            {
                return _languageSettings.NamesOfColors[randColor];
            }
            //Expamle output: "Not Not Red"
            return _languageSettings.Negation + " " + _languageSettings.Negation + " " + _languageSettings.NamesOfColors[randColor];
        }
        //Expamle output: "Not Red"
        else
        {
            _lightColor[randColor].IsSelected = true;
            return _languageSettings.Negation + " " + _languageSettings.NamesOfColors[randColor];
        }
    }

    private string GetCipher(bool isOneRightSide)
    {
        switch (_difficult)
        {
            //TODO: Language

            case DifficultsEnum.Easy:
                return _languageSettings.NamesOfSides[IndexOfSelectedSide];

            case DifficultsEnum.Medium:
                return CihpherText(isOneRightSide);

            case DifficultsEnum.Hard:
                return CihpherColor(isOneRightSide);

            case DifficultsEnum.HardPlus:
                return CihpherColor(isOneRightSide);

            case DifficultsEnum.Madness:
                return "";
        }
        return "";
    }

    private void DeselectLightColors()
    {
        for (byte i = 0; i < _lightColor.Length; i++)
        {
            _lightColor[i].IsSelected = false;
        }
    }

    public string GenerateSide(DifficultsEnum difficultsEnum, LightColor[] lightColor, LanguageSettings languageSettings, out SidesEnum side, out bool isOneRightSide)
    {
        _difficult = difficultsEnum;
        _lightColor = lightColor;
        _languageSettings = languageSettings;

        InitializeSideEnum(out side, out isOneRightSide);
        DeselectLightColors();
        return GetCipher(isOneRightSide);
    }
}
