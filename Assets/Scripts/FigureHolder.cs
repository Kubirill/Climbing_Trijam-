using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureHolder : MonoBehaviour
{
    [SerializeField] private FigureInfo _figure;

    private Vector3 _scaleFigure;
    private float _trueScale;
    public FigureInfo Figure { get { return _figure; } }


    [SerializeField] private GameObject _blockType;
    [SerializeField] private float _cellSize;
    // Start is called before the first frame update
    public void Initialize(float trueScale)
    {
        LevelStats.Merged += ChangeFigure;
        _scaleFigure = transform.localScale;
        _trueScale = trueScale;
        CreateFigure();
    }
    public void UpdateScale(float trueScale)
    {
        _trueScale = trueScale;
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
                        * _cellSize* _trueScale;
                    position = position + transform.position;
                    Instantiate(_blockType, position,Quaternion.identity, transform);

                }
            }
        }
        
    }
    public void ChangeFigure()
    {
        ClearFigure();
        transform.localScale = _scaleFigure;
        transform.rotation = Quaternion.identity;
        CreateFigure();
    }

    public void ClearFigure()
    {
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }

    private void OnDestroy()
    {
        LevelStats.Merged -= ChangeFigure;
    }
}
