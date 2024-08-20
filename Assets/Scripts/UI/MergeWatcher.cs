using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using System;

public class MergeWatcher : MonoBehaviour
{

    [SerializeField] TMP_Text _mergeWarning;
    [SerializeField] RectTransform _mapExample;
    Vector2Int maxSize;
    //[SerializeField] RectTransform _mapExample;

    private void Awake()
    {
        LevelStats.Merged += FinishMerge;
    }
    private void OnDestroy()
    {
        LevelStats.Merged -= FinishMerge;

    }
    public void Resize(Vector2Int current, Vector2Int max)
    {
        maxSize = max;
        // Debug.Log("Resize");
        current = current - Vector2Int.one * 3;
        max = max - Vector2Int.one * 3;
        _mapExample.transform.localScale=new Vector2(current.x*1f/ max.x, current.y * 1f / max.y);
    }

    public void Merge()
    {
        _mapExample.gameObject.SetActive(false);
        _mergeWarning.gameObject.SetActive(true);
    }
    public void FinishMerge(Vector2Int size)
    {
        _mapExample.gameObject.SetActive(true);
        _mergeWarning.gameObject.SetActive(false);
        Resize(size, maxSize);
    }
}
