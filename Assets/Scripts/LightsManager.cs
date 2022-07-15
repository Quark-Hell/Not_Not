using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct LightColor
{
    public string NameOfColor;
    public Color ColorOfLight;
    public bool IsSelected;
}

public class LightsManager : MonoBehaviour
{
    public LightColor[] lightColor = new LightColor[4];
    void Start()
    {
        lightColor[0].NameOfColor = "Blue";
        lightColor[0].ColorOfLight = Color.blue;

        lightColor[1].NameOfColor = "Red";
        lightColor[1].ColorOfLight = Color.red;

        lightColor[2].NameOfColor = "Green";
        lightColor[2].ColorOfLight = Color.green;

        lightColor[3].NameOfColor = "Yellow";
        lightColor[3].ColorOfLight = Color.yellow;
    }
}
