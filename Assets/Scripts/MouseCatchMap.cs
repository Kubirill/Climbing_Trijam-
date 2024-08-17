using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Map))]
public class MouseCatchMap : MonoBehaviour
{
    [SerializeField] private Map _map;
    [SerializeField] private SetFigure figureHolder;
    private Vector2Int _mousePonter;

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(Camera.main.pixelWidth/2, Camera.main.pixelHeight/2,0));
        _mousePonter = new Vector2Int (Mathf.RoundToInt((mousePos.x-1)/2), Mathf.RoundToInt((mousePos.y-1)/2));
        
    }
    private void OnMouseDown()
    {
        print(_mousePonter);
        //Debug.Log(_map.CheckSpace(figureHolder.Figure, _mousePonter,figureHolder.Offset));

    }
}
