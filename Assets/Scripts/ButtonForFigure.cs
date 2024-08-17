using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonForFigure : MonoBehaviour
{
    [SerializeField] private FigureHolder _holder;
    private Vector3 scaleFigure;
    // Start is called before the first frame update
    private void Awake()
    {
        scaleFigure= _holder.transform.localScale;
    }
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
    public void returnFigure(FigureHolder holder)
    {
        if (holder.transform.parent != transform.parent)
        {

            _holder.transform.parent = transform.parent;
            _holder.transform.localPosition = Vector3.zero;
            _holder.transform.localScale = scaleFigure;
        }
    }
    public void returnFigure()
    {
        
        _holder.transform.parent = transform.parent;
        _holder.transform.localPosition = Vector3.zero;
        _holder.transform.localScale = scaleFigure;
        
    }
}
