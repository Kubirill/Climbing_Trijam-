using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Cell : MonoBehaviour
{
    private Vector2Int _position;
    private int _stepToDelete;
    public void SetPosition(Vector2Int pos)
    {
        _position = pos;
    }
    public void SetStepToDelete(int step)
    {
        _stepToDelete = step;
    }

    public event Action<Vector2Int> MouseEnter;

    
    public event Action<Vector2Int> MouseDown;
    public event Action<Vector2Int> Refresh;

    private void OnMouseEnter()
    {
        MouseEnter?.Invoke(_position);
    }
    private void OnMouseDown()
    {
        MouseDown?.Invoke(_position);
    }

    public void DeleteBlock(int step)
    {
        if (_stepToDelete == step) 
        {

            Refresh?.Invoke(_position);
        }

    }
}
