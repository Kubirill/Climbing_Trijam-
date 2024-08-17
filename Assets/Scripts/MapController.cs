
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MapController : MonoBehaviour
{
    [SerializeField] private Vector2Int _gridSize;
    [SerializeField] private float _cellSize;
    [SerializeField] private Vector2 _offset;
    [SerializeField] private Transform _parent;
    [SerializeField] private FigureHolder _hand;
    [SerializeField] private Timer _timer;
    private List<List<Cell>> _tileMap = new List<List<Cell>>();
    private Map _map;

    [SerializeField] private Cell _emptyBlock;
    [SerializeField] private Cell _closedBlock;
    [SerializeField] private Sprite _block;

    public void Initialize()
    {
        _map= new Map(_gridSize);
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
                
                
                switch (_map.GetBlock(x,y))
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
        refCell.refresh += RefreshBlock;
    }

    private void ClickOnBlock(Vector2Int pos)
    {
        var emptySpaces = _map.CheckSpace(_hand.Figure, pos, _hand.Offset);
        if (emptySpaces!=null)
        {
            SetBlock(emptySpaces);
            _map.SetBlock(emptySpaces,1); //”казать тип блока
            List<int> lines, columns;
            _map.CheckLines(_hand.Figure, pos, _hand.Offset,out lines,out columns);
            LaunchDestroy(lines, columns, pos);
        }
    }


    private void LaunchDestroy(List<int> lines, List<int> columns, Vector2Int pivot)
    {
        int maxDistance = 2;
        for (int i = 0; i < lines.Count; i++)
        {
            for (int x = 0; x < _gridSize.x; x++)
            {
                if (_map.GetBlock(x,lines[i]) != -1)
                {
                    int distance = Mathf.Abs(x - pivot.x) + Mathf.Abs(lines[i] - pivot.y) + 2;
                    Debug.Log("Line" + lines[i] + " " + distance);
                    if (distance > maxDistance)
                    {
                        maxDistance = distance;
                    }
                    _tileMap[x][lines[i]].SetStepToDelete(distance);
                    _timer.destroyBlocks += _tileMap[x][lines[i]].DeleteBlock;
                }
                
            }
        }
        for (int i = 0; i < columns.Count; i++)
        {
            for (int y = 0; y < _gridSize.y; y++)
            {
                if (_map.GetBlock(columns[i],y) != -1)
                {
                    int distance = Mathf.Abs(columns[i] - pivot.x) + Mathf.Abs(y - pivot.y) + 2;
                    Debug.Log("column" + columns[i] + " " + distance);
                    if (distance > maxDistance)
                    {
                        maxDistance = distance;
                    }
                    _tileMap[columns[i]][y].SetStepToDelete(distance);
                    _timer.destroyBlocks += _tileMap[columns[i]][y].DeleteBlock;
                }
               
            }
        }
        _timer.SetUpTimer(maxDistance);
    }
    public void SetBlock(List<Vector2Int> spaces)
    {
        foreach (Vector2Int emptySpace in spaces)
        {
            _tileMap[emptySpace.x][emptySpace.y].
                GetComponent<SpriteRenderer>().sprite = _block;
        }

    }
    public void RefreshBlock(Vector2Int pos)
    {
        _tileMap[pos.x][pos.y].GetComponent<SpriteRenderer>().sprite =
            _emptyBlock.GetComponent<SpriteRenderer>().sprite;
        
        _map.SetBlock(pos,0);
    }
}
