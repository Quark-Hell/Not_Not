using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class SwipeChecker : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private CubeMovement _cubeMovement;

    private bool _hasFlashEffect;

    private void Start()
    {
        if (File.Exists(Application.persistentDataPath + "/SaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/SaveData.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

            _hasFlashEffect = data.FlashEffect;
        }
    }

    public void SwipeUp()
    {
        if (_gameManager.GameData.OneRightSide)
        {
            if (_gameManager.GameData.Side == SidesEnum.Up)
            {
                //_gameManager.GameData.GameDifficult.GetXP(10);
                _cubeMovement.OnSwipeUp();
                if (_hasFlashEffect)
                {
                    _gameManager._cubeEffects.BlindEffect(true);
                }
                _gameManager._cubeEffects.RandomChangeColorBackround(_gameManager.ColorListForBackround, _gameManager.Background);
                _gameManager.CreateNewSide();
                return;
            }
        }

        if ((_gameManager.GameData.Side & SidesEnum.Up) == SidesEnum.Up)
        {
            //_gameManager.GameData.GameDifficult.GetXP(10);
            _cubeMovement.OnSwipeUp();
            if (_hasFlashEffect)
            {
                _gameManager._cubeEffects.BlindEffect(true);
            }
            _gameManager._cubeEffects.RandomChangeColorBackround(_gameManager.ColorListForBackround, _gameManager.Background);
            _gameManager.CreateNewSide();
            return;
        }

        _gameManager.CreateNewSide();
        _gameManager.WrongSide();
    }

    public void SwipeDown()
    {
        if (_gameManager.GameData.OneRightSide)
        {
            if (_gameManager.GameData.Side == SidesEnum.Down)
            {
                //_gameManager.GameData.GameDifficult.GetXP(10);
                _cubeMovement.OnSwipeDown();
                if (_hasFlashEffect)
                {
                    _gameManager._cubeEffects.BlindEffect(true);
                }
                _gameManager._cubeEffects.RandomChangeColorBackround(_gameManager.ColorListForBackround, _gameManager.Background);
                _gameManager.CreateNewSide();
                return;
            }
        }

        if ((_gameManager.GameData.Side & SidesEnum.Down) == SidesEnum.Down)
        {
            //_gameManager.GameData.GameDifficult.GetXP(10);
            _cubeMovement.OnSwipeDown();
            if (_hasFlashEffect)
            {
                _gameManager._cubeEffects.BlindEffect(true);
            }
            _gameManager._cubeEffects.RandomChangeColorBackround(_gameManager.ColorListForBackround, _gameManager.Background);
            _gameManager.CreateNewSide();
            return;
        }

        _gameManager.CreateNewSide();
        _gameManager.WrongSide();
    }

    public void SwipeLeft()
    {
        if (_gameManager.GameData.OneRightSide)
        {
            if (_gameManager.GameData.Side == SidesEnum.Left)
            {
                //_gameManager.GameData.GameDifficult.GetXP(10);
                _cubeMovement.OnSwipeLeft();
                if (_hasFlashEffect)
                {
                    _gameManager._cubeEffects.BlindEffect(true);
                }
                _gameManager._cubeEffects.RandomChangeColorBackround(_gameManager.ColorListForBackround, _gameManager.Background);
                _gameManager.CreateNewSide();
                return;
            }
        }

        if ((_gameManager.GameData.Side & SidesEnum.Left) == SidesEnum.Left)
        {
            //_gameManager.GameData.GameDifficult.GetXP(10);
            _cubeMovement.OnSwipeLeft();
            if (_hasFlashEffect)
            {
                _gameManager._cubeEffects.BlindEffect(true);
            }
            _gameManager._cubeEffects.RandomChangeColorBackround(_gameManager.ColorListForBackround, _gameManager.Background);
            _gameManager.CreateNewSide();
            return;
        }

        _gameManager.CreateNewSide();
        _gameManager.WrongSide();
    }

    public void SwipeRight()
    {
        if (_gameManager.GameData.OneRightSide)
        {
            if (_gameManager.GameData.Side == SidesEnum.Right)
            {
                //_gameManager.GameData.GameDifficult.GetXP(10);
                _cubeMovement.OnSwipeRight();
                if (_hasFlashEffect)
                {
                    _gameManager._cubeEffects.BlindEffect(true);
                }
                _gameManager._cubeEffects.RandomChangeColorBackround(_gameManager.ColorListForBackround, _gameManager.Background);
                _gameManager.CreateNewSide();
                return;
            }
        }

        if ((_gameManager.GameData.Side & SidesEnum.Right) == SidesEnum.Right)
        {
            //_gameManager.GameData.GameDifficult.GetXP(10);
            _cubeMovement.OnSwipeRight();
            if (_hasFlashEffect)
            {
                _gameManager._cubeEffects.BlindEffect(true);
            }
            _gameManager._cubeEffects.RandomChangeColorBackround(_gameManager.ColorListForBackround, _gameManager.Background);
            _gameManager.CreateNewSide();
            return;
        }

        _gameManager.CreateNewSide();
        _gameManager.WrongSide();
    }
}
