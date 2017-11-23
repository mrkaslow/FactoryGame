using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InputHandler : ITickable
{
    private ICommand _up, _down, _right, _left;

    public InputHandler(ICommand up, ICommand down, ICommand right, ICommand left)
    {
        this._up = up;
        this._down = down;
        this._right = right;
        this._left = left;
    }

    public void Tick()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            this._up.Execute();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            this._down.Execute();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this._left.Execute();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            this._right.Execute();
        }
    }
}
