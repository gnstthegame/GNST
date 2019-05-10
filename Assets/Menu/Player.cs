using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public int level = 3;
    public int health = 40;

    public void SavePaler()
    {
        saveSystem.SavePlayer(this);

    }

    public void LoadPlayer()
    {
        savedata data = saveSystem.Loaddata(this);
        level = data.level;
        health = data.health;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }
}
