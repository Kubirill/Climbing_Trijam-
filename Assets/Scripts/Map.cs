
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private Vector2Int _gridSize;
    [SerializeField] private float _cellSize;
    [SerializeField] private Vector2 _offset;
    [SerializeField] private Transform _parent;
    private List<List<int>> _map = new List<List<int>>();


    [SerializeField] private GameObject _EmptyBlock;
    [SerializeField] private GameObject _ClosedBlock;


    // Start is called before the first frame update

    [ContextMenu("Generate grid")]
    public void GenerateGrid()
    {
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }

        for (int x = 0; x < _gridSize.x; x++)
        {
            
            for (int y = 0; y < _gridSize.y; y++)
            {
                Vector2 position;
                position = (new Vector2 ( x ,y)+ _offset) * _cellSize ;
                switch (_map[x][y])
                {
                    case 0:
                        Instantiate(_EmptyBlock, position, Quaternion.identity, _parent);
                        break;

                    default:
                        Instantiate(_ClosedBlock, position, Quaternion.identity, _parent);
                        break;
                }
              
            }
        }
    }
    [ContextMenu("CreateMap")]
    private void CreateMap()
    {
        _map = new List<List<int>>();
        for (int x = 0; x < _gridSize.x; x++)
        {
            _map.Add(new List<int>());
            for (int y = 0; y < _gridSize.y; y++)
            {
                if ((y == 0)||(y == _gridSize.y-1)||(x == 0)||(x == _gridSize.x - 1))
                {
                    _map[x].Add(-1);
                }
                else
                {
                    _map[x].Add(0);
                }
            }
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
