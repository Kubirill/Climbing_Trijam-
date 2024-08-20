using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject _canvas;
    public static bool _paused = false;
   
    public void Press(bool nextState)
    {
        Debug.Log("press "+_paused);
        if (_paused== nextState) return;
        _paused = nextState;
        _canvas.SetActive(nextState);
        if (_paused) LevelStats.gameActiveBlock.Add("Pause");
        else LevelStats.gameActiveBlock.Remove("Pause");
    }
}
