﻿using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts
{
    public class FigureInHand : MonoBehaviour
    {
        [SerializeField] private FigureHolder _holder;
        [SerializeField] private List<ButtonForFigure> _buttons;
        

        public event Action BlockChange;
        public event Action BecameInactive;

        public void Initialize()
        {
                
            foreach (var button in _buttons)
            {
                button.MouseDown += ButtonClicked;
            }
        }
        private void OnDestroy()
        {
            foreach (var button in _buttons)
            {
                button.MouseDown -= ButtonClicked;
            }
        }
        private void ButtonClicked(FigureHolder holder)
        {
            SoundManager.LaunchSound(SoundType._buttonPress);
            foreach (var button in _buttons)
            {
                button.returnFigure(holder);
            }
            if (holder != _holder)
            {
                
                _holder = holder;
                _holder.transform.parent = transform;
                _holder.transform.localPosition = Vector3.zero;
                float xSign = Mathf.Sign(_holder.transform.localScale.x);
                float ySign = Mathf.Sign(_holder.transform.localScale.y);
                Vector3 newScale = new Vector3 (xSign, ySign, 1);
                _holder.transform.localScale = newScale * LevelStats.sizeBlock/2; //_holder.transform.localScale / _buttons[0].ScaleFigure;//Scale Figure
            }
            else
            {
                _holder = null;
            }
        }

        public FigureInfo GetFigure()
        {
            if (_holder == null)
            {
                return null;
            }
            return _holder.Figure;
        }

        private void Update()
        {

            if (_holder == null) return;
            MoveFigure();
            if (LevelStats.gameActiveBlock.Count > 0) return;
            
            ManipulateFigure();
        }

        private void MoveFigure()
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos;
        }

        private void ManipulateFigure()
        {
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                _holder.Figure.RotateFigure(true);
                StartCoroutine(RotateFigure(true));
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _holder.Figure.RotateFigure(false);
                StartCoroutine( RotateFigure(false));
            }
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
            {
                _holder.Figure.FlipFigure(false);
                StartCoroutine(FlipFigure(false));
            }
            if (Input.GetKeyDown(KeyCode.D)|| Input.GetKeyDown(KeyCode.A))
            {
                _holder.Figure.FlipFigure(true);
                StartCoroutine(FlipFigure(true));

            }
        }

        public void ClearFigure()
        {
            _holder.ClearFigure();
            foreach (var button in _buttons)
            {
                button.returnFigure(_holder);
            }
        }

            
        public void ChangeFigure()
        {
            
            
            foreach (var button in _buttons)
            {
                button.returnFigure(_holder,true);
            }
           // _holder.ChangeFigure();
            _holder = null;
        }
        private IEnumerator RotateFigure(bool inRight)
        {
            LevelStats.gameActiveBlock.Add("Manipulate");
            BecameInactive?.Invoke();
            //_holder.transform.Rotate(Vector3.forward, inRight ? -90: 90);
            yield return (_holder.Rotation(inRight ? "RR" : "RL"));
           
            LevelStats.gameActiveBlock.Remove("Manipulate");
            BlockChange?.Invoke();
        }
        

        private IEnumerator FlipFigure (bool horizontal)
        {
            /* LevelStats.gameActiveBlock.Add("Manipulate");
             _holder.transform.localScale = new Vector3((
                 horizontal ? -1 : 1)*_holder.transform.localScale.x,
                 (horizontal ? 1 : -1) * _holder.transform.localScale.y, 
                 _holder.transform.localScale.z);*/
            LevelStats.gameActiveBlock.Add("Manipulate");
            BecameInactive?.Invoke();
            yield return (_holder.Rotation(horizontal ? "FH" : "FV"));
            
            LevelStats.gameActiveBlock.Remove("Manipulate");
            BlockChange?.Invoke();
        }
        
        public IEnumerator WrongClick()
        {
            if (transform.localScale.x > 0.9)
            {
                transform.DOScale(Vector3.one * 0.8f, 0.2f);
                yield return new WaitForSeconds(0.2f);
                transform.DOScale(Vector3.one, 0.2f);

            }

        }
    }
}