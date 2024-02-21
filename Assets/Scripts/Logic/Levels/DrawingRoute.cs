using System;
using UnityEngine;

namespace Logic.Levels
{
    public class DrawingRoute : MonoBehaviour
    {
        private bool _drawingActivity;

        public event Action DrawingCompleted;
        
        private EdgeCollider2D[] _colliders;
        private int _currentArrayOfPoints;

        [Header("Карандаш")]
        [SerializeField] private Transform _pencil;
        
        private Vector2[] _points;
        private int _currentTargetIndex;

        private Vector2 _currentTarget;

        [Header("Скорость рисования")]
        [SerializeField] private float _speed;

        public void ChangeDrawingActivity(bool state) =>
            _drawingActivity = state;

        public void PrepareRouteForPencil(EdgeCollider2D[] colliders)
        {
            _colliders = colliders;
            _points = _colliders[_currentArrayOfPoints].points;
            
            _pencil.transform.localPosition = _points[_currentTargetIndex];
            _pencil.gameObject.SetActive(true);
        }

        private void Update()
        {
            if (_drawingActivity == false)
                return;
            
            if (Input.GetMouseButton(0))
            {
                if (_currentTargetIndex < _points.Length)
                {
                    _currentTarget = _points[_currentTargetIndex];
                    _pencil.transform.localPosition = Vector3.MoveTowards(_pencil.transform.localPosition, _currentTarget, _speed * Time.deltaTime);
                    
                    if (_pencil.transform.localPosition == (Vector3)_currentTarget)
                        _currentTargetIndex++;
                }
                else
                {
                    if (_currentArrayOfPoints < _colliders.Length - 1)
                    {
                        _currentArrayOfPoints++;
                        _points = _colliders[_currentArrayOfPoints].points;
                        _currentTargetIndex = 0;
                        _pencil.transform.localPosition = _points[_currentTargetIndex];
                    }
                    else
                    {
                        ChangeDrawingActivity(state: false);
                        _pencil.gameObject.SetActive(false);
                        DrawingCompleted?.Invoke();
                    }
                }
            }
        }

        private void OnDestroy() =>
            DrawingCompleted = null;
    }
}