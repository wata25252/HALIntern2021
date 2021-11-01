using NCMB; 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NCMB_saveScore : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("start");

    }
    private void Update()
    {
        Debug.Log("update");
        if (Input.GetKeyDown(KeyCode.O))
        {
            save("NODATA", 0);
        }
    }
    public void save(string name, int score)
    {
        NCMBObject obj = new NCMBObject("GameScore");
        obj["name"] = "test";
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
    }
}