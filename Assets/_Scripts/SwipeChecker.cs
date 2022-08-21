using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class SwipeChecker : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private CubeMovement _cubeMovement;
    [SerializeField] private StartGame _startGame;

    private bool _hasFlashEffect;

    private void Start()
    {
        if (File.Exists(Application.persistentDataPath + "/SettingsData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/SettingsData.dat", FileMode.Open);
            SettingsData data = (SettingsData)bf.Deserialize(file);
            file.Close();

            _hasFlashEffect = data.FlashEffect;
        }
        else
        {
            _hasFlashEffect = true;
        }
    }

    public void SwipeUp()
    {
        if (!_startGame.IsStartingGame)
        {
            return;
        }

        if (_gameManager.GameData.OneRightSide)
        {
            if (_gameManager.GameData.Side == SidesEnum.Up)
            {
                _cubeMovement.OnSwipeUp();
                NextSide();
                return;
            }
        }

        if ((_gameManager.GameData.Side & SidesEnum.Up) == SidesEnum.Up)
        {
            _cubeMovement.OnSwipeUp();
            NextSide();
        }

        _gameManager.WrongSide();
    }

    public void SwipeDown()
    {
        if (!_startGame.IsStartingGame)
        {
            return;
        }

        if (_gameManager.GameData.OneRightSide)
        {
            if (_gameManager.GameData.Side == SidesEnum.Down)
            {
                _cubeMovement.OnSwipeDown();
                NextSide();
                return;
            }
        }

        if ((_gameManager.GameData.Side & SidesEnum.Down) == SidesEnum.Down)
        {
            _cubeMovement.OnSwipeDown();
            NextSide();
            return;
        }

        _gameManager.WrongSide();
    }

    public void SwipeLeft()
    {
        if (!_startGame.IsStartingGame)
        {
            return;
        }

        if (_gameManager.GameData.OneRightSide)
        {
            if (_gameManager.GameData.Side == SidesEnum.Left)
            {
                _cubeMovement.OnSwipeLeft();
                NextSide();
                return;
            }
        }

        if ((_gameManager.GameData.Side & SidesEnum.Left) == SidesEnum.Left)
        {
            _cubeMovement.OnSwipeLeft();
            NextSide();
            return;
        }

        _gameManager.WrongSide();
    }

    public void SwipeRight()
    {
        if (!_startGame.IsStartingGame)
        {
            return;
        }

        if (_gameManager.GameData.OneRightSide)
        {
            if (_gameManager.GameData.Side == SidesEnum.Right)
            {
                _cubeMovement.OnSwipeRight();
                NextSide();
                return;
            }
        }

        if ((_gameManager.GameData.Side & SidesEnum.Right) == SidesEnum.Right)
        {
            _cubeMovement.OnSwipeRight();
            NextSide();
            return;
        }

        _gameManager.WrongSide();
    }

    private void NextSide()
    {
        if (_hasFlashEffect)
        {
            _gameManager._cubeEffects.BlindEffect(true);
        }
        _gameManager.GameData.GameDifficult.GetXP(1, _gameManager._timerGUI);
        _gameManager._cubeEffects.RandomChangeColorBackround(_gameManager.ColorListForBackround, _gameManager.Background);
        _gameManager.CreateNewSide();
    }
}
