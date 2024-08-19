using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager 
{
    Difficulty _difficulty;
    static int _currentBlock;
    static int _levelUpBlock;

    public DifficultyManager(Difficulty difficulty)
    {
        _difficulty = difficulty;
        MapController.BlockDestroyed += DestroyBlock;
        //sign on BlockDestroy
    }
    private void DestroyBlock(int typeBlock)
    {
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
                break;
            default:
                _levelUpBlock = _difficulty.BlockForLevelUp3;
                break;
        }
        LevelUp?.Invoke();
    }
}
