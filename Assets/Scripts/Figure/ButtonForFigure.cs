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
        UpdateScale();
        _holder.Initialize(scaleFigure);
    }

    [ContextMenu ("UpdateScale")]
    private void UpdateScale()
    {
        scaleFigure = _holder.transform.localScale.x;
        for (Transform obj = _holder.transform; obj.parent != null; obj = obj.parent)
        {
            scaleFigure = scaleFigure * obj.parent.localScale.y;
        }
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
        if (LevelStats.gameActiveBlock.Count>0) return;
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
                float xSign = Mathf.Sign(_holder.transform.localScale.x);
                float ySign = Mathf.Sign(_holder.transform.localScale.y);
                Vector3 newScale = new Vector3(xSign, ySign, 1);
                _holder.transform.localScale = newScale * localScaleFigure;
            }
        }
    }
    public void returnFigure(FigureHolder holder,bool one)
    {
        
        _holder.transform.parent = transform.parent;
        _holder.transform.localPosition = Vector3.zero;
        
        if (Mathf.Abs(localScaleFigure - _holder.transform.localScale.z)>0.5f)
        {
            float xSign = Mathf.Sign(_holder.transform.localScale.x);
            float ySign = Mathf.Sign(_holder.transform.localScale.y);
            Vector3 newScale = new Vector3(xSign, ySign, 1);
            _holder.transform.localScale = newScale * localScaleFigure;
        }
        UpdateScale();
        _holder.UpdateScale(ScaleFigure);
        if (holder == _holder) _holder.ChangeFigure();
    }
}
