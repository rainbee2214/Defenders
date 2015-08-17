using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    public GameObject player;

    Sprite[] ships;
    SpriteRenderer sr;
    Rigidbody2D rb;
    public float speed = 0.1f;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ships = Resources.LoadAll<Sprite>("Sprites/Ships/Enemies");
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = ships[Random.Range(0, ships.Length)];
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        Vector3 v = player.transform.position - transform.position;
        Debug.Log(v);
        Debug.Log("Angle to player: " + Mathf.Atan(v.x / v.y) * 180 / Mathf.PI);

        if (player.transform.position.y - transform.position.y < 0)
        {

            Rotate(Mathf.Atan(v.x / v.y) * 180 / Mathf.PI - 270);
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
}
