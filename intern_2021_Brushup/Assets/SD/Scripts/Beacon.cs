/*-------------------------------------------------------
 * 
 *  [Beacon.cs]
 *  Author : 出合翔太
 *  
 *  ビーコンのスクリプト
 * 
 -------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beacon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        GameObject miniMapCamera = GameObject.FindGameObjectWithTag("MiniMapCamera");
        miniMapCamera.GetComponent<SD.MiniMapCamera>().RemoveBeacon(this.gameObject);
    }
}
