/*-------------------------------------------------------
 * 
 *      [ObjectPool.cs]
 *      オブジェクトプーリング
 *      Author : 出合翔太
 * 
 -------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private List<GameObject> _poolObjectList;
    private GameObject _poolObject;

    // オブジェクトプールを作成
    public void CreatePool(GameObject gameObject, uint maxCount)
    {
        _poolObject = gameObject;
        _poolObjectList = new List<GameObject>();

        for(int i = 0; i < maxCount; i++)
        {
            var newobj = CreateNewObject();
            newobj.SetActive(false);
            _poolObjectList.Add(newobj);
        }
    }

    public GameObject GetObject()
    {
        // 使用中でないものを探してreturn 
        foreach(var obj in _poolObjectList)
        {
            if(obj.activeSelf == false)
            {
                obj.SetActive(false);
                return obj;
            }
        }

        // すべて使用中なら、新しく生成する
        var newobj = CreateNewObject();
        newobj.SetActive(true);
        _poolObjectList.Add(newobj);
        return newobj;
    }

    // オブジェクトを生成する
    private GameObject CreateNewObject()
    {
        // インスタンスの生成
        var newObj = Instantiate(_poolObject);
        // オブジェクト名を設定 (name_数)
        newObj.name = _poolObject.name + "_" + (_poolObjectList.Count + 1);
        return newObj;
    }
}
