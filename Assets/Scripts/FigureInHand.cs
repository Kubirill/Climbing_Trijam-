using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.WSA;

namespace Assets.Scripts
{
    public class FigureInHand : MonoBehaviour
    {
        [SerializeField] private FigureHolder _holder;
        [SerializeField] private List<ButtonForFigure> _buttons;

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
            print(holder != _holder);
            if (holder != _holder)
            {
                
                _holder = holder;
                _holder.transform.parent = transform;
                _holder.transform.localPosition = Vector3.zero;
                _holder.transform.localScale = Vector3.one;
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
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos;
        }
        
        public void ChangeFigure()
        {
            _holder.ChangeFigure();
            _holder = null;
            foreach (var button in _buttons)
            {
                button.returnFigure();
            }
        }
    }
}