using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SD
{
    public class TitleState6th : TitleStateBase
    {
        public override void Begin()
        {
            _titleName = "大阪ドイツ村級";
            _peopleRequiredNextTitle = 30000;
        }
        public override void Tick(TitleStateManager mgr, GameObject player)
        {
            if (IsTitkeGoesUp(player))
            {
                mgr.ChangeTitle(new TitleState7th());
            }
        }
    }
}
