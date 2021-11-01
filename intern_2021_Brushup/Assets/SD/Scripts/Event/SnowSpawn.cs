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
    private Vector2 _spawnPositionMax;
    // 生成する個数
    [Header("生成する雪の個数")]
    [SerializeField] private int _createNum;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().enabled = false;
        _spawnPositionMax = new Vector2(this.gameObject.transform.localScale.x / 2, this.gameObject.transform.localScale.z / 2);        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
        for (int i = 0; i < _createNum; i++)
        {
            float min = 1.0f;
            float max = Mathf.Pow(_spawnPositionMax.x, 2);

            // 出現位置を乱数で出す
            float x = Random.Range(-_spawnPositionMax.x, _spawnPositionMax.x);
            float z = Random.Range(-_spawnPositionMax.y, _spawnPositionMax.y);

            // 絶対値を出す
            float xAbs = Mathf.Abs(Mathf.Pow(x, 2));
            float zAbs = Mathf.Abs(Mathf.Pow(z, 2));

            // 範囲内かどうか
            if (max > xAbs + zAbs && xAbs + zAbs > min)
            {
                GameObject obj = Instantiate(_snow, new Vector3(x, this.transform.position.y, z), Quaternion.identity);

                // 出現から5秒たったら削除
                Destroy(obj, 3.0f);
            }
        }
    }
}
