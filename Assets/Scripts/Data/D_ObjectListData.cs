using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "listObjectData", menuName = "Data/ObjectListData/ObjectData")]
public class D_ObjectListData : ScriptableObject
{
    [System.NonSerialized]public List<GameObject> runtimeObjects = new List<GameObject>();
}
