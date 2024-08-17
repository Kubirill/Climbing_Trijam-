using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class FigureHolder : MonoBehaviour
{
    [SerializeField] private FigureInfo _figure;
    [SerializeField] private ButtonForFigure _button;

    private Vector3 _scaleFigure;
    public FigureInfo Figure { get { return _figure; } }


    [SerializeField] private GameObject _blockType;
    [SerializeField] private float _cellSize;
    // Start is called before the first frame update
    private void Awake()
    {
        _figure = new FigureInfo(_blockType);
        _scaleFigure = transform.localScale;
        CreateFigure();
    }

    private void CreateFigure()
    {
        for (int x = 0; x < _figure.Size.x; x++)
        {
            for (int y = 0; y < _figure.Size.y; y++)
            {
                if (_figure.Figure[x][y] != 0)
                {
                    Vector3 position;
                    position = (new Vector2(x, y) + _figure.Offset) 
                        * _cellSize*transform.localScale;
                    position = position + transform.position;
                    Instantiate(_blockType, position,Quaternion.identity, transform);

                }
            }
        }
        
    }

    public void ReturnFigure()
    {
        transform.parent = _button.transform.parent;
        transform.localPosition = Vector3.zero;
        transform.localScale = _scaleFigure;
    }
   
}
