using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CubeButtons : MonoBehaviour
{
    private Vector2[] _buttonsCoordinate = new Vector2[5];
    public Vector2 CurrentButton;

    void Start()
    {
        _buttonsCoordinate[0] = new Vector2(0,0);//Play Button
        _buttonsCoordinate[1] = new Vector2(-1, 0);//Market Button
        _buttonsCoordinate[2] = new Vector2(1, 0);//Credits Button
        _buttonsCoordinate[3] = new Vector2(0, 1);//Exit Button
        _buttonsCoordinate[4] = new Vector2(0, -1);//Settings Button

        CurrentButton = _buttonsCoordinate[0];
    }

    public void Click(Vector2 coordinate)
    {
        switch (coordinate)
        {
            case Vector2 v when v.Equals(Vector2.zero):
                PlayButton();
                break;

            case Vector2 v when v.Equals(Vector2.left):
                MarketButton();
                break;

            case Vector2 v when v.Equals(Vector2.right):
                CreditsButton();
                break;

            case Vector2 v when v.Equals(Vector2.up):
                ExitButton();
                break;

            case Vector2 v when v.Equals(Vector2.down):
                SettingsButton();
                break;

            default:
                Debug.LogError("Side does not exist");
                break;
        }
    }

    private void PlayButton()
    {
        //Start Game
    }

    private void MarketButton()
    {
        //Open Market
    }

    private void CreditsButton()
    {
        //Open Credits
    }

    private void ExitButton()
    {
        //Exit
    }
    private void SettingsButton()
    {
        //Open Settings
    }
}
