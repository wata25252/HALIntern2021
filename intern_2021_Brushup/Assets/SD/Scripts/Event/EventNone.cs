/*-------------------------------------------------------
 * 
 *      [EventNone.cs]
 *      ゲームイベントなし
 *      Author : 出合翔太
 * 
 -------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SD
{
    public class EventNone : EventBase
    {
        private GameObject _playerCollider; // プレイヤー
        private float _eventSpan = 5.0f;    // イベントスパン

        public override void Begin()
        {
            _playerCollider = GameObject.Find("Player/Collider");
            // 物理マテリアルの切り替える
            _playerCollider.GetComponent<PhysicMatList>().Change(0);            
        }

        public override void Tick(GameEvent gameEvent)
        {
            _nowTime += Time.deltaTime;
            if (_nowTime > _eventSpan)
            {
                _nowTime = 0;

                // 1/3の確率でイベント発生
                int i = (int)Random.Range(0, 3);
                if(i == 0)
                {
                    // イベント
                    int j = (int)Random.Range(0, 3);
                    switch (j)
                    {
                        case 0:
                            gameEvent.ChangeEvent(new EarthQuake());
                            break;
                        case 1:
                            gameEvent.ChangeEvent(new Snow());
                            break;
                        case 2:
                            gameEvent.ChangeEvent(new Meteorite());
                            break;
                    }

                }                
            }
        }

        public override void End(GameEvent gameEvent)
        {
            
        }
    }
}
