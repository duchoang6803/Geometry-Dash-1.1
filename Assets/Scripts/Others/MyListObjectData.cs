using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyListObjectData : MonoBehaviour
{
    public D_ObjectListData objectListDatas;
    public List<GameObject> objectsInScene;
    private void Awake()
    {
        //objectListDatas.runtimeObjects.Clear();
        //objectListDatas.runtimeObjects.AddRange(objectsInScene);
    }
    private void Update()
    {
        
    }
}
