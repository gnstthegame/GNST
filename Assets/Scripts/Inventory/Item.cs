using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public int Id;
    public string Name;
    public Sprite Sprite;

    public virtual Skill getskill() {
        return new Skill();
    }
}
