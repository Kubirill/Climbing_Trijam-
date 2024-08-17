using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonForFigure : MonoBehaviour
{
    [SerializeField] private FigureHolder _holder;
  
    private void OnMouseEnter()
    {
        
    }
    private void OnMouseExit()
    {
        
    }

    public event Action<FigureHolder> MouseDown;
    private void OnMouseDown()
    {
        
        MouseDown?.Invoke(_holder);
        
    }
    
}
