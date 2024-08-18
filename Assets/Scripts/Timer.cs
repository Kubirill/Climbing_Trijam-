using System;
using System.Collections;
using UnityEngine;


    public class Timer : MonoBehaviour
    {
        [SerializeField] private float _lengthDestroy=1f;
        [SerializeField] private int _maxStep =3;
        private int _currentStep =3;


        public event Action<int> destroyBlocks;

        public IEnumerator SetUpTimer(int Steps)
        {
            _currentStep = 0;
            _maxStep = Steps;
            yield return StartCoroutine(TimerCountDown());
        }
        IEnumerator TimerCountDown()
        {
            while (_currentStep< _maxStep)
            {
                _currentStep++;
                yield return new WaitForSeconds(_lengthDestroy / _maxStep);
                destroyBlocks?.Invoke(_currentStep);
            }
            destroyBlocks=null;
        }
    }
