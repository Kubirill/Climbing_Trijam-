using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DifficultyManager 
{
    static Difficulty _difficulty;
    public static int _currentBlock;
    public static int _levelUpBlock;
    private MapController _map;
    private MergeWatcher _watcher;
    public DifficultyManager(Difficulty difficulty, MapController map, MergeWatcher mergeWatcher)
    {
        _difficulty = difficulty;
        _levelUpBlock = _difficulty.BlockForLevelUp3;
        MapController.BlockDestroyed += DestroyBlock;
        MapController.NewLine += MergeCheck;
        _watcher = mergeWatcher;
        _map = map;
        //sign on BlockDestroy
    }
    
    public void Destroy()
    {
        MapController.BlockDestroyed -= DestroyBlock;
        MapController.NewLine -= MergeCheck;
    }

    private void DestroyBlock(int typeBlock)
    {
        if (_levelUpBlock == _difficulty.BlockForLevelUp6) return;
        if (typeBlock > 0) 
        {
            _currentBlock++;
            if (_currentBlock >= _levelUpBlock) 
            {
                StartLevelUp();
            }
        }
    }
    public static event Action LevelUp;
    private void StartLevelUp()
    {
        LevelStats.LevelUp();
        _currentBlock = 0;
        switch (LevelStats.blockInFigure)
        {
            case 3:
                _levelUpBlock = _difficulty.BlockForLevelUp3;
                break;
            case 4:
                _levelUpBlock = _difficulty.BlockForLevelUp4;
                break;
            case 5:
                _levelUpBlock = _difficulty.BlockForLevelUp5;
                break;
            case 6:
                _levelUpBlock = _difficulty.BlockForLevelUp6;
                _currentBlock = _levelUpBlock;
                break;
            default:
                _levelUpBlock = _difficulty.BlockForLevelUp3;
                break;
        }
        LevelUp?.Invoke();
    }

    private void MergeCheck(Vector2Int trash1, Vector2Int size)
    {
        _watcher.Resize(size, _difficulty.mapSizeForMerge);
        if ((size.x > _difficulty.mapSizeForMerge.x)||(size.y > _difficulty.mapSizeForMerge.y))
        {
            _map.LaunchMerge();
            _watcher.Merge();
        }
        
    }
    public static void UpdateDifficulty()
    {
        _currentBlock = 0;
        _levelUpBlock = _difficulty.BlockForLevelUp4;
    }
}
