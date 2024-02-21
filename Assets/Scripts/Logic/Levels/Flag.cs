using UnityEngine;

namespace Logic.Levels
{
    public class Flag : MonoBehaviour
    {
        private bool _isFinalized;

        [Header("Маршруты рисования")]
        [SerializeField] private EdgeCollider2D[] _colliders;

        public EdgeCollider2D[] Colliders => _colliders;
    }
}