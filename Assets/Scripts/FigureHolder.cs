using System.Collections;
using UnityEngine;

public class FigureHolder : MonoBehaviour
{
    [SerializeField] private FigureInfo _figure;

    [SerializeField] public Transform _animation;
    private Animator _anim;

    private Vector3 _scaleFigure;
    private float _trueScale;
    public FigureInfo Figure { get { return _figure; } }


    [SerializeField] private GameObject _blockType;
    [SerializeField] private float _cellSize;
    // Start is called before the first frame update
    public void Initialize(float trueScale)
    {
        _anim = GetComponent<Animator>();
        LevelStats.Merged += ChangeFigure;
        _scaleFigure = transform.localScale;
        _trueScale = trueScale;
        CreateFigure();
    }
    public void UpdateScale(float trueScale)
    {
        _trueScale = trueScale;
    }
    private void CreateFigure()
    {
        _figure = new FigureInfo(_blockType);
        for (int x = 0; x < _figure.Size.x; x++)
        {
            for (int y = 0; y < _figure.Size.y; y++)
            {
                if (_figure.Figure[x][y] > 0)
                {
                    Vector3 position;
                    position = (new Vector2(x, y) - _figure.Pivot) 
                        * _cellSize* _trueScale;
                    position = position + transform.position;
                    Instantiate(_blockType, position,Quaternion.identity, transform);

                }
            }
        }
        
    }
    public void ChangeFigure()
    {
        ClearFigure();
        //transform.localScale = _scaleFigure;
        transform.rotation = Quaternion.identity;
        CreateFigure();
    }

    public void ClearFigure()
    {
        while (transform.childCount > 1)
        {
            DestroyImmediate(transform.GetChild(1).gameObject);
        }
    }

    private void OnDestroy()
    {
        LevelStats.Merged -= ChangeFigure;
    }

    public  IEnumerator Rotation(string triggerName)
    {
        _animation.rotation = Quaternion.identity;
        var children = transform.GetComponentsInChildren<Transform>();
        foreach (var child in children)
        {
            if (child != _animation)
            {
                child.parent = _animation;
            }
        }

        _anim.SetTrigger(triggerName);
        yield return new WaitForEndOfFrame();
        var length = _anim.GetCurrentAnimatorStateInfo(0).length;

        float progress = 0;

        while (progress < length)
        {
            progress += Time.deltaTime;
            ChangeRotationElements();
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
        children = _animation.transform.GetComponentsInChildren<Transform>();
        foreach (var child in children)
        {
            if (child != _animation)
            {
                child.parent = transform;
            }
        }
        _animation.rotation=Quaternion.identity;
        yield return new WaitForEndOfFrame();
        

    }

    private void ChangeRotationElements()
    {
        Vector3 rot= -_animation.localEulerAngles;
         //rot = new Vector3(rot.x, 0, rot.z);
        foreach (var child in _animation.transform.GetComponentsInChildren<Transform>())
        {
            child.localEulerAngles = rot;
        }
    }
}
