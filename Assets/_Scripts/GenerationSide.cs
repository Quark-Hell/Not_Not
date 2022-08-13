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

    private string CihpherText(bool isOneRightSide, int indexSide)
    {
        //Expamle output: "Right" or "Not Not Right"
        if (isOneRightSide)
        {
            // 0 = false, 1 = true
            bool hasAdditionalNegation = Random.Range(0, 2) == 1;
            //Expamle output: "Right"
            if (hasAdditionalNegation)
            {
                return LanguageSettings.NamesOfSides[indexSide];
            }
            //Expamle output: "Not Not Right"
            return LanguageSettings.Negation + " " + LanguageSettings.Negation + " " + LanguageSettings.NamesOfSides[indexSide];
        }
        //Expamle output: "Not Right"
        else
        {
            return LanguageSettings.Negation + " " + LanguageSettings.NamesOfSides[indexSide];
        }
    }

    private string InvertCihperText(bool isOneRightSide, int indexSide)
    {
        int shiftIndex = 0;
        switch (indexSide)
        {
            //Up to down
            case 0:
                shiftIndex = 1;
                break;

            //Down to up
            case 1:
                shiftIndex = 0;
                break;

            //Left to right
            case 2:
                shiftIndex = 3;
                break;

            //Right to left
            case 3:
                shiftIndex = 2;
                break;
        }

        return CihpherText(isOneRightSide, shiftIndex);
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
                return LanguageSettings.NamesOfColors[randColor];
            }
            //Expamle output: "Not Not Red"
            return LanguageSettings.Negation + " " + LanguageSettings.Negation + " " + LanguageSettings.NamesOfColors[randColor];
        }
        //Expamle output: "Not Red"
        else
        {
            _lightColor[randColor].IsSelected = true;
            return LanguageSettings.Negation + " " + LanguageSettings.NamesOfColors[randColor];
        }
    }

    private string GetCipher(bool isOneRightSide)
    {
        switch (_difficult)
        {
            case DifficultsEnum.Easy:
                return LanguageSettings.NamesOfSides[IndexOfSelectedSide];

            case DifficultsEnum.Medium:
                return CihpherText(isOneRightSide, IndexOfSelectedSide);

            case DifficultsEnum.Hard:
                return CihpherColor(isOneRightSide);

            case DifficultsEnum.HardPlus:
                return InvertCihperText(isOneRightSide, IndexOfSelectedSide);

            //TODO:
            case DifficultsEnum.Madness:
                return CihpherColor(isOneRightSide);
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

    public string GenerateSide(DifficultsEnum difficultsEnum, LightColor[] lightColor, out SidesEnum side, out bool isOneRightSide)
    {
        _difficult = difficultsEnum;
        _lightColor = lightColor;

        InitializeSideEnum(out side, out isOneRightSide);
        DeselectLightColors();
        return GetCipher(isOneRightSide);
    }
}
