/*-------------------------------------------------------
 * 
 *      [HumanCount.cs]
 *      収容人数をカウント
 *      Author : 出合翔太
 * 
 --------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SD
{
    public class HumanCount : MonoBehaviour
    {
        [Header("プレイヤー")]
        [SerializeField] private GameObject _player;        

        [Header("Textオブジェクト")]
        [SerializeField] private Text _text;

        private uint _count;

        public uint GetCount { get => _count; }

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
            _count = _player.GetComponent<TM.Player>().CrewCount;
            _text.text = "収容人数 : " + _count;
        }
    }
}
