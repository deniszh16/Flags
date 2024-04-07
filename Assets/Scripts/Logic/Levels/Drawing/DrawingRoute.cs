using Services.UpdateService;
using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;

namespace Logic.Levels.Drawing
{
    public class DrawingRoute : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public readonly ReactiveCommand FragmentDrawn = new();
        public readonly ReactiveCommand DrawingCompleted = new();
        public readonly ReactiveCommand StartDrawing = new();
        
        private bool _drawingActivity;
        private bool _tappingScreen;
        private bool _startDrawing;
        
        private EdgeCollider2D[] _colliders;
        private int _currentArrayOfPoints;

        [Header("Карандаш для рисования")]
        [SerializeField] private Transform _pencil;
        [SerializeField] private float _drawingSpeed;
        
        private Vector2[] _points;
        private int _currentTargetIndex;
        private Vector2 _currentTarget;

        private const float LineWidth = 0.012f;

        [Header("Ссылки на компоненты")]
        [SerializeField] private LineRenderer _lineRenderer;

        private IMonoUpdateService _monoUpdateService;

        public void Init(IMonoUpdateService monoUpdateService)
        {
            if (_monoUpdateService == null)
            {
                _monoUpdateService = monoUpdateService;
                _monoUpdateService.AddToUpdate(MyUpdate);
            }
        }

        public void ChangeDrawingActivity(bool state) =>
            _drawingActivity = state;

        public void PrepareRouteForPencil(EdgeCollider2D[] colliders)
        {
            _currentArrayOfPoints = 0;
            _currentTargetIndex = 0;
            
            _colliders = colliders;
            _points = _colliders[_currentArrayOfPoints].points;
            
            _pencil.transform.localPosition = _points[_currentTargetIndex];
            _pencil.gameObject.SetActive(true);
            
            _lineRenderer.startWidth = _lineRenderer.endWidth = LineWidth;
            _lineRenderer.positionCount = 1;
            _lineRenderer.SetPosition(index: 0, _pencil.transform.localPosition);
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _tappingScreen = true;

            if (_startDrawing != true)
            {
                StartDrawing.Execute();
                _startDrawing = true;
            }
        }

        public void OnPointerUp(PointerEventData eventData) =>
            _tappingScreen = false;

        private void MyUpdate()
        {
            if (_drawingActivity == false || _tappingScreen == false)
                return;
            
            if (Input.GetMouseButton(0))
            {
                if (_currentTargetIndex < _points.Length)
                {
                    _currentTarget = _points[_currentTargetIndex];
                    _pencil.transform.localPosition = Vector3.MoveTowards(_pencil.transform.localPosition, _currentTarget, _drawingSpeed * Time.deltaTime);
                    
                    _lineRenderer.positionCount++;
                    _lineRenderer.SetPosition(index: _lineRenderer.positionCount - 1, _pencil.transform.localPosition);
                    
                    if (_pencil.transform.localPosition == (Vector3)_currentTarget)
                        _currentTargetIndex++;
                }
                else
                {
                    FragmentDrawn.Execute();
                    _lineRenderer.positionCount = 1;
                    _lineRenderer.SetPosition(index: 0, _pencil.transform.localPosition);
                    ChangePointArray();
                }
            }
        }
        
        private void ChangePointArray()
        {
            if (_currentArrayOfPoints < _colliders.Length - 1)
            {
                _currentArrayOfPoints++;
                _points = _colliders[_currentArrayOfPoints].points;
                _currentTargetIndex = 0;
                _pencil.transform.localPosition = _points[_currentTargetIndex];
                _lineRenderer.SetPosition(index: 0, _pencil.transform.localPosition);
            }
            else
            {
                ChangeDrawingActivity(state: false);
                _pencil.gameObject.SetActive(false);
                DrawingCompleted.Execute();
            }
        }

        private void OnDestroy() =>
            _monoUpdateService?.RemoveFromUpdate(MyUpdate);
    }
}