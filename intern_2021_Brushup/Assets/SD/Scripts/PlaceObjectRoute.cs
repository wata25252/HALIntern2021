/*-------------------------------------------------------
 * 
 *      [PlaceObjectRoute.cs]
 *      引いたパスに、自動的にオブジェクトを配置する
 *      Author : 出合翔太
 * 
 -------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace SD
{
    public class PlaceObjectRoute : MonoBehaviour
    {
        [Header("Cinamachineの設定")]
        [SerializeField] private CinemachinePath.PositionUnits _uints;
        [Header("配置するゲームオブジェクト")]
        [SerializeField] private GameObject _gameObject;
        [Header("オブジェクトの数")]
        [SerializeField] private uint _amount;

        private CinemachineSmoothPath _smoothPath;

        // Start is called before the first frame update
        void Start()
        {
            _smoothPath = GetComponent<CinemachineSmoothPath>();
            float interval = _smoothPath.PathLength / (_amount - 1);
            Vector3 position = Vector3.zero;
            Quaternion rotation = Quaternion.identity;
            for(float pos = 0; pos <= _smoothPath.PathLength; pos+= interval)
            {
                position = _smoothPath.EvaluatePositionAtUnit(pos, _uints);
                rotation = _smoothPath.EvaluateOrientationAtUnit(pos, _uints);
                Instantiate(_gameObject, position, rotation, transform);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
