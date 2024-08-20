using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelSliderController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Slider _slider;
    [SerializeField] TMP_Text _level;
    [SerializeField] AudioClip _liquid;
    [SerializeField] List<Image> _sprites;
    public void Initialize()
    {
        Timer.TimerStop += UpdateSlider;
        LevelStats.Merged += UpdateSlider;
    }
    private void OnDestroy()
    {
        Timer.TimerStop -= UpdateSlider;
        LevelStats.Merged -= UpdateSlider;
    }
    private void UpdateSlider(Vector2Int @int)
    {
        UpdateSlider();
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
        if ((_liquid!=null)&& (LevelStats.blockInFigure == 6)) SoundManager.LaunchSound(_liquid);
        while (progress < 1)
        {
            progress += Time.deltaTime * 1;
            _slider.value = Mathf.Lerp(startpoint, targetSlide, progress);
            
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
        if (startpoint > endChange) {
            SoundManager.LaunchSound(SoundType._levelUp);
            _level.text = LevelStats.blockInFigure.ToString();
            progress = 0;
            if (_liquid != null) SoundManager.LaunchSound(_liquid);
            while (progress < 1)
            {
                progress += Time.deltaTime * 1;
                _slider.value = Mathf.Lerp(0, endChange, progress);

                yield return new WaitForEndOfFrame();
            }
        }
        if ((LevelStats.blockInFigure==6) && (_level.text == "5"))
        {
            _level.text = "6";

            SoundManager.LaunchSound(SoundType._levelUp);
        }
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ChsngeOpscity(0.3f);

    }

    private void ChsngeOpscity(float opacity)
    {
        foreach (var sprite in _sprites)
        {
            sprite.color = new Color(1, 1, 1, opacity);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ChsngeOpscity(1);

    }
}
