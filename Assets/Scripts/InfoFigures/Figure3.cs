using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figure3 : Figures
{
    // Start is called before the first frame update
  
    public override void SetFigure()
    {
        createFigure.Add(figure1);
        createFigure.Add(figure2);
        createFigure.Add(figure3);
        createFigure[UnityEngine.Random.Range(0, createFigure.Count)].Invoke();
    }

    public void figure1()
    {
        _figure.Add(new List<int> { 1, 2, 1 });
    }
    public void figure2()
    {
        _figure.Add(new List<int> { 0, 1, 0 });
        _figure.Add(new List<int> { 1, -2, 1 });
    }
    public void figure3()
    {
        _figure.Add(new List<int> { 1, 0});
        _figure.Add(new List<int> { 2, 1});
    }
}


