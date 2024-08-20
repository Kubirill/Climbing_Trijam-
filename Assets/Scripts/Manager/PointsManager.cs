using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PointsManager 
{
    static CostPoints _costPoints;
    public static int _points = 0;

    public static int _record = 0;
    public static int _lines = 0;
    public PointsManager(CostPoints costPoints)
    {

       if (PlayerPrefs.HasKey("Record")) _record = PlayerPrefs.GetInt("Record");
        _costPoints = costPoints;
       MapController.BlockDestroyed += DestroyBlock;
    }
    public void Destroy()
    {
        _points = 0;
        MapController.BlockDestroyed -= DestroyBlock;
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
        if (_points > _record)
        {
            PlayerPrefs.SetInt("Record", _points);
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
