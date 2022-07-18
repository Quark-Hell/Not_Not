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

    [SerializeField] private GameData _gameData;
    [SerializeField] private GameObject[] _arrows = new GameObject[4];

    private Material[] _linkToMaterials = new Material[4];
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

        //Cach data
        for (byte i = 0; i < _arrows.Length; i++)
        {
            _linkToMaterials[i] = _arrows[i].GetComponent<Renderer>().material;
        }
}

    int GetIndexOfSide(SidesEnum sidesEnum)
    {
        System.Array values = System.Enum.GetValues(typeof(SidesEnum));

        return System.Array.IndexOf(SidesEnum.GetValues(sidesEnum.GetType()), sidesEnum);
    }

    public void ChangeArrowMaterials()
    {
        if (_gameData.OneRightSide)
        {
            int indexOfSelected = GetIndexOfSide(_gameData.Side);

            for (byte i = 0; i < lightColor.Length;i++)
            {
                if (lightColor[i].IsSelected)
                {
                    _linkToMaterials[indexOfSelected].SetColor("_EmissionColor", lightColor[i].ColorOfLight);
                    return;
                }
            }
        }
        else
        {
            SidesEnum all = SidesEnum.Up | SidesEnum.Down | SidesEnum.Left | SidesEnum.Right;
            SidesEnum invertedEnum = ~all ^ ~_gameData.Side;

            int indexOfNotSelected = GetIndexOfSide(invertedEnum);

            for (byte i = 0; i < lightColor.Length; i++)
            {
                print(lightColor[i].NameOfColor);
                if (lightColor[i].IsSelected)
                {
                    print(lightColor[i].NameOfColor);
                    _linkToMaterials[indexOfNotSelected].SetColor("_EmissionColor", lightColor[i].ColorOfLight);
                }
            }
        }
    }
}
