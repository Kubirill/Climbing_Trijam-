using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figure6 : Figures
{
    // Start is called before the first frame update
    public Figure6() : base()
    {
        createFigure.Add(figure1);
        createFigure.Add(figure2);
        createFigure.Add(figure3);
        createFigure.Add(figure4);
        createFigure.Add(figure5);
        createFigure.Add(figure6);
        createFigure.Add(figure7);
        createFigure[UnityEngine.Random.Range(0, createFigure.Count)].Invoke();
    }
    public override void SetFigure()
    {
        _figure.Add(new List<int> { 1, 1, 1 });
        _figure.Add(new List<int> { 1, 0, 0 });
    }

    public void figure1()
    {
        _figure.Add(new List<int> { 1, 1, 1 });
        _figure.Add(new List<int> { 1, 0, 1 });
        _figure.Add(new List<int> { 0, 0, 1 });
    }
    public void figure2()
    {
        _figure.Add(new List<int> { 0, 1, 1 });
        _figure.Add(new List<int> { 1, 1, 0 });
        _figure.Add(new List<int> { 0, 1, 1 });
    }
    public void figure3()
    {
        _figure.Add(new List<int> { 1, 1 });
        _figure.Add(new List<int> { 1, 1 });
        _figure.Add(new List<int> { 1, 1 });
    }
    public void figure4()
    {
        _figure.Add(new List<int> { 1, 0, 0, 0 });
        _figure.Add(new List<int> { 1, 1, 1, 1 });
        _figure.Add(new List<int> { 1, 0, 0, 0 });
    }
    public void figure5()
    {
        _figure.Add(new List<int> { 1, 1, 1, 1, 1 });
        _figure.Add(new List<int> { 0, 0, 1, 0, 0 });

    }
    public void figure6()
    {
        _figure.Add(new List<int> { 1, 1, 1, 0 });
        _figure.Add(new List<int> { 0, 1, 1, 1 });

    }
    public void figure7()
    {
        _figure.Add(new List<int> { 1, 1, 1, 1, 1 });
        _figure.Add(new List<int> { 1, 0, 0, 0, 0 });

    }
}


