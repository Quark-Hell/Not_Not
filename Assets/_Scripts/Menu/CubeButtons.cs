using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine;


public class CubeButtons : MonoBehaviour
{
    public Vector2[] _buttonsCoordinate { get; private set; }
    [HideInInspector] public Vector2 CurrentButton;

    [SerializeField] private CubeEffects _cubeEffects;

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

    public void Click()
    {
        switch (CurrentButton)
        {
            case Vector2 v when v.Equals(Vector2.zero):
                PlayButton();
                break;

            case Vector2 v when v.Equals(Vector2.right):
                MarketButton();
                break;

            case Vector2 v when v.Equals(Vector2.left):
                CreditsButton();
                break;

            case Vector2 v when v.Equals(Vector2.down):
                ExitButton();
                break;

            case Vector2 v when v.Equals(Vector2.up):
                SettingsButton();
                break;

            default:
                Debug.LogError("Side does not exist");
                break;
        }
    }

    private void PlayButton()
    {
        _cubeEffects.OpenCube();
        _cubeEffects.BlindEffect(false).OnComplete(() => _cubeEffects.LoadLevel("Game"));
        //Start Game
    }

    private void MarketButton()
    {
        //Open Market
    }

    private void CreditsButton()
    {
        _cubeEffects.OpenCube();
        _cubeEffects.BlindEffect(false).OnComplete(() => _cubeEffects.LoadLevel("Credits"));
    }

    private void ExitButton()
    {
        _cubeEffects.OpenCube();
        _cubeEffects.BlindEffect(false).OnComplete(() => Application.Quit());
    }
    private void SettingsButton()
    {
        //Open Settings
    }
}
