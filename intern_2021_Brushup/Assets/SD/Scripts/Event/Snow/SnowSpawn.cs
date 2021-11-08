/*-------------------------------------------------------
 * 
 *      [SnowSpawn.cs]
 *      雪を降らせる
 *      Author : 出合翔太
 * 
 -------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowSpawn : MonoBehaviour
{
    [SerializeField] GameObject _snow;
    
    // Start is called before the first frame update
    void Start()
    {
          
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 雪のパーティクルのスポーン
    public GameObject Spawn()
    {
        var obj = Instantiate(_snow, this.transform);
        return obj;
    }
}
