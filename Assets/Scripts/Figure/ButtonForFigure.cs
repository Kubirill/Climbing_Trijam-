using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonForFigure : MonoBehaviour
{
    [SerializeField] private FigureHolder _holder;
    [SerializeField] private Transform _parentHolder;
    private float scaleFigure=1;
    private float localScaleFigure=1;
    private const float canvasScale1920 = 0.013f;
    public float ScaleFigure { get { return scaleFigure; } }

    // Start is called before the first frame update
    private void Awake()
    {
        _holder.transform.localScale = Vector3.one * 0.2f / GetParentScale() * canvasScale1920;
        localScaleFigure = _holder.transform.localScale.x;
        UpdateScale();
        _holder.Initialize(scaleFigure);
    }
    private void OnRectTransformDimensionsChange()
    {
        _holder.transform.localScale = Vector3.one * 0.2f / GetParentScale() * canvasScale1920;
        localScaleFigure = _holder.transform.localScale.x;
        UpdateScale();
    }
    float GetParentScale()
    {
        float canvScale = 0; ;
        for (Transform obj = _holder.transform; obj.parent != null; obj = obj.parent)
        {
            canvScale=obj.parent.localScale.y;
        }
        return canvScale;
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
    public void Down()
    {
        if (LevelStats.gameActiveBlock.Count>0) return;
        MouseDown?.Invoke(_holder);
        
    }
    public void returnFigure(FigureHolder holder)
    {
        if (holder.transform.parent != _parentHolder)
        {

            _holder.transform.parent = _parentHolder;
            _holder.transform.localPosition = Vector3.zero;
            if (Mathf.Abs(localScaleFigure - _holder.transform.localScale.z) > 0.001f)
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
        
        _holder.transform.parent = _parentHolder;
        _holder.transform.localPosition = Vector3.zero;
        
        if (Mathf.Abs(localScaleFigure - _holder.transform.localScale.z)>0.001f)
        {
            float xSign = Mathf.Sign(_holder.transform.localScale.x);
            float ySign = Mathf.Sign(_holder.transform.localScale.y);
            Vector3 newScale = new Vector3(xSign, ySign, 1);
            _holder.transform.localScale = newScale * localScaleFigure;
        }
        UpdateScale();
        _holder.UpdateScale(ScaleFigure);
        if (holder == _holder) _holder.ChangeFigure(Vector2Int.zero);
    }
}
