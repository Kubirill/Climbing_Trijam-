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
    public static GameIcons _icons;
    public static void NewGame()
    {
        sizeBlock = 2;
        offsetForCells =Vector2Int.zero;
        blockInFigure = 3;
        gameActiveBlock = new List<string>(); 
    }
    public static event Action Merged;
    public static event Action MergeStart;
    public static void MergeCompleete()
    {
        Merged?.Invoke();
        gameActiveBlock.Remove("Merge");
    }
    public static void  LaunchMerge()
    {
        gameActiveBlock.Add("Merge");
        offsetForCells = Vector2Int.zero;
        blockInFigure = 3;
        sizeBlock *= 2;
        MergeStart?.Invoke();
        
    }


    public static void LevelUp()
    {
        blockInFigure++;
    }
    public static void AddPoints(int point)
    {
        points=points+point* sizeBlock/2;
        

    }
    
}
