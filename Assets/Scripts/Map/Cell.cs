using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using JetBrains.Annotations;

public class Cell : MonoBehaviour
{
    private Vector2Int _position;
    private int _stepToDelete;
    [SerializeField] private List<GameObject> _ghost;
    [SerializeField] private GameObject _pointText;
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
        transform.DOShakeScale(2, 0.5f);
        GetComponent<SpriteRenderer>().sortingOrder = 0;
    }
    public void SetPosition(Vector2Int pos)
    {
        _position = pos;
    }
    public void SetStepToDelete(int step)
    {
        transform.DOShakePosition(1,0.1f);
        _stepToDelete = step;
    }

    public event Action<Vector2Int> MouseEnter;
    public event Action MouseExit;

    
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
    private void OnMouseExit()
    {
        MouseExit?.Invoke();
    }
    public void DeleteBlock(int step)
    {
        if (_stepToDelete == step) 
        {
            
            //_stepToDelete = 0;
            Refresh?.Invoke(_position + LevelStats.offsetForCells);
            var ghost= Instantiate(_ghost[0], transform.position,Quaternion.identity);
            ghost.transform.localScale = Vector3.one* LevelStats.sizeBlock / 2;
            SpriteRenderer renderer;
            if (ghost.TryGetComponent<SpriteRenderer>(out renderer))
            renderer.sprite = LevelStats._icons._mainFigure;
            
            if (_ghost.Count > 1)
            {
                _ghost.RemoveAt(0);
                if (PointsManager.GetCurrentBlockClosedCost() > 0)
                {
                    ghost = Instantiate(_pointText, transform.position, Quaternion.identity);
                    ghost.transform.localScale *=  LevelStats.sizeBlock / 2;
                    ghost.transform.GetComponentInChildren<TMPro.TMP_Text>().text =
                        PointsManager.GetCurrentBlockClosedCost().ToString();
                }
                SoundManager.LaunchSound(SoundType._blockDestroy);
            }
            else
            {
                SoundManager.LaunchSound(SoundType._elementDestroy);
                if (PointsManager.GetCurrentBlockCost() > 0)
                {
                    ghost = Instantiate(_pointText, transform.position, Quaternion.identity);
                    ghost.transform.localScale *=  LevelStats.sizeBlock / 2;
                    ghost.transform.GetComponentInChildren<TMPro.TMP_Text>().text =
                        PointsManager.GetCurrentBlockCost().ToString();
                }
            }

            

        }
        
    }
    public void MergeDestroy()
    {
        Refresh?.Invoke(_position + LevelStats.offsetForCells);
        var ghost = Instantiate(_ghost[0], transform.position, Quaternion.identity);
        ghost.transform.localScale = Vector3.one * LevelStats.sizeBlock / 2;
        SpriteRenderer renderer;
        if (ghost.TryGetComponent<SpriteRenderer>(out renderer))
            renderer.sprite = LevelStats._icons._mainFigure;
        if (PointsManager.GetCurrentBlockMergedCost() > 0)
        {
            ghost = Instantiate(_pointText, transform.position, Quaternion.identity);
            ghost.transform.localScale *= LevelStats.sizeBlock / 2;
            ghost.transform.GetComponentInChildren<TMPro.TMP_Text>().text =
                PointsManager.GetCurrentBlockMergedCost().ToString();
        }
    }
}
