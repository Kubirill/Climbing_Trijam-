using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private MapController _map;
    [SerializeField] private FigureInHand _figureInHand;
    [SerializeField] private CameraFollow _camera;
    [SerializeField] Difficulty _difficulty;
    [SerializeField] CostPoints _costPoints;
    [SerializeField] private LevelSliderController _levelSlider;


    private DifficultyManager _difficultyManager;
    private PointsManager _pointsManager;
    private void Awake()
    {
        LevelStats.NewGame();
        _figureInHand.Initialize();
        _map.Initialize(_figureInHand);
        _camera.Initialize(_map);
        _levelSlider.Initialize();
        _difficultyManager = new DifficultyManager(_difficulty,_map);
        _pointsManager = new PointsManager(_costPoints);
    }
}
