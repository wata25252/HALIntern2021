using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SD
{
    public class TitleState2nd : TitleStateBase
    {
        public override void Begin()
        {
            _titleName = "市民公園級";
            _peopleRequiredNextTitle = 10000;
        }
        public override void Tick(TitleStateManager mgr, GameObject player)
        {
            if(IsTitkeGoesUp(player))
            {
                mgr.ChangeTitle(new TitleState3rd());
            }
        }
    }
}
