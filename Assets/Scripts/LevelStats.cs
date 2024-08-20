using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public static class LevelStats 
{
    public static Vector2Int offsetForCells =Vector2Int.zero;
    public static int blockInFigure = 3;
    public static int sizeBlock = 2;
    public static List<string> gameActiveBlock = new List<string>();
    public static int points = 0;
    public static int curLevel = 6;
    public static int mergeCount=0;
    public static GameIcons _icons;
    public static void NewGame()
    {
        mergeCount = 0;
        sizeBlock = 2;
        offsetForCells =Vector2Int.zero;
        blockInFigure = 3;
        gameActiveBlock = new List<string>(); 
    }
    public static event Action<Vector2Int> Merged;
    public static event Action MergeStart;
    public static void MergeCompleete(Vector2Int size)
    {
        Merged?.Invoke(size);
        gameActiveBlock.Remove("Merge");
        mergeCount++;
    }
    public static void  LaunchMerge()
    {
        MergeStart?.Invoke();
        

    }

    public static void UpdateParam()
    {
        gameActiveBlock.Add("Merge");
        offsetForCells = Vector2Int.zero;
        blockInFigure = 4;
        //sizeBlock *= 2;
    }

    public static void LevelUp()
    {
        blockInFigure++;
    }
    
}
