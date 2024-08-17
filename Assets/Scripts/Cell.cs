using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Cell : MonoBehaviour
{
    private Vector2Int position;
    public void SetPosition(Vector2Int pos)
    {
        position = pos;
    }

    
    public event Action<Vector2Int> mouseEnter;

    
    public event Action<Vector2Int> mouseDown;

    private void OnMouseEnter()
    {
        mouseEnter?.Invoke(position);
    }
    private void OnMouseDown()
    {
        mouseDown?.Invoke(position);
    }
}
