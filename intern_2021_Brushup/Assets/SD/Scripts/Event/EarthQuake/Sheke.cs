/*------------------------------------------------------
 *  
 *      [Sheke,cs]
 *      カメラを揺らす
 *      Author : 出合翔太
 * 
 ------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SD
{
    public class Sheke : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        // カメラを揺らす
        public void CameraSheke()
        {
            //
            GetComponent<Cinemachine.CinemachineImpulseSource>().GenerateImpulse();
        }
    }
}
