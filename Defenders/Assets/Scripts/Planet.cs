using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Planet : MonoBehaviour, UIHealth
{
    float radius;

    public int health = 100;

    public Slider healthBar;
    public bool explode;

    void Awake()
    {
        //Get the planet radius from the collider
        radius = GetComponent<CircleCollider2D>().radius;
        SetupPlanet();
    }



    public void SetupPlanet()
    {

    }

    void Update()
    {
        if (explode)
        {
            explode = false;
            GetComponent<SpriteRenderer>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(true);
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver()
    {

        transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        //TODO: add message controller
        Application.LoadLevel("Level");
        yield return null;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collision");
        if (other.gameObject.tag == "Player")
        {
            if (!other.gameObject.GetComponent<Player>().dying) other.gameObject.GetComponent<Player>().Die();
        }
        else if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().Explode();
            health -= other.gameObject.GetComponent<Enemy>().damageAmount;
            TakeDamage();
        }

    }
    public void TakeDamage(float damage = -1)
    {
        healthBar.value = health;
        if (health <= 0)
        {
            health = 0;
            Debug.Log("Explode!");
            //explode 
            explode = true;
        }
    }

    public void ResetHealth()
    {
        health = 100;
    }

}
