using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugPinrts : MonoBehaviour
{
    [SerializeField] private TMP_Text m_TextMeshPro;
    

    // Update is called once per frame
    void Update()
    {
        m_TextMeshPro.text = LevelStats.points.ToString();
    }
}
