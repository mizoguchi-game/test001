using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class NKPrefabMan : MonoBehaviour {

    private string prefabDir = "Prefab/";

    public void savePrefab(GameObject gameObj,string name)
    {
        //prefabの保存フォルダパス
        string prefabDirPath = Application.dataPath + "/Resources/" + prefabDir;
        if (!Directory.Exists(prefabDirPath))
        {
            //prefab保存用のフォルダがなければ作成する
            Directory.CreateDirectory(prefabDirPath);
        }

        //prefabの保存ファイルpath
        string prefabPath = prefabDirPath + name + ".prefab";
        if (!File.Exists(prefabPath))
        {
            //prefabファイルがなければ作成する
            File.Create(prefabPath);
        }

        //prefabの保存
        PrefabUtility.CreatePrefab("Assets/Resources/" + prefabDir + name + ".prefab",gameObj);
        AssetDatabase.SaveAssets();
    }

    public GameObject loadPrefab(string name)
    {
        string prefabPath = Application.dataPath + "/Resources/" + prefabDir + name + ".prefab";
        if (File.Exists(prefabPath))
        {
            //prefabファイルの読み込み
            string resourcePath = prefabDir + name;
            GameObject gameObj = (GameObject)Resources.Load<GameObject>(resourcePath);
            if (gameObj)
            {
                return gameObj;
            }
        }
        return null;
    }
}
