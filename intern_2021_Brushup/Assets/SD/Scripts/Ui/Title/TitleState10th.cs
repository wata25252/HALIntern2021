using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SD
{
    public class TitleState10th : TitleStateBase
    {
        public override void Begin()
        {
            _titleName = "ホーステンボス級";
            _peopleRequiredNextTitle = 50000;
        }

        public override void Tick(TitleStateManager mgr, GameObject player)
        {
            if (IsTitkeGoesUp(player))
            {
                mgr.ChangeTitle(new TitleState11th());
            }
        }
    }
}
