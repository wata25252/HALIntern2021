using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TM
{
    public class ExplosionUponDestruction : MonoBehaviour
    {
        [SerializeField] private GameObject _explosionPrefab;
        private bool _isQuitting = false;

        private void OnApplicationQuit() => _isQuitting = true;

        private void OnDestroy()
        {
            if (!_isQuitting)
            {
                Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}