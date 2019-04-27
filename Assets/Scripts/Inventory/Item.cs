using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    [SerializeField] string id;
    public string ID { get { return id; } }
    public string Name;
    public Sprite Sprite;
    public int BuyValue;
    public int SellValue;

    private void OnValidate()
    {
        string path = AssetDatabase.GetAssetPath(this);
        id = AssetDatabase.AssetPathToGUID(path);
    }
}
