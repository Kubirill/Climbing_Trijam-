using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class LevelStats 
{
    public static Vector2Int offsetForCells =Vector2Int.zero;
    public static int blockInFigure = 3;
    public static int sizeBlock = 2;
    public static bool gameActive = true;

    public static void NewGame()
    {
        sizeBlock = 2;
        offsetForCells =Vector2Int.zero;
        blockInFigure = 3;
        gameActive = true;
    }
    public static event Action Merged;
    public static void Merge()
    {
        offsetForCells = Vector2Int.zero;
        blockInFigure = 3;
        sizeBlock *= 2;
        Merged?.Invoke();
    }
    public static void LevelUp()
    {
        blockInFigure++;
    }
}
