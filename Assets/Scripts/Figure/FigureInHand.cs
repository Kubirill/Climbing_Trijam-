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

        public void Initialize()
        {
                
            foreach (var button in _buttons)
            {
                button.MouseDown += ButtonClicked;
            }
        }

        private void ButtonClicked(FigureHolder holder)
        {

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
            if (LevelStats.gameActiveBlock.Count > 0) return;
            MoveFigure();
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
                RotateFigure(true);
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _holder.Figure.RotateFigure(false);
                RotateFigure(false);
            }
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
            {
                _holder.Figure.FlipFigure(false);
                FlipFigure(false);
            }
            if (Input.GetKeyDown(KeyCode.D)|| Input.GetKeyDown(KeyCode.A))
            {
                _holder.Figure.FlipFigure(true);
                FlipFigure(true);

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
        private void RotateFigure(bool inRight)
        {
            LevelStats.gameActiveBlock.Add("Manipulate");
            //_holder.transform.Rotate(Vector3.forward, inRight ? -90: 90);
            StartCoroutine (_holder.Rotation(inRight ? "RR" : "RL"));
            BlockChange?.Invoke();
        }
        

        private void FlipFigure (bool horizontal)
        {
            /* LevelStats.gameActiveBlock.Add("Manipulate");
             _holder.transform.localScale = new Vector3((
                 horizontal ? -1 : 1)*_holder.transform.localScale.x,
                 (horizontal ? 1 : -1) * _holder.transform.localScale.y, 
                 _holder.transform.localScale.z);*/
            StartCoroutine(_holder.Rotation(horizontal ? "FH" : "FV"));
            BlockChange?.Invoke();
        }
    }
}