using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// zarządza pozycją kamery
/// </summary>
public class CameraFollow : MonoBehaviour {
    public Transform player, currentPlace;
    public Vector3 distance;
    CharacterMotor motor;
    public float lookUP = 6, speed = 1f;
    float  pSmo = 4, rSmo = 8;
    public bool x, y, z;
    public bool ThirdPerson = true;
    bool frez=false;
    // Use this for initialization
    void Awake() {
        motor = player.GetComponent<CharacterMotor>();
        if (currentPlace == null) {
            currentPlace = transform;
        }
        StartCoroutine(Move(player, distance));

    }
    
    /// <summary>
    /// porusza kamerą gdy nie jest statyczna
    /// </summary>
    void Update() {
        if (frez) {
            return;
        }

        if (ThirdPerson) {
            Vector3 targ = distance.z * player.transform.forward + player.position + new Vector3(0, distance.y, 0);
            transform.position = Vector3.Lerp(transform.position, targ, pSmo * Time.deltaTime);
            Quaternion tar = Quaternion.LookRotation(new Vector3(0, lookUP, 0) + player.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, tar, rSmo * Time.deltaTime);

            motor.thirdP = true;
        } else {
            if (x == true && y == true && z == true) {
                return;
            }
            motor.thirdP = false;
            //transform.position = Vector3.Lerp(transform.position, player.position + distance, pSmo * Time.deltaTime);
            transform.localPosition = player.position + distance;
            Vector3 loc = transform.localPosition;
            //Vector3 loc = Vector3.Lerp(transform.position, player.position + distance, pSmo * Time.deltaTime/10f);
            if (x) {
                loc.x = currentPlace.position.x;
            }
            if (y) {
                loc.y = currentPlace.position.y;
            }
            if (z) {
                loc.z = currentPlace.position.z;
            }
            transform.localPosition = loc;
        }
    }
    /// <summary>
    /// zmienia ustawienie kamery
    /// </summary>
    /// <param name="pos">pozycja</param>
    /// <param name="dis">odległość</param>
    /// <param name="a">zamróź x</param>
    /// <param name="b">zamróź y</param>
    /// <param name="c">zamróź z</param>
    public void MoveCamera(Transform pos, Vector3 dis, bool a = false, bool b = false, bool c = false) {
        currentPlace = pos;
        ThirdPerson = false;
        distance = dis;
        x = a;
        y = b;
        z = c;
        StopAllCoroutines();
        frez = true;
        if (x == true && y == true && z == true) {
            StartCoroutine(Move(pos, Vector3.zero));
        } else {
            StartCoroutine(Move(player, dis));
        }
    }
    /// <summary>
    /// zmień w tryb trzecio-osobowy
    /// </summary>
    /// <param name="dis">odległość</param>
    public void MoveCamera(Vector3 dis) {
        ThirdPerson = true;
        distance = dis;
    }
    /// <summary>
    /// przejazd między pozycjami
    /// </summary>
    /// <param name="pos">pozycja</param>
    /// <param name="dif">odległość</param>
    IEnumerator Move(Transform pos, Vector3 dif) {
        float tmp = 0;

        while (tmp < 1f) {
            Vector3 loc = pos.localPosition + dif;
            if (x) {
                loc.x = currentPlace.position.x;
            }
            if (y) {
                loc.y = currentPlace.position.y;
            }
            if (z) {
                loc.z = currentPlace.position.z;
            }
            tmp += Time.deltaTime * speed;
            transform.localPosition = Vector3.Lerp(transform.localPosition, loc, tmp);
            transform.rotation = Quaternion.Lerp(transform.rotation, currentPlace.rotation, tmp);
            yield return null;
        }
        motor.LevelDir();
        frez = false;
    }
}
