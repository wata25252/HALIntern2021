/*--------------------------------------------------------
 * 
 *      [ClockCollsion.cs]
 *      時計塔の処理
 *      Author : 出合翔太
 * 
 --------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SD
{
    public class ClockCollison : MonoBehaviour
    {
        private GameObject _gameManager;
        private SE _se; 
        private float _aadTime;        

        // Start is called before the first frame update
        void Start()
        {
            _gameManager = GameObject.Find("GameManager");
            _se = GameObject.Find("SEManager").GetComponent<SE>();
        }

        // Update is called once per frame
        void Update()
        {
            _aadTime = 10;        }

        private void FixedUpdate()
        {
            
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                _gameManager.GetComponent<TK.GameManager>().AddTime(_aadTime);
                _se.Play(6);
                Destroy(this.gameObject);
                Debug.Log("Triggr");
            }
        }
    }
}