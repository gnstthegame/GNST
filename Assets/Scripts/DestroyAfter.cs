using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour {
    public float Life;
    private void Update() {
        Life -= Time.deltaTime;
        if (Life <= 0) {
            Destroy(gameObject);
        }
    }

}
