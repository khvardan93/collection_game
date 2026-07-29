using System;
using Core.DI;
using UnityEngine;
using Core.Services;
using Random = UnityEngine.Random;

namespace GamePlay
{
    public class CollectableSpawner : MonoBehaviour
    {
        [SerializeField] private Transform[] _borders;
        
        [SerializeField] private int _spawnCount = 10;//TODO remove
        [SerializeField] private float _heightAboveSurface = 1f;
        [Header("Raycast Settings")]
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _raycastStartHeight = 50f;
        [SerializeField] private float _raycastMaxDistance = 200f;
        [SerializeField] private int _maxAttemptsPerSpawn = 30;

        private readonly RaycastHit[] _raycastHits = new RaycastHit[1];

        private ICollection _collection;

        private void Awake()
        {
            _collection = ServiceLocator.Container.Resolve<ICollection>();
        }

        private void Start()
        {
            Invoke(nameof(SpawnCollectables), 2f);
        }

        public void SpawnCollectables()
        {
            if (_collection is null)
            {
                Debug.LogWarning("CollectableSpawner: no collectable prefabs assigned.", this);
                return;
            }

            if (_borders == null || _borders.Length != 4)
            {
                Debug.LogWarning("CollectableSpawner: expected 4 border transforms (top, bottom, left, right).", this);
                return;
            }

            var bounds = GetSpawnBounds();

            for (int j = 0; j < _collection.Items.Count; j++)
            {
                var prefab = _collection.Items[j].Prefab;
                
                for (var i = 0; i < _spawnCount; i++)
                {
                    if (!TryGetRandomGroundPoint(bounds, out var spawnPoint))
                    {
                        Debug.LogWarning("CollectableSpawner: failed to find a valid ground point after max attempts.", this);
                        continue;
                    }

                    Instantiate(prefab, spawnPoint, Quaternion.identity);
                }
            }
        }

        private bool TryGetRandomGroundPoint(Bounds bounds, out Vector3 spawnPoint)
        {
            for (var attempt = 0; attempt < _maxAttemptsPerSpawn; attempt++)
            {
                var randomX = Random.Range(bounds.min.x, bounds.max.x);
                var randomZ = Random.Range(bounds.min.z, bounds.max.z);
                var rayOrigin = new Vector3(randomX, _raycastStartHeight, randomZ);

                var hitCount = Physics.RaycastNonAlloc(rayOrigin, Vector3.down, _raycastHits, _raycastMaxDistance, _layerMask);
                if (hitCount > 0 && _raycastHits[0].collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    Debug.Log(LayerMask.LayerToName(_raycastHits[0].collider.gameObject.layer));
                    spawnPoint = _raycastHits[0].point + Vector3.up * _heightAboveSurface;
                    return true;
                }
            }

            spawnPoint = Vector3.zero;
            return false;
        }

        private Bounds GetSpawnBounds()
        {
            var bounds = new Bounds(_borders[0].position, Vector3.zero);
            for (var i = 1; i < _borders.Length; i++)
            {
                bounds.Encapsulate(_borders[i].position);
            }

            return bounds;
        }
    }
}