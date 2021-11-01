using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TM
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField] private float _explosionForce;     // 爆破の威力
        [SerializeField] private float _explosionUpwardsModifier;    // 爆破の上方向への力
        [SerializeField] private float _explosionRadius;    // 爆破の影響範囲
        [SerializeField] private uint _lifeTime;    // 爆破エフェクトの生存時間
        private uint _lifeTimeCnt;  // 生存時間カウンター
        [SerializeField] private AnimationCurve _scaleTransition;   // サイズの推移曲線
        [SerializeField] private float _minScaleFactor;
        [SerializeField] private float _maxScaleFactor;
        private float _scaleFactor;

        private void Start()
        {
            _scaleFactor = Random.Range(_minScaleFactor, _maxScaleFactor);

            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, _explosionRadius * _scaleFactor);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.AddExplosionForce(
                        _explosionForce * _scaleFactor,
                        transform.position,
                        _explosionRadius * _scaleFactor,
                        _explosionUpwardsModifier * _scaleFactor,
                        ForceMode.Impulse);
                }
            }

            _lifeTimeCnt = _lifeTime;
        }

        private void FixedUpdate()
        {
            if (_lifeTimeCnt-- > 0)
            {
                float t = 1.0f - (_lifeTimeCnt / (float)_lifeTime);
                var scale = Vector3.one * _scaleTransition.Evaluate(t) * _scaleFactor;
                transform.localScale = scale;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}