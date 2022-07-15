using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeChecker : MonoBehaviour
{
    [SerializeField] private GameData _gameData;
    [SerializeField] private GameManager _gameManager;

    public void SwipeUp()
    {
        if (_gameData.OneRightSide)
        {
            if(_gameData.Side == SidesEnum.Up)
            {
                _gameManager.CreateNewSide();
            }
            else
            {
                _gameManager.Loos();
            }
        }
        else
        {
            if ((_gameData.Side & SidesEnum.Up) == SidesEnum.Up)
            {
                _gameManager.CreateNewSide();
            }
            else
            {
                _gameManager.Loos();
            }
        }
    }

    public void SwipeDown()
    {
        if (_gameData.OneRightSide)
        {
            if (_gameData.Side == SidesEnum.Down)
            {
                _gameManager.CreateNewSide();
            }
            else
            {
                _gameManager.Loos();
            }
        }
        else
        {
            if ((_gameData.Side & SidesEnum.Down) == SidesEnum.Down)
            {
                _gameManager.CreateNewSide();
            }
            else
            {
                _gameManager.Loos();
            }
        }
    }

    public void SwipeLeft()
    {
        if (_gameData.OneRightSide)
        {
            if (_gameData.Side == SidesEnum.Left)
            {
                _gameManager.CreateNewSide();
            }
            else
            {
                _gameManager.Loos();
            }
        }
        else
        {
            if ((_gameData.Side & SidesEnum.Left) == SidesEnum.Left)
            {
                _gameManager.CreateNewSide();
            }
            else
            {
                _gameManager.Loos();
            }
        }
    }

    public void SwipeRight()
    {
        if (_gameData.OneRightSide)
        {
            if (_gameData.Side == SidesEnum.Right)
            {
                _gameManager.CreateNewSide();
            }
            else
            {
                _gameManager.Loos();
            }
        }
        else
        {
            if ((_gameData.Side & SidesEnum.Right) == SidesEnum.Right)
            {
                _gameManager.CreateNewSide();
            }
            else
            {
                _gameManager.Loos();
            }
        }
    }
}
