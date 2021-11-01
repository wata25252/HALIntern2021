/*-------------------------------------------------------
 * 
 *      [TimerDestory.cs]
 *      一定の時間が着たら、Destory（）するオブジェクトに付ける
 *      Author : 出合翔太
 * 
 -------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerDestory : MonoBehaviour
{
    [Header("オブジェクトが残っている時間")]
    [SerializeField] private float _time;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, _time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
