using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(ShipLoader))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    PlayerMovement movement;
    ShipLoader shipLoader;

    Animator anim;
    SpriteRenderer srenderer;

    Camera mainCamera;
    Vector3 cameraPosition;

    public bool dying;

    Vector3 spawnLocation = new Vector3(6f, 0f, 0f);
    void Awake()
    {
        mainCamera = Camera.main;
        srenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();

    }

    public bool explode;

    void Start()
    {
    }

    void Update()
    {
        if (!dying)
        {
   
            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0) movement.Move(Input.GetAxisRaw("Vertical"));
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0) movement.Rotate(Input.GetAxisRaw("Horizontal"));
        
            if ((Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0) || (Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0))
                anim.SetBool("PlayerFlying", true);
            else anim.SetBool("PlayerFlying", false);

            cameraPosition = transform.position;
            cameraPosition.z = -10;
            mainCamera.transform.position = cameraPosition;
        }

    }

    public void Die()
    {
        StartCoroutine(Death());
    }

    public void ReSpawn()
    {
        transform.position = spawnLocation;
        //movement.transform.eulerAngles = Vector3.zero;
    }

    public void Explode()
    {
        anim.SetTrigger("Explode");
    }

    IEnumerator Death()
    {
        dying = true;
        Debug.Log("Dying");
        Explode();
        yield return new WaitForSeconds(1.5f);
        ReSpawn();
        Debug.Log("Respawning");
        anim.SetTrigger("Respawned");
        dying = false;
    }
}
