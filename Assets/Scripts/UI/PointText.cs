using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointText : MonoBehaviour
{

    [SerializeField] TMP_Text _points;

    public void Initialize()
    {
        Timer.TimerStop += UpdateText;
    }
    private void UpdateText()
    {
        transform.DOShakeScale(1, 0.5f);
        StartCoroutine(TextChange());
    }
    private IEnumerator TextChange()
    {
        int startpoint = int.Parse(_points.text);
        float progress = 0;
        while (progress < 1)
        {
            progress += Time.deltaTime * 1;
            int points = Mathf.RoundToInt( Mathf.Lerp(startpoint, PointsManager._points, progress));
            _points.text = points.ToString();
            yield return new WaitForEndOfFrame();
        }
        _points.text = PointsManager._points.ToString();
    }
}
