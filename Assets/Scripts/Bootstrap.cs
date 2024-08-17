using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private MapController _map;
    private void Awake()
    {
        _map.Initialize();
    }
}
