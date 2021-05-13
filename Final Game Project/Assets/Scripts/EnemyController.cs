using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D theRB;
    public float moveSpeed;
    public float rangeToChasePlayer;
    private Vector3 moveDirection;
    public Animator anim;
    public float lastXVal;
    public int health = 150;
    public GameObject DeathEffect;
    public bool shouldShot;
    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    private float fireCounter;
    public SpriteRenderer theBody;
    public float shotRange;

    public bool canDrop;
    public GameObject[] itemDrops;
    public float dropPercentChance;

    // Start is called before the first frame update
    void Start()
    {
        lastXVal = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(theBody.isVisible && PlayerController.instance.gameObject.activeInHierarchy)
        {
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer)
            {
                moveDirection = PlayerController.instance.transform.position - transform.position;
            }
            else
            {
                moveDirection = new Vector3(0f, 0f, 0f);
            }

            moveDirection.Normalize();

            theRB.velocity = moveDirection * moveSpeed;

            if (transform.position.x > lastXVal)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
                lastXVal = transform.position.x;
            }
            else if (transform.position.x < lastXVal)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                lastXVal = transform.position.x;
            }

            if (shouldShot && Vector3.Distance(transform.position, PlayerController.instance.transform.position) < shotRange)
            {
                fireCounter -= Time.deltaTime;
                if (fireCounter <= 0)
                {
                    AudioManager.instance.PlaySFX(3);

                    fireCounter = fireRate;
                    Instantiate(bullet, firePoint.transform.position, transform.rotation);
                }
            }
        }
        else
        {
            theRB.velocity = Vector2.zero;
        }

        if (moveDirection != Vector3.zero)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    public void DamageEnemy(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            AudioManager.instance.PlaySFX(2);

            Instantiate(DeathEffect, transform.position, transform.rotation);
            Destroy(gameObject);

            if (canDrop)
            {
                float dropChance = Random.Range(0f, 100f);
                if (dropChance < dropPercentChance)
                {
                    int randItem = Random.Range(0, itemDrops.Length);
                    Instantiate(itemDrops[randItem], transform.position, transform.rotation);
                }
            }
        }
    }
}
