using Core.DI;
using Core.Services;
using UnityEngine;

namespace GamePlay
{
    public class CollectableController : MonoBehaviour
    {
        [SerializeField] private BaseCollectionItem _config;
        [SerializeField] private AudioSource _audioSource;
        
        private IInventory _inventory;
        
        private void Awake()
        {
            _inventory = ServiceLocator.Container.Resolve<IInventory>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            _inventory.Place(_config.Type, _config.Name, 1);
            _audioSource.Play();
            
            Destroy(gameObject, _audioSource.clip.length);
        }
    }
}