using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraPosition : MonoBehaviour {
    public CharacterMotor Player;
    public float speed = 1f;
    private void Start() {
    }
    public void MoveCamera(Transform pos) {
        StopCoroutine("Move");
        StartCoroutine("Move", pos);
    }
    IEnumerator Move(Transform pos) {
        float tmp=0;
        while (Vector3.Distance(transform.position, pos.position) > 0f || Vector3.Angle(transform.forward, pos.forward) > 0f) {
            tmp += Time.deltaTime*speed;
            transform.position = Vector3.Lerp(transform.position, pos.position, tmp);
            transform.rotation = Quaternion.Lerp(transform.rotation, pos.rotation, tmp);
            yield return null;
        }
        Player.LevelDir();
    }
    
}
