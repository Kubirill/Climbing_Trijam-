using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultShow : MonoBehaviour
{
    [SerializeField] TMP_Text _points;
    [SerializeField] GameObject _record;

    private void OnEnable()
    {
        _points.text = PointsManager._points.ToString();
        if (PointsManager._points> PointsManager._record) _record.SetActive(true);
    }
}
