using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SD
{
    public class TitleState4th : TitleStateBase
    {
        public override void Begin()
        {
            _titleName = "エゴランド級";
            _peopleRequiredNextTitle = 20000;
        }
        public override void Tick(TitleStateManager mgr, GameObject player)
        {
            if (IsTitkeGoesUp(player))
            {
                mgr.ChangeTitle(new TitleState5th());
            }
        }
    }
}
