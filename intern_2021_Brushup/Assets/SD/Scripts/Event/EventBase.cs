/*-------------------------------------------------------
 * 
 *      [EventBase.cs]
 *      イベントのベース
 *      Author : 出合翔太
 * 
 ------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SD
{
    public abstract class EventBase : MonoBehaviour
    {
        protected float _nowTime;

        public abstract void Begin();   // はじめ
        public abstract void Tick(GameEvent gameEvent);    // 更新

        public abstract void End(GameEvent gameEvent); // 終了
    }
}
