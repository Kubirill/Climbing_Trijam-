using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(FigureInHand))]
public class FigureManipulation : MonoBehaviour
{
    private FigureInHand _figure;
    private void Awake()
    {
        _figure=GetComponent<FigureInHand>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RotateFigure(_figure.GetFigure().Figure,true);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            RotateFigure(_figure.GetFigure().Figure, false);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            FlipHVertical(_figure.GetFigure().Figure);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            FlipHorizontal(_figure.GetFigure().Figure);
        }
    }
    public List<List<int>> RotateFigure(List<List<int>> figure, bool inRight)
    {
        List<List<int>> newFigure = new List<List<int>>();
        if (inRight)
        {
            for (int y = figure[0].Count - 1; y >= 0; y--)
            {
                newFigure.Add(new List<int>());
                for (int x = 0; x < figure.Count; x++)
                {
                    newFigure[figure[0].Count - 1 - y].Add(figure[x][y]);
                }
            }
        }
        else
        {
            for (int y = 0; y < figure[0].Count; y++)
            {
                newFigure.Add(new List<int>());
                for (int x = figure.Count-1; x >=0 ; x--)
                {
                    newFigure[y].Add(figure[x][y]);
                }
            }
        }
        /*
        PrintList<int>(figure);
        Debug.Log("_________________");
        PrintList<int>(newFigure);*/
        return newFigure;
    }

    public List<List<int>> FlipHorizontal(List<List<int>> figure)
    {
        List<List<int>> newFigure = new List<List<int>>();
        for (int x = figure.Count - 1; x >= 0; x--)
        {
            newFigure.Add(new List<int>());

            for (int y = 0; y < figure[0].Count ; y++)
            {
                newFigure[figure.Count - 1 - x].Add(figure[x][y]);

            }
        }
        
        
        return newFigure;
    }
    public List<List<int>> FlipHVertical(List<List<int>> figure)
    {
        List<List<int>> newFigure = new List<List<int>>();
        for (int x = 0; x < figure.Count; x++)
        {
            newFigure.Add(new List<int>());

            for (int y = figure[0].Count - 1; y >= 0; y--)
            {
                newFigure[x].Add(figure[x][y]);

            }
        }
        
        return newFigure;
    }

    public void PrintList<T>(List<List<T>> list)
    {
        foreach (var line in list)
        {
            foreach (var i in line)
            {
                Debug.Log(i);
            }
            Debug.Log("_____");
        }
    }
}
