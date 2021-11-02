/*-------------------------------------------------------
 * 
 *      [SnowMove.cs]
 *      雪の落下
 *      Author : 出合翔太
 * 
 -------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SD
{
    public class SnowMove : MonoBehaviour
    {
        // 落下速度
        private float _speed = -1.0f;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            // 位置を更新
            this.gameObject.transform.position += new Vector3(0.0f, _speed, 0.0f);
        }
    }
}
