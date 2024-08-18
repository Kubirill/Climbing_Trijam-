using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonForFigure : MonoBehaviour
{
    [SerializeField] private FigureHolder _holder;
    private float scaleFigure;
    public float ScaleFigure { get { return scaleFigure; } }

    // Start is called before the first frame update
    private void Awake()
    {
        scaleFigure= _holder.transform.localScale.x;
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
            if (scaleFigure!= _holder.transform.localScale.z)
            {

                _holder.transform.localScale *= scaleFigure;
            }
        }
    }
    public void returnFigure()
    {
        
        _holder.transform.parent = transform.parent;
        _holder.transform.localPosition = Vector3.zero;
        if (scaleFigure != _holder.transform.localScale.z)
        {

            _holder.transform.localScale *= scaleFigure;
        }

    }
}
