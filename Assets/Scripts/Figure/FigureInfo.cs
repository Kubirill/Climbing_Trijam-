using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FigureInfo
{
    [SerializeField] private Vector2Int _figureSize = new Vector2Int(2, 2);
    [SerializeField] private Vector2Int _pivot;
    [SerializeField] private GameObject _blockType;

    private List<List<int>> _figure = new List<List<int>>();
    public Vector2Int Pivot { get { return _pivot; } }
    public List<List<int>> Figure { get { return _figure; } }

    public Vector2Int Size { get { return _figureSize; } }
    public GameObject BlockType { get { return _blockType; } }

    public FigureInfo(GameObject blocktype)
    {
        //Figures figure = chooseClass(UnityEngine.Random.Range(3, 7));
        Figures figure = chooseClass(LevelStats.blockInFigure);
        _figure = figure.GetFigure();
        _figureSize = figure.GetFigureSize();
        _pivot = getPivot();
        _blockType = blocktype;
    }

    private Figures chooseClass(int blocksCount)
    {
        Figures figure;
        switch (blocksCount)
        {
            case 4:
                figure = new Figure4();
                break;

            case 5:
                figure = new Figure5();
                break;
            case 6:
                figure = new Figure6();
                break;
            default:
                figure = new Figure3();
                break;
        }
        return figure;
    }
    private Vector2Int getPivot()
    {
        Vector2Int pivot = Vector2Int.one;
        for (int x = 0; x < _figure.Count; x++)
        {
            for (int y = 0; y < _figure[0].Count; y++)
            { 
                if ((_figure[x][y] == 2)|| (_figure[x][y] == -2))
                {
                    pivot=new Vector2Int(x, y);
                    break;
                }
            }
        }
        return pivot;
    }


    public void RotateFigure(bool inRight)
    {
        _figure = FigureManipulation.RotateFigure(_figure, inRight);
        _figureSize = new Vector2Int(_figure.Count, _figure[0].Count);
        _pivot = getPivot();
    }

    public void FlipFigure(bool inHorizontal)
    {
        if (inHorizontal)
        {
            _figure = FigureManipulation.FlipHorizontal(_figure);

        }
        else
        {
            _figure = FigureManipulation.FlipHVertical(_figure);

        }
        _figureSize = new Vector2Int(_figure.Count, _figure[0].Count);
        _pivot = getPivot();
    }
}
