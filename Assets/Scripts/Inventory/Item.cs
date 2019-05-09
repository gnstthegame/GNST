using System.Collections;
using System.Collections.Generic;
<<<<<<< HEAD
using UnityEditor;
using UnityEngine;
=======
using UnityEngine;
using UnityEditor;
>>>>>>> 923e860fc7ce348d585a8508193aea820623ec1d

[CreateAssetMenu]
public class Item : ScriptableObject
{
    [SerializeField] string id;
    public string ID { get { return id; } }
<<<<<<< HEAD
    public string Name;
    public Sprite Sprite;
    public int BuyValue;
    public int SellValue;
    [Range(1,999)]
    public int MaximumStacks = 1;

=======
    public string ItemName;
    public Sprite Icon;
    public int BuyValue;
    public int SellValue;

    //Aby rozrozniac takie same przedmioty w eq (nie trzeba samemu numerowac)
>>>>>>> 923e860fc7ce348d585a8508193aea820623ec1d
    private void OnValidate()
    {
        string path = AssetDatabase.GetAssetPath(this);
        id = AssetDatabase.AssetPathToGUID(path);
    }
<<<<<<< HEAD

    public virtual Item GetCopy()
    {
        return this;
    }

    public virtual void Destroy()
    {

    }
=======
>>>>>>> 923e860fc7ce348d585a8508193aea820623ec1d
}
