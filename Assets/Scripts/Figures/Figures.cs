using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figures 
{
    private Vector2Int _figureSize;
    protected List<List<int>> _figure = new List<List<int>>();

    protected List<Action> createFigure = new List<Action>();

    public Figures()
    {
        SetFigure();
        _figureSize = new Vector2Int(_figure.Count, _figure[0].Count);
    }
    public virtual void SetFigure()
    {

        _figure.Add(new List<int> { 1, 1, 1 });
    }
    public List<List<int>> GetFigure()
    {
        
        return _figure;
    }
}
