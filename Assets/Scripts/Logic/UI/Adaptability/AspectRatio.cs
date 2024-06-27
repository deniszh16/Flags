using UnityEngine;

namespace Flags.Logic
{
    public class AspectRatio : MonoBehaviour
    {
        public static float Ratio;

        [SerializeField] private Camera _camera;

        private void Awake() =>
            Ratio = _camera.aspect;
    }
}