using UnityEngine;

namespace Logic.Levels
{
    public class CurrentLevel : MonoBehaviour
    {
        [Header("Элементы уровня")]
        [SerializeField] private GameObject _container;
        
        public void StartLevel()
        {
            _container.SetActive(true);
        }
    }
}