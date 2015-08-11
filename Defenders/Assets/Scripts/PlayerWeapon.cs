using UnityEngine;
using System.Collections;

public class PlayerWeapon : MonoBehaviour
{
    public GameObject bullet;

    GameObject b;

    public int spinAttackDelay = 25;

    public void Shoot (float theta)
    {
        b = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
        b.name = "Bullet";
        b.GetComponent<Bullet>().Move(theta);
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
            Shoot(theta+i);
            yield return null;
        }
    }
}
