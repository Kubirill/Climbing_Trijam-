using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

using DG.Tweening;

public class Cell : MonoBehaviour
{
    private Vector2Int _position;
    private int _stepToDelete;
    private void Awake()
    {
        LevelStats.MergeStart += AnimationBeforeMerge;
    }
    private void OnDestroy()
    {
        LevelStats.MergeStart -= AnimationBeforeMerge;
    }
    private void AnimationBeforeMerge()
    {
        transform.DOShakeScale(1, 0.5f);
    }
    public void SetPosition(Vector2Int pos)
    {
        _position = pos;
    }
    public void SetStepToDelete(int step)
    {
        LevelStats.AddPoints(1);
        transform.DOShakePosition(1,0.2f);
        _stepToDelete = step;
    }

    public event Action<Vector2Int> MouseEnter;

    
    public event Action<Vector2Int> MouseDown;
    public event Action<Vector2Int> Refresh;

    private void OnMouseEnter()
    {
        MouseEnter?.Invoke(_position+ LevelStats.offsetForCells);
    }
    private void OnMouseDown()
    {
        MouseDown?.Invoke(_position + LevelStats.offsetForCells);
    }

    public void DeleteBlock(int step)
    {
        if (_stepToDelete == step) 
        {
            
            //_stepToDelete = 0;
            Refresh?.Invoke(_position + LevelStats.offsetForCells);
        }

    }
}
