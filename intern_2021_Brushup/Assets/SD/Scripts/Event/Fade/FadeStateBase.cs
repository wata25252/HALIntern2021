/*-------------------------------------------------------
 * 
 *      [FadeStateBase.cs]
 *      フェードステートのbaseクラス
 *      Author : 出合翔太
 * 
 -------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SD
{
    public abstract class FadeStateBase : MonoBehaviour
    {
        public abstract void Execute(Fade fade);
    }
}
