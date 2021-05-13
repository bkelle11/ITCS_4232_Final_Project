using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public static BossController instance;
    public BossAction[] actions;
    private int curAction;
    private float actionCounter;
    private float shotCounter;
    public Rigidbody2D theRB;
    private Vector2 moveDirection;
    public Animator anim;
    public float lastXVal;
    public int curHealth;
    public GameObject DeathEffect;
    public GameObject endPortal;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        actionCounter = actions[curAction].actionLentgh;
    }

    // Update is called once per frame
    void Update()
    {
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

        if (actionCounter > 0)
        {
            actionCounter -= Time.deltaTime;
            moveDirection = Vector2.zero;
            if(actions[curAction].shouldMove)
            {
                if(actions[curAction].shouldChasePlayer)
                {
                    moveDirection = PlayerController.instance.transform.position - transform.position;
                    moveDirection.Normalize();
                    anim.SetBool("isMoving", true);
                }

                if(actions[curAction].moveToPoint)
                {
                    moveDirection = actions[curAction].pointToGoTo.position - transform.position;
                }
            }
            theRB.velocity = moveDirection * actions[curAction].moveSpeed;

            if(actions[curAction].shouldShoot)
            {
                shotCounter -= Time.deltaTime;
                if(shotCounter < 0)
                {
                    shotCounter = actions[curAction].timeBetweenShots;

                    foreach(Transform t in actions[curAction].shootPoints)
                    {
                        Instantiate(actions[curAction].itemToShoot, t.position, t.rotation);
                    }
                }
            }
        }
        else
        {
            curAction++;
            if(curAction >= actions.Length)
            {
                curAction = 0;
            }
            anim.SetBool("isMoving", false);

            actionCounter = actions[curAction].actionLentgh;
        }
    }

    public void DamageBoss(int damage)
    {
        curHealth -= damage;
        if(curHealth <= 0)
        {
            gameObject.SetActive(false);
            Instantiate(DeathEffect, transform.position, transform.rotation);
            Instantiate(endPortal, transform.position, transform.rotation);
        }
    }
}

[System.Serializable]
public class BossAction
{
    [Header("Action")]
    public float actionLentgh;
    public bool shouldMove;
    public bool shouldChasePlayer;
    public float moveSpeed;
    public bool moveToPoint;
    public Transform pointToGoTo;
    public bool shouldShoot;
    public GameObject itemToShoot;
    public float timeBetweenShots;
    public Transform[] shootPoints;

}
