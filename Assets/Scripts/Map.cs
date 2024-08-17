

using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class Map 
{

    private List<List<int>> _map = new List<List<int>>();
    public Map(Vector2Int gridSize)
    {
        GenerateGrid(gridSize);
    }
    private void GenerateGrid(Vector2Int gridSize)
    {
        _map = new List<List<int>>();
        for (int x = 0; x < gridSize.x; x++)
        {
            _map.Add(new List<int>());
            for (int y = 0; y < gridSize.y; y++)
            {
                if ((y == 0) || (y == gridSize.y - 1) || (x == 0) || (x == gridSize.x - 1))
                {
                    _map[x].Add(-1);
                }
                else
                {
                    _map[x].Add(0);
                }
            }
        }
    }
    public List<Vector2Int> CheckSpace(List<List<int>> _figure, Vector2Int pivot, Vector2Int offset)
    {
        List<Vector2Int> freeSpaces = new List<Vector2Int>();
        Vector2Int offsetChecker = pivot + offset;

        for (int x = 0; x < _figure.Count; x++)
        {
            for (int y = 0; y < _figure[0].Count; y++)
            {
                if (_figure[x][y] != 0)
                {
                    
                    bool notEmptyCell = (x >= _map.Count) || (y >= _map[0].Count)
                            || (_map[x + offsetChecker.x][y + offsetChecker.y] != 0);
                    if (notEmptyCell)
                    {
                        return null;
                    }
                    freeSpaces.Add(new Vector2Int(x+offsetChecker.x, y+ offsetChecker.y));
                }



            }
        }
        return freeSpaces;
    }
    public void CheckLines(List<List<int>> _figure, Vector2Int pivot, Vector2Int offset,
        out List<int> lines, out List<int> columns)
    {
        Vector2Int offsetChecker = pivot + offset;
        columns = new List<int>();
        lines = new List<int>();

        for (int x = 0; x < _figure.Count; x++)
        {
            bool lineFull = true;
            for (int y = 0; y < _map[x].Count; y++)
            {
                if (_map[x + offsetChecker.x][y] == 0)
                {
                    lineFull = false;
                    break;
                }
            }
            if (lineFull)
            {
                columns.Add(x + offsetChecker.x);
            }
        }

        for (int y = 0; y < _figure[0].Count; y++)
        {
            bool lineFull = true;
            for (int x = 0; x < _map.Count; x++)
            {
                if (_map[x][y + offsetChecker.y] == 0)
                {
                    lineFull = false;
                    break;
                }
            }
            if (lineFull)
            {
                lines.Add(y + offsetChecker.y);
            }
        }

        
    }
    public void SetBlock(List<Vector2Int> spaces, int typeBlock)
    {
        foreach (Vector2Int emptySpace in spaces)
        {
            _map[emptySpace.x][emptySpace.y] = typeBlock;
        }
    }
    public void SetBlock(Vector2Int space, int typeBlock)
    {
        
        _map[space.x][space.y] = typeBlock;
        
    }

    public int GetBlock(int posX, int posY)
    {
        return _map[posX][posY];
    }
}
