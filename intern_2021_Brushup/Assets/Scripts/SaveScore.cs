using NCMB; //mobile backendのSDKを読み込む
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// 【mBaaS】データの保存
public class SaveScore : MonoBehaviour {
	public void save( string name, int score ) {
        // **********【問題１】名前とスコアを保存しよう！**********
        NCMBObject obj = new NCMBObject("GameScore");
        obj["name"] = name;
        obj["score"] = score;
        obj.SaveAsync((NCMBException e) => {
            if (e != null)
            {
                Debug.Log("保存に失敗しました。エラーコード:" + e.ErrorCode);
                //エラー処理
            }
            else
            {
                Debug.Log("保存に成功しました。ObjectId:" + obj.ObjectId);

                //成功時の処理
            }
        });









        // **************************************************
    }
}