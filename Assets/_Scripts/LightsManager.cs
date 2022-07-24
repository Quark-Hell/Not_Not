using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public struct LightColor
{
    public string NameOfColor;
    public Color ColorOfLight;
    [Range(0, 100)] public float EmissionIntensity;
    public bool IsSelected;
}

public class LightsManager : MonoBehaviour
{
    public LightColor[] lightColor = new LightColor[4];

    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameObject[] _arrows = new GameObject[4];

    private Image[] _linkToImage = new Image[4];
    void Awake()
    {
        lightColor[0].NameOfColor = "Blue";
        lightColor[0].ColorOfLight = Color.blue;
        lightColor[0].EmissionIntensity = 40f;

        lightColor[1].NameOfColor = "Red";
        lightColor[1].ColorOfLight = Color.red;
        lightColor[1].EmissionIntensity = 10f;

        lightColor[2].NameOfColor = "Green";
        lightColor[2].ColorOfLight = Color.green;
        lightColor[2].EmissionIntensity = 10f;

        lightColor[3].NameOfColor = "Yellow";
        lightColor[3].ColorOfLight = Color.yellow;
        lightColor[3].EmissionIntensity = 10f;

        //Cach data
        for (byte i = 0; i < _arrows.Length; i++)
        {
            _linkToImage[i] = _arrows[i].GetComponent<Image>();
        }
}

    public void ResetArrowColors()
    {
        for (byte i = 0; i < _linkToImage.Length; i++)
        {
            _linkToImage[i].color = Color.white;
        }
    }

    private int GetIndexOfSide(SidesEnum sidesEnum)
    {
        System.Array values = System.Enum.GetValues(typeof(SidesEnum));
        return System.Array.IndexOf(SidesEnum.GetValues(sidesEnum.GetType()), sidesEnum);
    }

    public void Shuffle<T>(T[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            int k = Random.Range(0,n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }

    private void ChangeWrongArrowsMaterial(int indexOfSelected)
    {
        Color[] colors = new Color[3];

        byte t = 0;
        for (byte i = 0; i < lightColor.Length; i++)
        {
            if (lightColor[i].IsSelected == false)
            {
                try
                {
                    colors[t] = lightColor[i].ColorOfLight;
                    t++;
                }
                catch
                {
                    print(colors.Length + " colors");
                    print(lightColor.Length + " light color");
                    print(t + " t");
                }
            }
        }
        t = 0;

        Shuffle(colors);

        for (byte i = 0; i < lightColor.Length; i++)
        {
            if (i != indexOfSelected)
            {
                _linkToImage[i].color = colors[t];
                t++;
            }
        }
    }

    private void ChangeRightArrowMaterial(int indexOfSelected)
    {
        for (byte i = 0; i < lightColor.Length; i++)
        {
            if (lightColor[i].IsSelected)
            {
                _linkToImage[indexOfSelected].color =  lightColor[i].ColorOfLight;
                return;
            }
        }
    }

    public void ChangeArrowColors()
    {
        ResetArrowColors();

        if (_gameManager.GameData.OneRightSide)
        {
            int indexOfSelected = GetIndexOfSide(_gameManager.GameData.Side);
            ChangeWrongArrowsMaterial(indexOfSelected);
            ChangeRightArrowMaterial(indexOfSelected);
        }
        else
        {
            SidesEnum all = SidesEnum.Up | SidesEnum.Down | SidesEnum.Left | SidesEnum.Right;
            SidesEnum invertedEnum = ~all ^ ~_gameManager.GameData.Side;

            int indexOfNotSelected = GetIndexOfSide(invertedEnum);
            ChangeWrongArrowsMaterial(indexOfNotSelected);
            ChangeRightArrowMaterial(indexOfNotSelected);
        }
    }
}
