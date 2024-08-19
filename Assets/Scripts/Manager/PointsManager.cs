using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PointsManager 
{
    static CostPoints _costPoints;
    public static int _points = 0;
    public static int _lines = 0;
    public PointsManager(CostPoints costPoints)
    {
        _costPoints = costPoints;
       MapController.BlockDestroyed += DestroyBlock;
    }

    private void DestroyBlock(int typeBlock)
    {
        if (_costPoints.isSimpleScore&&( typeBlock > 0))
        {
            _points+=_costPoints.pointsForBlock*_lines;
            
        }
        if (_costPoints.isClosedScore && (typeBlock < 0))
        {
            _points += _costPoints.pointsForClosedBlock * _lines;

        }
    }
    public static int GetCurrentBlockCost()
    {
        return _costPoints.pointsForBlock * _lines;
    }
    public static int GetCurrentBlockClosedCost()
    {
        return _costPoints.pointsForClosedBlock * _lines;
    }
    public static int GetCurrentBlockMergedCost()
    {
        return _costPoints.pointsForBlockAfterMerge;
    }
}
