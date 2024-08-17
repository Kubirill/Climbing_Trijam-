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

    public event Action<Vector2Int> mouseEnter;

    
    public event Action<Vector2Int> mouseDown;
    public event Action<Vector2Int> refresh;

    private void OnMouseEnter()
    {
        mouseEnter?.Invoke(_position);
    }
    private void OnMouseDown()
    {
        mouseDown?.Invoke(_position);
    }

    public void DeleteBlock(int step)
    {
        if (_stepToDelete == step) 
        {

            refresh?.Invoke(_position);
        }

    }
}
