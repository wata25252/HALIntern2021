/*-------------------------------------------------------
 * 
 *      [StartCamera.cs]
 *      シーンが開始したときのカメラ（タイトル用）
 *      Author ; 出合翔太
 * 
 -------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCamera : MonoBehaviour
{
    private float _time; // 時間計測
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
        _time += Time.deltaTime;
        if(_time > 8.0f)
        {
            this.gameObject.SetActive(false);
        }
    }
}
