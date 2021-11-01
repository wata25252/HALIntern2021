using NCMB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoard {
	
	public int currentRank = 0;
	public List<NCMB.Rankers> topRankers = null;
	
	//【mBaaS】保存したデータの検索と取得   
	public void fetchTopRankers() {
		// **********【問題２】ランキング表示しよう！**********
		NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("GameScore");

		query.OrderByDescending("score");

		query.Limit = 5;

		query.FindAsync((List<NCMBObject> objList, NCMBException e) => {
			if (e != null)
			{
				Debug.Log("検索に失敗しました。エラーコード：" + e.ErrorCode);
			}
			else
			{
				Debug.Log("検索に成功しました。");

				List<NCMB.Rankers> list = new List<NCMB.Rankers>();
				foreach (NCMBObject obj in objList)
				{
					int s = System.Convert.ToInt32(obj["score"]);
					string n = System.Convert.ToString(obj["name"]);
					list.Add(new Rankers(s, n));
				}
				topRankers = list;
			}
		});

		// ***********************************************
	}

}

