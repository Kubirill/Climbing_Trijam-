using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FigureInfo 
{
    [SerializeField] private Vector2Int _figureSize = new Vector2Int(2, 2);
    [SerializeField] private Vector2Int _offset;
    [SerializeField] private GameObject _blockType;

    private List<List<int>> _figure = new List<List<int>>();
    public Vector2Int Offset { get { return _offset; } }
    public List<List<int>> Figure { get { return _figure; } }

    public Vector2Int Size { get { return _figureSize; } }
    public GameObject BlockType { get { return _blockType; } }

    public FigureInfo(GameObject blocktype)
    {
        Figures figure = chooseClass(UnityEngine.Random.Range(3, 7));
        _figure = figure.GetFigure();
        _figureSize = figure.GetFigureSize();
        _offset = new Vector2Int(-getPivot(_figureSize.x), -getPivot(_figureSize.y));
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
    private int getPivot(int x)
    {
        float substractValue = 1f;
        return Mathf.RoundToInt((x - substractValue) / 2);
    }
}
