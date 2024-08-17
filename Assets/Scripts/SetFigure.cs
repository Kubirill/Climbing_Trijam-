using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts
{
    public class SetFigure : MonoBehaviour
    {

        [SerializeField] private Vector2Int _figureSize = new Vector2Int(2,2);
        [SerializeField] private Vector2Int _offset;
        [SerializeField] private GameObject _blockType;

        [SerializeField] private float _cellSize;
        private List<List<int>> _figure = new List<List<int>>();
        // Use this for initialization


        public Vector2Int Offset { get { return _offset; } }
        public  List<List<int>> Figure { get { return _figure; } }


        private void Awake()
        {
            for (int x = 0; x < _figureSize.x; x++)
            {
                _figure.Add(new List<int>());
                for (int y = 0; y < _figureSize.y; y++)
                {
                    _figure[x].Add(1);
                }
            }
            CreateFigure();
        }
        private void CreateFigure()
        {
            _offset = new Vector2Int(-getPivot(_figureSize.x), -getPivot(_figureSize.y));
            for (int x = 0; x < _figureSize.x; x++)
            {
                for (int y = 0; y < _figureSize.y; y++)
                {
                    if (_figure[x][y] != 0)
                    {

                        Vector2 position;
                        position = (new Vector2(x, y) + _offset) * _cellSize;
                        Instantiate(_blockType, position, Quaternion.identity, transform);
                    }
                }
            }
        }
        private void Update()
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos;
        }

        private int getPivot(int x)
        {
            float substractValue = 1f;
            return Mathf.RoundToInt((x - substractValue) / 2);
        }
    }
}