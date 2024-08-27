using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using System;
using DG.Tweening;

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
        StartCoroutine(SquereChange(new Vector2(current.x * 1f / max.x, current.y * 1f / max.y)));
    }
    private IEnumerator SquereChange(Vector2 endChange)
    {
        Vector2 startpoint = _mapExample.transform.localScale;
       
        float progress = 0;
         while (progress < 1)
        {
            progress += Time.deltaTime * 2;
            _mapExample.transform.localScale = Vector2.Lerp(startpoint, endChange, progress);

            yield return new WaitForEndOfFrame();
        }
    }

    public void Merge()
    {
        _mapExample.gameObject.SetActive(false);
        _mergeWarning.gameObject.SetActive(true);
        //_mergeWarning.transform.DOShakeScale(5, 0.1f);
    }
    public void FinishMerge(Vector2Int size)
    {
        _mapExample.gameObject.SetActive(true);
        _mergeWarning.gameObject.SetActive(false);
        Resize(size, maxSize);
    }
}
