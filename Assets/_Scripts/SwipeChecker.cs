using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeChecker : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private CubeMovement _cubeMovement;

    public void SwipeUp()
    {
        if (_gameManager.GameData.OneRightSide)
        {
            if (_gameManager.GameData.Side == SidesEnum.Up)
            {
                //_gameManager.GameData.GameDifficult.GetXP(10);
                _cubeMovement.OnSwipeUp();
                _gameManager._cubeEffects.BlindEffect(true);
                _gameManager._cubeEffects.RandomChangeColorBackround(_gameManager.ColorListForBackround, _gameManager.Background);
                _gameManager.CreateNewSide();
                return;
            }
        }

        if ((_gameManager.GameData.Side & SidesEnum.Up) == SidesEnum.Up)
        {
            //_gameManager.GameData.GameDifficult.GetXP(10);
            _cubeMovement.OnSwipeUp();
            _gameManager._cubeEffects.BlindEffect(true);
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
                _gameManager._cubeEffects.BlindEffect(true);
                _gameManager._cubeEffects.RandomChangeColorBackround(_gameManager.ColorListForBackround, _gameManager.Background);
                _gameManager.CreateNewSide();
                return;
            }
        }

        if ((_gameManager.GameData.Side & SidesEnum.Down) == SidesEnum.Down)
        {
            //_gameManager.GameData.GameDifficult.GetXP(10);
            _cubeMovement.OnSwipeDown();
            _gameManager._cubeEffects.BlindEffect(true);
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
                _gameManager._cubeEffects.BlindEffect(true);
                _gameManager._cubeEffects.RandomChangeColorBackround(_gameManager.ColorListForBackround, _gameManager.Background);
                _gameManager.CreateNewSide();
                return;
            }
        }

        if ((_gameManager.GameData.Side & SidesEnum.Left) == SidesEnum.Left)
        {
            //_gameManager.GameData.GameDifficult.GetXP(10);
            _cubeMovement.OnSwipeLeft();
            _gameManager._cubeEffects.BlindEffect(true);
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
                _gameManager._cubeEffects.BlindEffect(true);
                _gameManager._cubeEffects.RandomChangeColorBackround(_gameManager.ColorListForBackround, _gameManager.Background);
                _gameManager.CreateNewSide();
                return;
            }
        }

        if ((_gameManager.GameData.Side & SidesEnum.Right) == SidesEnum.Right)
        {
            //_gameManager.GameData.GameDifficult.GetXP(10);
            _cubeMovement.OnSwipeRight();
            _gameManager._cubeEffects.BlindEffect(true);
            _gameManager._cubeEffects.RandomChangeColorBackround(_gameManager.ColorListForBackround, _gameManager.Background);
            _gameManager.CreateNewSide();
            return;
        }

        _gameManager.CreateNewSide();
        _gameManager.WrongSide();
    }
}
