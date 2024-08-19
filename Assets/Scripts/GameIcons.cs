using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Cow", menuName = "Puzzle/New Icons")]
public class GameIcons : ScriptableObject
{
    [SerializeField] public Sprite _mainFigure;
    [SerializeField] public Sprite _figureOnEarthPrevision;
    [SerializeField] public Sprite _figureOnEarth;
    [SerializeField] public Sprite _figureWrong;
    [SerializeField] public Sprite _earth;

    [SerializeField] public AudioClip _figureTake;
    [SerializeField] public AudioClip _buttonPress;
    [SerializeField] public AudioClip _figureSelected;
    [SerializeField] public AudioClip _figurePlace;
    [SerializeField] public AudioClip _elementDestroy;
    [SerializeField] public AudioClip _blockDestroy;
    [SerializeField] public AudioClip _forbidenMove;
    [SerializeField] public AudioClip _levelUp;

    public AudioClip GetClip(SoundType soundType)
    {
        switch (soundType)
        {
            case SoundType._figureTake:
                return _figureTake;
            case SoundType._buttonPress:
                return _buttonPress;
            case SoundType._figureSelected:
                return _figureSelected;
            case SoundType._figurePlace:
                return _figurePlace;
            case SoundType._elementDestroy:
                return _elementDestroy;
            case SoundType._blockDestroy:
                return _blockDestroy;



            case SoundType._forbidenMove:
                return _forbidenMove;
            case SoundType._levelUp:
                return _levelUp;
            default:
                return null;

        }

    }
}
public enum SoundType
{
    _figureTake,
    _buttonPress,
    _figureSelected,
    _figurePlace,
    _elementDestroy,
    _blockDestroy,
    _forbidenMove,
    _levelUp
       

}
