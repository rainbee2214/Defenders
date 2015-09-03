using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(ShipLoader))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour, UIHealth
{
    int health;
    int maxHealth = 100;

    public Slider healthBar;
    PlayerMovement movement;
    ShipLoader shipLoader;

    PlayerWeapon weapon;

    Animator anim;
    SpriteRenderer srenderer;

    Camera mainCamera;
    Vector3 cameraPosition;

    public bool dying;

    Vector3 spawnLocation = new Vector3(6f, 0f, 0f);
    void Awake()
    {
        health = maxHealth;
        weapon = GetComponent<PlayerWeapon>();
        mainCamera = Camera.main;
        srenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();

    }

    public bool explode;

    float move, rotate;

    public Vector2 bulletDirection;

    public float sin, cos;

    float shootDelay = .1f;
    float nextShootTime;

    void Start()
    {
    }

    void Update()
    {
        move = Input.GetAxisRaw("Vertical");
        rotate = Input.GetAxisRaw("Horizontal");
        sin = Mathf.Sin(transform.localEulerAngles.z * 180 / Mathf.PI);
        cos = Mathf.Cos(transform.localEulerAngles.z * 180 / Mathf.PI);

        bulletDirection.Set(sin, cos);
    }

    public bool spinAttack;

    void FixedUpdate()
    {
        if (spinAttack)
        {
            spinAttack = false;
            weapon.SpinAttack(transform.eulerAngles.z);
        }
        if (!dying)
        {
            if (Input.GetButtonDown("SpinAttack"))
            {
                Debug.Log("Spin Attack");
                weapon.SpinAttack(transform.eulerAngles.z);
            }
            if (Input.GetButtonDown("Shoot") && Time.time > nextShootTime)
            {
                Debug.Log("Shooting");
                weapon.Shoot(transform.eulerAngles.z);
                nextShootTime = Time.time + shootDelay;
            }
            if (Input.GetAxisRaw("Vertical") != 0) movement.Move(move);
            if (Input.GetAxisRaw("Horizontal") != 0) movement.Rotate(rotate);

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
        ResetHealth();
        healthBar.enabled = true;
    }

    public void TakeDamage(float damage = -1)
    {
        healthBar.value = health;
        if (health <= 0)
        {
            health = 0;
            healthBar.enabled = false;
            Die();
        }
    }

    public void ResetHealth()
    {
        health = maxHealth;
        healthBar.value = health;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().Explode();
            health -= other.gameObject.GetComponent<Enemy>().damageAmount;
            TakeDamage();
        }

    }
}
