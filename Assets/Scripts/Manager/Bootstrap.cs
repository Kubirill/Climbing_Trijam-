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
    [SerializeField] private PointText _pointText;
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private MergeWatcher _mergeWatcher;

    private DifficultyManager _difficultyManager;
    private PointsManager _pointsManager;
    private void Awake()
    {
        LevelStats.NewGame();
        _figureInHand.Initialize();
        _map.Initialize(_figureInHand);
        _camera.Initialize(_map);
        _levelSlider.Initialize();
        _difficultyManager = new DifficultyManager(_difficulty,_map, _mergeWatcher);
        _pointsManager = new PointsManager(_costPoints);
        _pointText.Initialize();
        _soundManager.Initialize();
    }
    private void OnDestroy()
    {
        _difficultyManager.Destroy();
        _pointsManager.Destroy();
    }
}
