using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class LevelStats 
{
    public static Vector2Int offsetForCells =Vector2Int.zero;
    public static int blockInFigure = 3;
    public static int sizeBlock = 2;

    public static void NewGame()
    {
        
        offsetForCells =Vector2Int.zero;
        blockInFigure = 3;
    }
}
