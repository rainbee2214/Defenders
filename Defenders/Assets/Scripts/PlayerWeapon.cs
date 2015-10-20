using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerWeapon : MonoBehaviour
{
    GameObject bullet;

    GameObject b;

    public int spinAttackDelay = 25;

    public List<GameObject> bullets;
    int topBullet;
    int numberOfBullets = 10;

    public Vector3 weapon1Position;

    void Awake()
    {
        bullet = Resources.Load<GameObject>("Prefabs/Bullet");
        GameObject weapon1 = new GameObject("PlayerWeapon1");
        weapon1.transform.position = transform.position;
        weapon1.transform.eulerAngles = Vector3.zero;

        bullets = new List<GameObject>();
        for (int i = 0; i < numberOfBullets; i++)
        {
            bullets.Add(Instantiate(bullet));
            bullets[i].name = "Bullet" + i;
            bullets[i].SetActive(false);
            bullets[i].transform.SetParent(weapon1.transform);
        }
    }


    public void Shoot(float theta)
    {
        b = bullets[topBullet];
        b.transform.position = weapon1Position + transform.position;
        b.gameObject.SetActive(true);
        b.GetComponent<Bullet>().Move(theta);
        topBullet++;
        if (topBullet >= bullets.Count) topBullet = 0;
    }

    public void DoubleShoot(float theta)
    {
        b = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
        b.name = "Bullet";
        b.GetComponent<Bullet>().Move(theta);
    }
    public void SpinAttack(float theta)
    {
        StartCoroutine(StartSpinAttack(theta));
    }

    //theta is in degrees
    IEnumerator StartSpinAttack(float theta)
    {
        for (int i = 0; i < 360; i += spinAttackDelay)
        {
            Debug.Log("Shooting");
            Shoot(theta + i);
            yield return null;
        }
    }
}
