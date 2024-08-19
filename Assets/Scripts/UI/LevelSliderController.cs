using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSliderController : MonoBehaviour
{
    [SerializeField] Slider _slider;


    public void Initialize()
    {
        MapController.BlockDestroyed += UpdateSlider;
    }

    private void UpdateSlider(int typeBlock)
    {
        
        if (typeBlock > 0)
        {
            
            _slider.value = DifficultyManager._currentBlock*1f / 
                (DifficultyManager._levelUpBlock+1);

        }
    }
}
