using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

    Vector2 direction;
    float speed = 1;

    float startTime;
    float bulletLife = 5f;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(float theta)
    {
        rb.velocity = new Vector2(-Mathf.Sin(theta * Mathf.PI / 180), Mathf.Cos(theta * Mathf.PI / 180)) * speed;
        //rb.AddForce(new Vector2(2,3));
        StartCoroutine(TurnOff(5f));
    }

    IEnumerator TurnOff(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
        Debug.Log("DoneMoving");
    }
}
