using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureHolder : MonoBehaviour
{
    [SerializeField] private FigureInfo _figure;

    private Vector3 _scaleFigure;
    public FigureInfo Figure { get { return _figure; } }


    [SerializeField] private GameObject _blockType;
    [SerializeField] private float _cellSize;
    // Start is called before the first frame update
    private void Awake()
    {
        _scaleFigure = transform.localScale;
        CreateFigure();
    }

    private void CreateFigure()
    {
        _figure = new FigureInfo(_blockType);
        for (int x = 0; x < _figure.Size.x; x++)
        {
            for (int y = 0; y < _figure.Size.y; y++)
            {
                if (_figure.Figure[x][y] > 0)
                {
                    Vector3 position;
                    position = (new Vector2(x, y) - _figure.Pivot) 
                        * _cellSize*transform.localScale;
                    position = position + transform.position;
                    Instantiate(_blockType, position,Quaternion.identity, transform);

                }
            }
        }
        
    }
    public void ChangeFigure()
    {
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
        transform.localScale = _scaleFigure;
        transform.rotation = Quaternion.identity;
        CreateFigure();
    }


}
