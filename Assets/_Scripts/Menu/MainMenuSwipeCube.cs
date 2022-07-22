using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSwipeCube : MonoBehaviour
{
    public CubeMovement _cubeMovement;
    public CubeButtons _cubeButtons;

    public void SwipeUp()
    {
        if (_cubeButtons.CurrentButton == Vector2.down || _cubeButtons.CurrentButton == Vector2.zero)
        {
            if (_cubeMovement.IsMoving == false)
            {
                _cubeButtons.CurrentButton.y++;
                _cubeMovement.OnSwipeUp();
            }
        }
    }

    public void SwipeDown()
    {
        if (_cubeButtons.CurrentButton == Vector2.up || _cubeButtons.CurrentButton == Vector2.zero)
        {

            if (_cubeMovement.IsMoving == false)
            {
                _cubeButtons.CurrentButton.y--;
                _cubeMovement.OnSwipeDown();
            }
        }
    }

    public void SwipeLeft()
    {
        if (_cubeButtons.CurrentButton == Vector2.right || _cubeButtons.CurrentButton == Vector2.zero)
        {
            if (_cubeMovement.IsMoving == false)
            {
                _cubeButtons.CurrentButton.x--;
                _cubeMovement.OnSwipeLeft();
            }
        }
    }

    public void SwipeRight()
    {
        if (_cubeButtons.CurrentButton == Vector2.left || _cubeButtons.CurrentButton == Vector2.zero)
        {
            if (_cubeMovement.IsMoving == false)
            {
                _cubeButtons.CurrentButton.x++;
                _cubeMovement.OnSwipeRight();
            }
        }
    }
}
