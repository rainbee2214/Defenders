using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    GameObject explosion;
    Sprite[] ships;
    SpriteRenderer sr;
    Rigidbody2D rb;
    public float speed = 0.1f;
    bool dead;

    Vector2 startPosition;

    public int damageAmount = 5;

    void Awake()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        ships = Resources.LoadAll<Sprite>("Sprites/Ships/Enemies");
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = ships[Random.Range(0, ships.Length)];
        explosion = GetComponentsInChildren<SpriteRenderer>()[1].gameObject;
        explosion.SetActive(false);

    }

    void Update()
    {
        if (dead) return;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        Vector3 v = player.transform.position - transform.position;

        if (player.transform.position.y - transform.position.y < 0)
        {

            Rotate(-Mathf.Atan(v.x / v.y) * 180 / Mathf.PI -180);
        }
        else
        {
            Rotate(-Mathf.Atan(v.x / v.y) * 180 / Mathf.PI);

        }
    }
    //Move(new Vector3(Mathf.Sin((-rb.rotation)* Mathf.PI / 180), Mathf.Cos((-rb.rotation) * Mathf.PI / 180),0f)*speed*Time.deltaTime);

    public void Rotate(float angle)
    {
        rb.MoveRotation(angle);
    }

    public void Move(Vector3 pos)
    {
        rb.MovePosition(transform.position + pos);
    }

    public void Explode()
    {
        explosion.SetActive(true);
        sr.enabled = false;
        dead = true;
        StartCoroutine(ResetEnemy());
    }

    IEnumerator ResetEnemy()
    {
        yield return new WaitForSeconds(2f);
        Reset();
        yield return null;
    }

    public void Reset()
    {
        transform.position = startPosition;
        rb.velocity = Vector3.zero;
        sr.enabled = true;
        dead = false;
        explosion.SetActive(false);
    }
}
