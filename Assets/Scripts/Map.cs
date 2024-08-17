
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Map : MonoBehaviour
{
    [SerializeField] private Vector2Int _gridSize;
    [SerializeField] private float _cellSize;
    [SerializeField] private Vector2 _offset;
    [SerializeField] private Transform _parent;
    [SerializeField] private SetFigure _hand;
    private List<List<int>> _map = new List<List<int>>();
    private List<List<Cell>> _tileMap = new List<List<Cell>>();


    [SerializeField] private Cell _emptyBlock;
    [SerializeField] private Cell _closedBlock;
    [SerializeField] private Sprite _block;

    public void Initialize()
    {
        GenerateGrid();
    }
    // Start is called before the first frame update
    private void GenerateGrid()
    {
        _map = new List<List<int>>();
        for (int x = 0; x < _gridSize.x; x++)
        {
            _map.Add(new List<int>());
            for (int y = 0; y < _gridSize.y; y++)
            {
                if ((y == 0) || (y == _gridSize.y - 1) || (x == 0) || (x == _gridSize.x - 1))
                {
                    _map[x].Add(-1);
                }
                else
                {
                    _map[x].Add(0);
                }
            }
        }
        CreateMap();
    }
    public void CreateMap()
    {
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
        _tileMap = new List<List<Cell>>();
        for (int x = 0; x < _gridSize.x; x++)
        {
            _tileMap.Add(new List<Cell>());
            for (int y = 0; y < _gridSize.y; y++)
            {
                
                
                switch (_map[x][y])
                {
                    
                    case 0:
                        CreateCell(x, y, _emptyBlock);
                        break;

                    default:
                        CreateCell(x, y, _closedBlock);
                        break;
                }
              
            }
        }
    }
    private void CreateCell(int x, int y,Cell block)
    {
        Vector2 position;
        position = (new Vector2(x, y) + _offset) * _cellSize;
        Cell refCell;
        refCell = Instantiate(block, position, Quaternion.identity, _parent);
        refCell.SetPosition(new Vector2Int(x, y));
        _tileMap[x].Add(refCell);
        refCell.mouseDown += ClickOnBlock;
    }

    private void ClickOnBlock(Vector2Int pos)
    {
        Debug.Log(CheckSpace(_hand.Figure,pos,_hand.Offset));
        if (CheckSpace(_hand.Figure, pos, _hand.Offset))
        {
            SetBlock(_hand.Figure, pos, _hand.Offset);
        }
    }

   

    public bool CheckSpace(List<List<int>> _figure, Vector2Int pivot, Vector2Int offset)
    {
        bool cheecker = true;
        Vector2Int offsetChecker = pivot + offset;

        for (int x = 0; x < _figure.Count; x++)
        {
            for (int y =0; y < _figure[0].Count; y++)
            {
                if (_figure[x][y] != 0)
                {
                    bool notEmptyCell = (x >= _map.Count) || (y >= _map[0].Count)
                            || (_map[x + offsetChecker.x][y + offsetChecker.y] != 0);
                    if (notEmptyCell)
                    {
                        return false;
                    }
                }
               
                
               
            }
        }
        return cheecker;
    }

    public bool SetBlock(List<List<int>> _figure, Vector2Int pivot, Vector2Int offset)
    {
        bool cheecker = true;
        Vector2Int offsetChecker = pivot + offset;

        for (int x =0; x < _figure.Count ; x++)
        {
            for (int y = 0; y < _figure[0].Count ; y++)
            {
                if (_figure[x][y] != 0)
                {
                    _map[x + offsetChecker.x][y + offsetChecker.y] = 1;
                    _tileMap[x +offsetChecker.x][y + offsetChecker.y].
                        GetComponent<SpriteRenderer>().sprite = _block;
                }



            }
        }
        return cheecker;
    }

}
