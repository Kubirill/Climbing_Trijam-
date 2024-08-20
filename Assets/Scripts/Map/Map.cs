

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
    public Vector2Int GetSize()
    {
        return new Vector2Int(_map.Count, _map[0].Count);
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
    public List<Vector2Int> CheckSpace(List<List<int>> _figure, Vector2Int clickedCell, Vector2Int pivot)
    {
        List<Vector2Int> freeSpaces = new List<Vector2Int>();
        Vector2Int offsetChecker = clickedCell - pivot;

        for (int x = 0; x < _figure.Count; x++)
        {
            for (int y = 0; y < _figure[0].Count; y++)
            {
                if (_figure[x][y] > 0)
                {

                    bool notEmptyCell = (x + offsetChecker.x >= _map.Count) || 
                        (y + offsetChecker.y >= _map[0].Count) ||
                        (x + offsetChecker.x <0) ||
                        (y + offsetChecker.y <0);
                    if (!notEmptyCell) notEmptyCell= 
                            (_map[x + offsetChecker.x][y + offsetChecker.y] != 0);

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
    
    public bool CheckSpace(List<List<int>> _figure, Vector2Int clickedCell, Vector2Int pivot,
        out List<Vector2Int> pointedSpaces, out List<Vector2Int> emptySpaces)
    {

        pointedSpaces = new List<Vector2Int>();
        emptySpaces = new List<Vector2Int>();
        bool spaceExists = true;
        Vector2Int offsetChecker = clickedCell - pivot;

        for (int x = 0; x < _figure.Count; x++)
        {
            for (int y = 0; y < _figure[0].Count; y++)
            {
                if (_figure[x][y] > 0)
                {

                    bool notEmptyCell = (x + offsetChecker.x >= _map.Count) ||
                        (y + offsetChecker.y >= _map[0].Count) ||
                        (x + offsetChecker.x < 0) ||
                        (y + offsetChecker.y < 0);
                    if (!notEmptyCell)
                    {

                        pointedSpaces.Add(new Vector2Int(x + offsetChecker.x, y + offsetChecker.y));
                    }

                    if (!notEmptyCell) notEmptyCell =
                            (_map[x + offsetChecker.x][y + offsetChecker.y] != 0);

                    if (notEmptyCell)
                    {
                        spaceExists=false;
                    }
                    else
                    {
                        emptySpaces = new List<Vector2Int>();
                    }
                }



            }
        }
        return spaceExists;
    }
    /*
    public void CheckLines(List<Vector2Int> places, out List<int> lines, out List<int> columns)
    {
        columns = new List<int>();
        lines = new List<int>();
        List<int> testColumns = new List<int>();
        List<int> testLines = new List<int>();
        foreach (var place in places)
        {
            if (!columns.Contains(place.x))columns.Add(place.x);
            if (!lines.Contains(place.y)) lines.Add(place.y);
            if (!testColumns.Contains(place.x)) testColumns.Add(place.x);
            if (!testLines.Contains(place.y)) testLines.Add(place.y);
            
        }
        
        foreach (var column in testColumns)
        {
            bool notEmpty = false;
            for (int y = 0; y < _map[column].Count; y++)
            {
                if (_map[column][y] == 0)
                {
                    columns.Remove(column);
                    break;
                }
                if ((!notEmpty) && (_map[column][y] > 0))
                {
                    notEmpty = true;
                }
            }
            if  (!notEmpty)
            {
                columns.Remove(column);
            }

        }
        foreach (var line in testLines)
        {
            bool notEmpty = false;
            for (int x = 0; x < _map.Count; x++)
            {
                if (_map[x][line] == 0)
                {
                    columns.Remove(line);
                    break;
                }
                if ((!notEmpty) && (_map[x][line] > 0))
                {
                    notEmpty = true;
                }
            }
            if (!notEmpty)
            {
                lines.Remove(line);
            }
        }

    }*/
    public void CheckLines(List<List<int>> _figure, Vector2Int clickedCell, Vector2Int pivot,
        out List<int> lines, out List<int> columns)
    {
        Vector2Int offsetChecker =   clickedCell - pivot;
        columns = new List<int>();
        lines = new List<int>();

        for (int x = 0; x < _figure.Count; x++)
        {
            bool lineFull = true;
            bool notEmpty = false;
            for (int y = 0; y < _map[x].Count; y++)
            {
                if (_map[x + offsetChecker.x][y] == 0)
                {
                    lineFull = false;
                    break;
                }
                if ((!notEmpty)&&(_map[x + offsetChecker.x][y] > 0))
                {
                    notEmpty=true;
                }
                

            }
            if ((notEmpty) && (lineFull))
            {
                columns.Add(x + offsetChecker.x);
            }
        }

        for (int y = 0; y < _figure[0].Count; y++)
        {
            bool lineFull = true;
            bool notEmpty = false;
            for (int x = 0; x < _map.Count; x++)
            {
                if (_map[x][y + offsetChecker.y] == 0)
                {
                    lineFull = false;
                    break;
                }
                if (_map[x][y + offsetChecker.y] > 0)
                {
                    notEmpty = true;
                }
            }
            if ((notEmpty) && (lineFull))
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

    public void AddColumn(bool inRight)
    {
        List<int> ambars = new List<int>();
        int count = 20 - LevelStats.mergeCount * 4;
        count = Mathf.Max(count, 0);

        if (count < _map[1].Count)
        {
            while (count < _map[1].Count)
            {
                ambars.Add(Random.Range(0, _map[1].Count+4));
                count = count + 5;
            }
        }

        if (inRight)
        {
            _map.Add(new List<int>());
            for (int y = 0; y < _map[1].Count; y++)
            {
                if (ambars.Contains(y)) _map[_map.Count - 1].Add(-2);
                else _map[_map.Count - 1].Add(-1);
            }
        }
        else
        {
            _map.Insert(0,new List<int>());
            for (int y = 0; y < _map[1].Count; y++)
            {
                if (ambars.Contains(y)) _map[0].Add(-2);
                else _map[0].Add(-1);
            }
        }
    }

    public void AddRow(bool abow)
    {
        List<int> ambars = new List<int>();
        int count = 20 - LevelStats.mergeCount * 4;
        count=Mathf.Max(count, 0);
        if (count < _map[1].Count)
        {
            while (count < _map[1].Count)
            {
                ambars.Add(Random.Range(0, _map[1].Count+4));
                count = count + 5;
            }
        }
        for (int x =0; x < _map.Count; x++)
        {
            if (abow)
            {
                if (ambars.Contains(x)) _map[x].Add(-2);
                else _map[x].Add(-1);
            }
            else
            {
                if (ambars.Contains(x)) _map[x].Insert(0, -2);
                else _map[x].Insert(0,-1);
            }
        }
        
    }
    public void MergeMap()
    {

        List<List<int>> mergedMap = new List<List<int>>();
        for (int x = 0; x < (_map.Count / 2f); x++)
        {
            mergedMap.Add(new List<int>());
            for (int y = 0; y < (_map[1].Count / 2f); y++)
            {
                int typeBlock = CountTypeBlocks(x, y);
                mergedMap[x].Add(typeBlock);
            }
        }
        _map = mergedMap;
    }
    int CountTypeBlocks(int startX,int startY)
    {
        int closeCount = 0;
        int emptyCoint = 0;
        int blockCount = 0;
        for (int x = 0; x < 2; x++)
        {
            for (int y = 0; y < 2; y++)
            {
                if ((startX * 2 + x >= _map.Count - 1) ||
                    (startY * 2 + y >= _map[0].Count - 1))
                {
                    closeCount++;
                    
                }
                else
                {
                    switch (_map[startX * 2 + x][startY * 2 + y])
                    {
                        case 1:
                            blockCount++;
                            break;
                        case 0:
                            emptyCoint++;
                            break;
                        default:
                            closeCount++;
                            break;
                    }
                }
                


            }
        }
        //Debug.Log("c "+closeCount + "e " + emptyCoint + "b " + blockCount);
        if (closeCount >= 2) return -1;
        if (emptyCoint >= 2) return 0;
        return 0;//Block
    }
}
