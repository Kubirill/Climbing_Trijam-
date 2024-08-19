
using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    private List<List<Cell>> _tileMap = new List<List<Cell>>();
    private Map _map;

    
    private Vector2 _offset;
    private FigureInHand _hand;

    List<Vector2Int> _pickedSpases;
    bool _spaceExists;
    Vector2Int _lastBlovck;

    [SerializeField] private Vector2Int _gridSize;
    [SerializeField] private float _cellSize;
    [SerializeField] private Transform _parent;
    [SerializeField] private Timer _timer;

    [Header("Block ref")]

    [SerializeField] private Cell _emptyBlock;
    [SerializeField] private Cell _closedBlock;
    [SerializeField] private Sprite _block;



    public static event Action<Vector2Int, Vector2Int> NewLine;//1 - Direction for new line from center
                                                               //2 - Size map
    public static event Action<int> BlockDestroyed;


    
    public void Initialize(FigureInHand hand)
    {
        _hand = hand;
        _map= new Map(_gridSize);
        _hand.BlockChange +=RefreshPick;
        StartCoroutine(CreateMap());
        //LevelStats.MergeStart += MergeMap;
    }

    [ContextMenu("GenerateMap")]
    public void GenerateMap()
    {
        _map = new Map(_gridSize);
        StartCoroutine( CreateMap());

    }
    public IEnumerator CreateMap()
    {
        _offset = -((_map.GetSize() - Vector2.one)/* *LevelStats.sizeBlock*/ / 2f);
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
                        /*
                    case 1:
                        CreateCell(x, y, _emptyBlock);
                        _tileMap[x][y].
                            GetComponent<SpriteRenderer>().sprite = _block;
                        break;*/

                    default:
                        CreateCell(x, y, _closedBlock);
                        break;
                }
              
            }
        }
        yield return new WaitForEndOfFrame();
    }
    private void CreateCell(int x, int y,Cell block)
    {
        Vector2 position;
        position = (new Vector2(x - LevelStats.offsetForCells.x, y - LevelStats.offsetForCells.y) + _offset) * _cellSize;
        Cell refCell;
        refCell = Instantiate(block, position, Quaternion.identity, _parent);
        refCell.transform.localScale =Vector3.one* LevelStats.sizeBlock / 2;
        refCell.SetPosition(new Vector2Int(x- LevelStats.offsetForCells.x, y- LevelStats.offsetForCells.y));
        _tileMap[x].Insert(y,refCell);
        refCell.MouseDown += ClickOnBlockAsync;
        refCell.Refresh += RefreshBlock;
        refCell.MouseEnter += HoldOnBlock;
    }

    
    private void HoldOnBlock(Vector2Int pos)
    {
        _lastBlovck = pos;
        if (_pickedSpases !=null)
        {
            PickBlock(_pickedSpases, false);
        }
        
        var figure = _hand.GetFigure();
        if (figure == null)
        {
            return;
        }
        List<Vector2Int> emptySpaces;
        _spaceExists = _map.CheckSpace(figure.Figure, pos, figure.Pivot,
            out _pickedSpases, out emptySpaces);
        PickBlock(_pickedSpases, true);
    }
    private void RefreshPick()
    {
        HoldOnBlock(_lastBlovck);
    }

    

    private void ClickOnBlockAsync(Vector2Int pos)
    {
        if (LevelStats.gameActiveBlock.Count>0) return;

        LevelStats.gameActiveBlock.Add("Click");
        var figure = _hand.GetFigure();
        if (figure == null)
        {
            LevelStats.gameActiveBlock.Remove("Click");
            return;
        }
        if (!_spaceExists)
        {
            LevelStats.gameActiveBlock.Remove("Click");
            return;
        }

        StartCoroutine(SetUpFigureAsync(pos, figure));
    }

    private IEnumerator SetUpFigureAsync(Vector2Int pos, FigureInfo figure)
    {
        //var emptySpaces = _map.CheckSpace(figure.Figure, pos, figure.Pivot);
        if (_pickedSpases != null)
        {
            PickBlock(_pickedSpases, false);
            SetBlock(_pickedSpases);
            _map.SetBlock(_pickedSpases, 1); //”казать тип блока
            List<int> lines, columns;
             _map.CheckLines(figure.Figure, pos, figure.Pivot, out lines, out columns);
            //_map.CheckLines(_pickedSpases, out lines, out columns);
            _hand.ClearFigure();
            if (lines.Count + columns.Count > 0)
            {
                yield return LaunchDestroyAsync(lines, columns, pos);
                
                //LevelStats.CheckNewLevel();
                yield return new WaitForSeconds(1);
            }
            _hand.ChangeFigure();
        }
        yield return new WaitForSeconds(0);
        LevelStats.gameActiveBlock.Remove("Click");

    }

    private IEnumerator LaunchDestroyAsync(List<int> lines, List<int> columns, Vector2Int pivot)
    {
        int maxDistance = 2;
        for (int i = 0; i < lines.Count; i++)
        {
            for (int x = 0; x < _gridSize.x; x++)
            {
                int distance = Mathf.Abs(x - pivot.x) + Mathf.Abs(lines[i] - pivot.y) + 2;
                
                if (_map.GetBlock(x,lines[i]) != -1)
                {
                    
                    if (distance > maxDistance)
                    {
                        maxDistance = distance;
                    }
                    _tileMap[x][lines[i]].SetStepToDelete(distance);
                    _timer.destroyBlocks += _tileMap[x][lines[i]].DeleteBlock;
                    DigClosesdBlock(new Vector2Int(x,lines[i]), distance+1);
                }
                
            }
        }
        for (int i = 0; i < columns.Count; i++)
        {
           
            for (int y = 0; y < _gridSize.y; y++)
            {
                int distance = Mathf.Abs(columns[i] - pivot.x) + Mathf.Abs(y - pivot.y) + 2;
               
                if (_map.GetBlock(columns[i],y) != -1)
                {
                    
                    if (distance > maxDistance)
                    {
                        maxDistance = distance;
                    }
                    _tileMap[columns[i]][y].SetStepToDelete(distance);
                    _timer.destroyBlocks += _tileMap[columns[i]][y].DeleteBlock;
                    DigClosesdBlock(new Vector2Int(columns[i], y), distance+1);
                }
               
            }
        }
        yield return _timer.SetUpTimer(maxDistance+1);
    }
    public void SetBlock(List<Vector2Int> spaces)
    {
        foreach (Vector2Int emptySpace in spaces)
        {
            _tileMap[emptySpace.x][emptySpace.y].
                GetComponent<SpriteRenderer>().sprite = _block;
        }

    }

    public void PickBlock(List<Vector2Int> spaces,bool pick)
    {
        foreach (Vector2Int emptySpace in spaces)
        {
            _tileMap[emptySpace.x][emptySpace.y].
                GetComponent<SpriteRenderer>().color = new Color(1, pick?0.5f:1, 1);
        }

    }

    public void RefreshBlock(Vector2Int pos)
    {
        try
        {
            int typeBlock = _map.GetBlock(pos.x, pos.y);
            MakeEmpty(pos);
            if (typeBlock == -1) CheckEdge(pos);
        }
        catch 
        {
            Debug.LogWarning("OutOfIndex");
        }
        
        
        //DigClosesdBlock( pos);
    }

    private void DigClosesdBlock(Vector2Int pos,int distance)
    {/*
        bool edge = (pos.x>= _tileMap.Count-1) ||//Change to _gridSize
                        (pos.y >= _tileMap[0].Count-1) ||
                        (pos.x <= 0) ||
                        (pos.y <= 0);
        if (edge)
        {
            return;
        }*/
        // pos += new Vector2Int(1, 0);
        Vector2Int check = new Vector2Int(pos.x-1, pos.y);
        CheckClosedBlock(check, distance);
        check = new Vector2Int(pos.x+1, pos.y);
        CheckClosedBlock(check, distance);
        check = new Vector2Int(pos.x, pos.y-1);
        CheckClosedBlock(check, distance);
        check = new Vector2Int(pos.x, pos.y+1);
        CheckClosedBlock(check, distance);
    }

    private void CheckClosedBlock(Vector2Int pos, int distance)
    {
        try
        {
            if (_map.GetBlock(pos.x, pos.y) == -1)
            {
                _tileMap[pos.x][pos.y].SetStepToDelete(distance);
                _timer.destroyBlocks += _tileMap[pos.x][pos.y].DeleteBlock;
                //pos = MakeEmpty(pos);

            }
        }
        catch
        {
            Debug.LogWarning("outOfLine");
        }
    }
    
    private void MakeEmpty(Vector2Int pos) //Destroy Block
    {
        BlockDestroyed(_map.GetBlock(pos.x, pos.y));
        _map.SetBlock(pos, 0);
        _tileMap[pos.x][pos.y].GetComponent<SpriteRenderer>().sprite =
            _emptyBlock.GetComponent<SpriteRenderer>().sprite;
        
    }



    private void CheckEdge(Vector2Int pos)
    {
        if (pos.x == _gridSize.x - 1)
        {
            AddColumn(true);
            NewLine?.Invoke(new Vector2Int(1,0),_gridSize);
        }
        if (pos.x == 1)
        {
            AddColumn(false);
            NewLine?.Invoke(new Vector2Int(-1, 0), _gridSize);
        }
        if (pos.y == _gridSize.y-1)
        {
            AddRow(true);
            NewLine?.Invoke(new Vector2Int(0, 1), _gridSize);
        }
        if (pos.y == 1)
        {
            AddRow(false);
            NewLine?.Invoke(new Vector2Int(0, -1), _gridSize);

        }
    }
    private void AddColumn(bool inRight)
    {
        _map.AddColumn(inRight);
        _gridSize.x++;
        if (inRight)
        {
            _tileMap.Add(new List<Cell>());
            for (int y = 0; y < _gridSize.y; y++)
            {
                CreateCell(_gridSize.x- 1, y, _closedBlock);
            }
        }
        else
        {
            _tileMap.Insert(0, new List<Cell>());
            LevelStats.offsetForCells.x++;
            for (int y = 0; y < _gridSize.y; y++)
            {
                
                CreateCell(0, y, _closedBlock);
                
            }
        }
        
    }
    private void AddRow(bool abow)
    {
        _map.AddRow(abow);
        if (!abow) LevelStats.offsetForCells.y++;
        for (int x = 0; x < _gridSize.x; x++)
        {
            if (abow)
            {
                CreateCell(x, _gridSize.y, _closedBlock);
            }
            else
            {
                
                CreateCell(x, 0, _closedBlock);
            }
        }

        _gridSize.y++;
    }

    [ContextMenu ("Merge")]
    public IEnumerator MergeMap()
    {
        yield return new WaitForSeconds(1);
        _cellSize=LevelStats.sizeBlock;
        _map.MergeMap();
        _gridSize= _map.GetSize();
        _pickedSpases?.Clear();
        yield return CreateMap();
        LevelStats.MergeCompleete();
    }
    public void LaunchMerge()
    {
        LevelStats.LaunchMerge();
        StartCoroutine(MergeMap());
    }

    [ContextMenu ("levelUP")]
    public void LevelUp()
    {
        LevelStats.LevelUp();
    }
}
