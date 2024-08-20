using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeHelper : MonoBehaviour
{
    [SerializeField] Camera _camera;

    public void StartMerge(Vector2 currentPos,Vector2Int size)
    {
        var x = Mathf.RoundToInt((size.x + 0.4f)/2);
        var y = Mathf.RoundToInt((size.y + 0.4f)/2);
        size =new Vector2Int(x, y);
       
        var _offset = -((size - Vector2.one) / 2f)* 2;
        var target = _offset - Vector2.one * 0.5f;
        var origin = 2 * target - currentPos;

        transform.position = origin;
        transform.localScale = Vector2.one;
    }
    public IEnumerator CameraRescale()
    {
        Vector3 startpoint = transform.localScale;
        Vector3 endpoint = transform.localScale/2;
        float progress = 0;
        while (progress < 1)
        {
            progress += Time.deltaTime * 1;
            transform.localScale = Vector3.Lerp(startpoint, endpoint, progress);
            yield return null;
        }
    }
    public void Destrouer()
    {
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }
}
