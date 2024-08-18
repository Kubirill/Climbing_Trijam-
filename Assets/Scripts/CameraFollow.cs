using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private Vector2Int sizeMap;
    public void Initialize(MapController mapController)
    {
        
        _camera = GetComponent<Camera>();
        mapController.NewLine += Resize;
    }
    private void Resize(Vector2Int direction, Vector2Int size)
    {
        Vector3 dir = new Vector3(direction.x, direction.y, 0);
        transform.position = transform.position+ dir/2;
        if ((sizeMap.x<size.x)|| (sizeMap.y < size.y))
        {
            _camera.orthographicSize = _camera.orthographicSize+LevelStats.sizeBlock/2;
            sizeMap += Vector2Int.one;
        }
    }
}
