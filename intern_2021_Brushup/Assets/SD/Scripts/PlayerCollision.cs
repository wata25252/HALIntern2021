/*-------------------------------------------------------
 * 
 *      [PlayerCollisoin.cs]
 *      プレイヤーの当たり判定
 *      Author : 出合翔太
 *      いじりました（古寺）
 -------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SD
{


    public class PlayerCollision : MonoBehaviour
    {
        private GameObject _ranking;

        private GameObject _gameManager;

        private bool isdead = false;
        // Start is called before the first frame update
        void Start()
        {
            _ranking = GameObject.Find("ranking");
            _gameManager = GameObject.Find("GameManager");
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ground"))
            {
                ShowResult();
            }
        }

        private void ViewRanking()
        {
            _ranking.GetComponent<NCMB_leaderboard>().ViewRanking();
        }
        private void SaveScore()
        {
            _ranking.GetComponent<NCMB_leaderboard>().SaveScore("YOU", (int)gameObject.GetComponent<TM.Player>().CrewCount);
        }
        public bool IsDead()
        {
            return isdead;
        }

        // リザルト
        public void ShowResult()
        {
            if (!isdead)
            {
                // ゲームを止める
                _gameManager.GetComponent<TK.GameManager>().GameStop();

                Invoke("SaveScore", 0.1f);
                Invoke("ViewRanking", 1.0f);
                isdead = true;
            }
        }
    }
}
