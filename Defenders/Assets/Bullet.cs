using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    GameObject explosion;

    Vector2 direction;
    float speed = 10;

    float startTime;
    float bulletLife = 2.5f;

    Rigidbody2D rb;

    float delay = 5f;
    Animator anim;

    bool canCollide = true;

    Sprite defaultSprite;
    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        defaultSprite = sr.sprite;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        explosion = GetComponentsInChildren<SpriteRenderer>()[1].gameObject;

    }

    public void Explode()
    {
        StopCoroutine("TurnOff");
        StartCoroutine(ExplodeRoutine());
    }

    public void Move(float theta)
    {
        explosion.gameObject.SetActive(false);
        rb.velocity = new Vector2(-Mathf.Sin(theta * Mathf.PI / 180), Mathf.Cos(theta * Mathf.PI / 180)) * speed;
        StartCoroutine("TurnOff");
    }

    IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }

    IEnumerator ExplodeRoutine()
    {
        rb.velocity = Vector3.zero;
        Debug.Log("explode");
        canCollide = false;
        // play explosion
        explosion.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        gameObject.SetActive(false);
        canCollide = true; 

        explosion.gameObject.SetActive(false);
        yield return null;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (canCollide)
        {
            if (other.tag != "Player" && other.tag != "Bullet")
            {
                Debug.Log("Trigger on bullet" + other.tag);
                Explode();
            }
        }
    }
}
