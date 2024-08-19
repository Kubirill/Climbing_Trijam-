using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu (fileName ="Level",menuName = "Puzzle/New difficulty")]
public class Difficulty : ScriptableObject
{
    [SerializeField] public int BlockForLevelUp3;
    [SerializeField] public int BlockForLevelUp4;
    [SerializeField] public int BlockForLevelUp5;
    [SerializeField] public int BlockForLevelUp6;

    [SerializeField] public Vector2Int mapSizeForMerge;
}
