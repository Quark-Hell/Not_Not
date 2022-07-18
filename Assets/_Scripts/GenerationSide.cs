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

    public string[] NamesSide = { "Up", "Down", "Left", "Right" };
    public string[] ColorsSide = { "Blue", "Red", "Green", "Yellow" };
    public string Negation = "Not";

    private void InitializeSideEnum(out SidesEnum side, out bool isOneRightSide)
    {
        // 0 = false, 1 = true
        isOneRightSide = Random.Range(0, 2) == 1;

        //get index of selected value
        System.Array values = System.Enum.GetValues(typeof(SidesEnum));
        IndexOfSelectedSide = Random.Range(0, values.Length);

        //clearing side enum
        side = new SidesEnum();

        if (isOneRightSide)
        {
            side = (SidesEnum)values.GetValue(IndexOfSelectedSide);
        }
        else
        {
            for (byte i = 0; i < values.Length; i++)
            {
                if (i != IndexOfSelectedSide)
                {
                    side |= (SidesEnum)values.GetValue(i);
                }
            }
        }
    }

    private string ChipherText(bool isOneRightSide)
    {
        //Expamle output: "Right" or "Not Not Right"
        if (isOneRightSide)
        {
            // 0 = false, 1 = true
            bool hasAdditionalNegation = Random.Range(0, 2) == 1;
            //Expamle output: "Right"
            if (hasAdditionalNegation)
            {
                return NamesSide[IndexOfSelectedSide];
            }
            //Expamle output: "Not Not Right"
            return Negation + " " + Negation + " " + NamesSide[IndexOfSelectedSide];
        }
        //Expamle output: "Not Right"
        else
        {
            return Negation + " " + NamesSide[IndexOfSelectedSide];
        }
    }
    private string ChipherColor(LightColor[] lightColor, bool isOneRightSide)
    {
        //Prepare to next generation
        for (byte i = 0; i < lightColor.Length; i++)
        {
            lightColor[i].IsSelected = false;
        }

        int randColor = Random.Range(0, lightColor.Length);
        //Expamle output: "Red" or "Not Not Red"
        if (isOneRightSide)
        {
            lightColor[randColor].IsSelected = true;

            // 0 = false, 1 = true
            bool hasAdditionalNegation = Random.Range(0, 2) == 1;

            //Expamle output: "Red"
            if (hasAdditionalNegation)
            {
                return lightColor[randColor].NameOfColor;
            }
            //Expamle output: "Not Not Red"
            return Negation + " " + Negation + " " + lightColor[randColor].NameOfColor;
        }
        //Expamle output: "Not Red"
        else
        {

            lightColor[randColor].IsSelected = true;
            return Negation + " " + lightColor[randColor].NameOfColor;
        }
    }

    private string GetCipher(DifficultsEnum difficult, LightColor[] lightColor, bool isOneRightSide)
    {
        switch (difficult)
        {
            //TODO: Language

            case DifficultsEnum.Easy:
                return NamesSide[IndexOfSelectedSide];

            case DifficultsEnum.Medium:
                return ChipherText(isOneRightSide);

            case DifficultsEnum.Hard:
                return ChipherColor(lightColor, isOneRightSide);

            case DifficultsEnum.HardPlus:
                return "";

            case DifficultsEnum.Madness:
                return "";
        }
        return "";
    }

    private void DeselectedLightColors(LightColor[] lightColor)
    {
        for (byte i = 0; i < lightColor.Length; i++)
        {
            lightColor[i].IsSelected = false;
        }
    }

    public string GenerateSide(DifficultsEnum difficult, LightColor[] lightColor, out SidesEnum side, out bool isOneRightSide)
    {
        InitializeSideEnum(out side, out isOneRightSide);
        DeselectedLightColors(lightColor);
        return GetCipher(difficult, lightColor, isOneRightSide);
    }
}
