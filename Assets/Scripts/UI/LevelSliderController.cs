using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSliderController : MonoBehaviour
{
    [SerializeField] Slider _slider;


    public void Initialize()
    {
        Timer.TimerStop += UpdateSlider;
    }

    private void UpdateSlider()
    {

        StartCoroutine( SliderChange(DifficultyManager._currentBlock * 1f /
                (DifficultyManager._levelUpBlock + 1)));

    }
    private IEnumerator SliderChange(float endChange)
    {
        float targetSlide = endChange;
        float startpoint = _slider.value;
        if (startpoint > endChange)
        {
            targetSlide = 1;
        }
        float progress = 0;
        while (progress < 1)
        {
            progress += Time.deltaTime * 1;
            _slider.value = Mathf.Lerp(startpoint, targetSlide, progress);
            
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
        if (startpoint > endChange) {
            SoundManager.LaunchSound(SoundType._levelUp);
            progress = 0;
            while (progress < 1)
            {
                progress += Time.deltaTime * 1;
                _slider.value = Mathf.Lerp(0, endChange, progress);

                yield return new WaitForEndOfFrame();
            }
        }
        
    }
}
