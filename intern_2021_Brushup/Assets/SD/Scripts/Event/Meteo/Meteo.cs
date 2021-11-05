/*-------------------------------------------------------
 * 
 *      [Meteo.cs]
 *      隕石の処理
 *      Author : 出合翔太
 * 
 --------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SD
{
    public class Meteo : MonoBehaviour
    {
        public bool _isBreak { get; set; }

        // Start is called before the first frame update
        void Start()
        {
           
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void FixedUpdate()
        {

        }
        
        // 隕石を壊す
        public void Break()
        {
            // Destroyするまでの時間
            float r = Random.Range(0.1f, 3.0f);
            // 消す
            Destroy(this.gameObject, r);
        }
    }
}