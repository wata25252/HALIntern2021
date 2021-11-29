/*-------------------------------------------------------
 * 
 *      [GameAudio.cs]
 *      ゲーム用のオーディオクリップを管理
 *      Author : 出合翔太
 * 
 -------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SD
{
    public class GameAudio : MonoBehaviour
    {       
        [SerializeField] private AudioClip[] _audioClip;
        private AudioSource _audioSource;

        // Start is called before the first frame update
        void Start()
        {
            _audioSource = GetComponent<AudioSource>();

            // ゲームが始まったときの処理
            Begin();
        }

        // Update is called once per frame
        void Update()
        {

        }

        // ゲームが始まったときの処理
        private void Begin()
        {
            // 乱数を生成（0～クリップの数）
            int r = Random.Range(0, _audioClip.Length);

            // 乱数からクリップを設定する
            _audioSource.clip = _audioClip[r];

            // サウンドを鳴らす
            _audioSource.Play();
        }
    }
}
