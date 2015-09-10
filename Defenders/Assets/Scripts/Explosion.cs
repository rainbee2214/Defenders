using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public GameObject explosion;

    public void Die()
    {
        StartCoroutine(Death());
    }

    public void Explode()
    {
        StartCoroutine(SetExplosion());
    }

    public void Respawn()
    {
        ISpawnable spawn = GetComponent<ISpawnable>();
        if (spawn != null)
        {
            StartCoroutine(SetRespawn());
        }
    }

    IEnumerator Death()
    {
        //get an explosion from the pool and turn it on
        // turn off the game object
        // if respawnable
        yield return null;
    }
    IEnumerator SetExplosion()
    {
        yield return null;
    }

    IEnumerator SetRespawn()
    {
        yield return null;
    }

}
