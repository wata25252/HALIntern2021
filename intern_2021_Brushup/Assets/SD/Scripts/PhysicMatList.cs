/*-------------------------------------------------------
 * 
 *      [physicMatList.cs]
 *      物理マテリアルを設定する
 *      Author : 出合翔太
 * 
 -------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SD
{
    public class PhysicMatList : MonoBehaviour
    {
        [SerializeField] PhysicMaterial[] _material;

        // Start is called before the first frame update
        void Start()
        {
            this.gameObject.GetComponent<MeshCollider>().material = _material[0];
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void FixedUpdate()
        {

        }

        // マテリアルの変更
        public void Change(int i)
        {            
            this.gameObject.GetComponent<MeshCollider>().material = _material[i];
        }
    }
}
