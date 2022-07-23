using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class CubeButtons : MonoBehaviour
{
    public Vector2[] _buttonsCoordinate { get; private set; }
    public Vector2 CurrentButton;

    [SerializeField] private CubeAnimation _cubeAnimation;

    void Start()
    {
        _buttonsCoordinate = new Vector2[5];

        _buttonsCoordinate[0] = new Vector2(0,0);//Play Button
        _buttonsCoordinate[1] = new Vector2(-1, 0);//Market Button
        _buttonsCoordinate[2] = new Vector2(1, 0);//Credits Button
        _buttonsCoordinate[3] = new Vector2(0, 1);//Exit Button
        _buttonsCoordinate[4] = new Vector2(0, -1);//Settings Button

        CurrentButton = _buttonsCoordinate[0];
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Click(CurrentButton);
        }
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
        _cubeAnimation.Interact("Game");
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
