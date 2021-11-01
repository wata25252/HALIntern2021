using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SD
{
    public class TitleState12th : TitleStateBase
    {
        public override void Begin()
        {
            _titleName = "ユニバーサル級";
            _peopleRequiredNextTitle = 60000;
        }
        public override void Tick(TitleStateManager mgr, GameObject player)
        {
            if (IsTitkeGoesUp(player))
            {
                mgr.ChangeTitle(new TitleState13th());
            }
        }
    }
}
