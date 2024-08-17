using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Map _map;
    private void Awake()
    {
        _map.Initialize();
    }
}
