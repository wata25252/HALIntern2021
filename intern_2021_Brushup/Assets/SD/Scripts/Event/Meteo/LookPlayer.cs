/*-------------------------------------------------------
 * 
 *      [LookPlayer.cs]
 *      プレイヤーの方向を見る
 *      Author : 出合翔太
 * 
 --------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookPlayer : MonoBehaviour
{
    private GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // プレイヤーの方向を見る
        this.transform.LookAt(_player.transform);
    }
}
