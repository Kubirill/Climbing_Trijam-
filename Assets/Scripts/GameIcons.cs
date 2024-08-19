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
    [SerializeField] public AudioClip _figurePlace;
}
