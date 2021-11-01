/*-------------------------------------------------------
 * 
 *      [Norendering.cs]
 *      ゲームが開始したら、レンダリングしないオブジェクトにアタッチする
 *      Author : 出合翔太
 * 
 -------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SD
{
    public class NoRendering : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            // ゲーム中はレンダリングしない
            Renderer renderer = GetComponent<Renderer>();
            renderer.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}