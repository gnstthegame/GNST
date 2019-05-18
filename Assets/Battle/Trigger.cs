using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// aktywuje pole walki
/// </summary>
public class Trigger : MonoBehaviour {
    public Transform place;
    public GameObject Prefab;
    public int mapX = 10;
    public int mapY = 10;
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            GameObject go = new GameObject();
            go.transform.position = place.position;
            go.transform.rotation = place.rotation;
            var tm = go.AddComponent<TileMap>();
            tm.mapSizeX = mapX;
            tm.mapSizeY = mapY;
            tm.VisualPrefab = Prefab;
            Destroy(gameObject);
        }
    }

}
