using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private MapController _map;
    [SerializeField] private FigureInHand _figureInHand;
    [SerializeField] private CameraFollow _camera;
    private void Awake()
    {
        LevelStats.NewGame();
        _map.Initialize();
        _figureInHand.Initialize();
        _camera.Initialize(_map);
    }
}
