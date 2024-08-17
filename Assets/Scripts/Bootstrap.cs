using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private MapController _map;
    [SerializeField] private FigureInHand _figureInHand;
    private void Awake()
    {
        _map.Initialize();
        _figureInHand.Initialize();
    }
}
