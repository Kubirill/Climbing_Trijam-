using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Points", menuName = "Puzzle/New Costs")]
public class CostPoints : ScriptableObject
{
    [SerializeField] public bool isSimpleScore = true;
    [SerializeField] public int pointsForBlock=10;
    [SerializeField] public bool isMergeBlockScore = true;
    [SerializeField] public int pointsForBlockAfterMerge=30;
    [SerializeField] public bool isClosedScore = true;
    [SerializeField] public int pointsForClosedBlock=20;
}
