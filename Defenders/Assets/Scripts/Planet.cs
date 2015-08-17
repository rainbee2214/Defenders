using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour
{
    float radius;

    void Awake()
    {
        //Get the planet radius from the collider
        radius = GetComponent<CircleCollider2D>().radius;
        SetupPlanet();
    }



    public void SetupPlanet()
    {

    }


    public void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collision");
        if (other.gameObject.tag == "Player")
        {
            if (!other.gameObject.GetComponent<Player>().dying) other.gameObject.GetComponent<Player>().Die();
        }

    }

    public void OnTriggerExit2D(Collider2D other)
    {
 
    }

    void Update()
    {

    }

}
