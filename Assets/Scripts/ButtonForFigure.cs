using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonForFigure : MonoBehaviour
{
    [SerializeField] private FigureHolder _holder;
    private float scaleFigure=1;
    private float localScaleFigure=1;
    public float ScaleFigure { get { return scaleFigure; } }

    // Start is called before the first frame update
    private void Awake()
    {
        localScaleFigure = _holder.transform.localScale.x;
        scaleFigure = _holder.transform.localScale.x;
        for (Transform obj = _holder.transform; obj.parent != null; obj = obj.parent)
        {
            scaleFigure= scaleFigure*obj.parent.localScale.y;
        }
        _holder.Initialize(scaleFigure);
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
            if (Mathf.Abs(localScaleFigure - 
                _holder.transform.localScale.z) > 0.5f)
            {

                _holder.transform.localScale *= scaleFigure;
            }
        }
    }
    public void returnFigure()
    {
        
        _holder.transform.parent = transform.parent;
        _holder.transform.localPosition = Vector3.zero;
        if (Mathf.Abs(localScaleFigure - _holder.transform.localScale.z)>0.5f)
        {

            _holder.transform.localScale *= scaleFigure;
        }

    }
}
