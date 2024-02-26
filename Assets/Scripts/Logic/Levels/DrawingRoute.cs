﻿using System;
using UnityEngine;

namespace Logic.Levels
{
    public class DrawingRoute : MonoBehaviour
    {
        public event Action FragmentDrawn;
        public event Action DrawingCompleted;
        
        private bool _drawingActivity;
        private EdgeCollider2D[] _colliders;
        private int _currentArrayOfPoints;

        [Header("Карандаш")]
        [SerializeField] private Transform _pencil;
        
        private Vector2[] _points;
        private int _currentTargetIndex;
        private Vector2 _currentTarget;

        private const float LineWidth = 0.012f;

        [Header("Скорость рисования")]
        [SerializeField] private float _speed;

        [Header("Ссылки на компоненты")]
        [SerializeField] private LineRenderer _lineRenderer;

        public void ChangeDrawingActivity(bool state) =>
            _drawingActivity = state;

        public void PrepareRouteForPencil(EdgeCollider2D[] colliders)
        {
            _colliders = colliders;
            _points = _colliders[_currentArrayOfPoints].points;
            
            _pencil.transform.localPosition = _points[_currentTargetIndex];
            _pencil.gameObject.SetActive(true);
            
            _lineRenderer.startWidth = _lineRenderer.endWidth = LineWidth;
            _lineRenderer.positionCount = 1;
            _lineRenderer.SetPosition(0, _pencil.transform.localPosition);
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
                    
                    _lineRenderer.positionCount++;
                    _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, _pencil.transform.localPosition);
                    
                    if (_pencil.transform.localPosition == (Vector3)_currentTarget)
                        _currentTargetIndex++;
                }
                else
                {
                    FragmentDrawn?.Invoke();
                    _lineRenderer.positionCount = 1;
                    _lineRenderer.SetPosition(0, _pencil.transform.localPosition);
                    
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

        private void OnDestroy()
        {
            FragmentDrawn = null;
            DrawingCompleted = null;
        }
    }
}