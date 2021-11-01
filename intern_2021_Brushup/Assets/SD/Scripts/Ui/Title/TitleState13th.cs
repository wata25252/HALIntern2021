using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SD
{
    public class TitleState13th : TitleStateBase
    {
        public override void Begin()
        {
            _titleName = "夢の国級";
            _peopleRequiredNextTitle = 99999;
        }
        public override void Tick(TitleStateManager mgr, GameObject player)
        {
            
        }
    }
}
