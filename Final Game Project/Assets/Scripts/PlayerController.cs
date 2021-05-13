using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public float moveSpeed;
    private Vector2 moveInput;
    public Rigidbody2D theRB;
    public Transform weaponArm;
    private Camera theCam;
    public Animator anim;
    public GameObject bulletToFire;
    public Transform firePoint;
    public float timeBetweenShots;
    private float shotCounter;
    public SpriteRenderer bodySR;
    private float activeMoveSpeed;
    public float dashSpeed = 9f;
    public float dashLength = 0.5f;
    public float dashCoolDown = 1f;
    public float dashInivnic = .5f;
    [HideInInspector]
    public float dashCounter;
    private float dashCoolCounter;
    [HideInInspector]
    public bool canMove = true;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        theCam = Camera.main;
        activeMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove && !LevelManager.instance.isPaused)
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");
            moveInput.Normalize();

            theRB.velocity = moveInput * activeMoveSpeed;

            // Finds mouse location
            Vector3 mousePos = Input.mousePosition;
            Vector3 screenPoint = theCam.WorldToScreenPoint(transform.localPosition);

            // Moves weapon to point at mouse
            Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            weaponArm.rotation = Quaternion.Euler(0, 0, angle - 90);

            if (mousePos.x < screenPoint.x)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                weaponArm.localScale = new Vector3(-1f, -1f, 1f);
                weaponArm.rotation = Quaternion.Euler(0, 0, angle + 90);
            }
            else
            {
                transform.localScale = Vector3.one;
                weaponArm.localScale = Vector3.one;
            }

            if (moveInput != Vector2.zero)
            {
                anim.SetBool("isMoving", true);
            }
            else
            {
                anim.SetBool("isMoving", false);
            }

            if (Input.GetMouseButtonDown(0))
            {
                AudioManager.instance.PlaySFX(5);

                Instantiate(bulletToFire, firePoint.position, firePoint.rotation);

                shotCounter = timeBetweenShots;
            }

            if (Input.GetMouseButton(0))
            {
                shotCounter -= Time.deltaTime;

                if (shotCounter <= 0)
                {
                    AudioManager.instance.PlaySFX(5);

                    Instantiate(bulletToFire, firePoint.position, firePoint.rotation);

                    shotCounter = timeBetweenShots;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (dashCoolCounter <= 0 && dashCounter <= 0)
                {
                    activeMoveSpeed = dashSpeed;
                    dashCounter = dashLength;
                    anim.SetTrigger("dash");
                    PlayerHealthController.instance.MakeInvincible(dashInivnic);
                }
            }

            if (dashCounter > 0)
            {
                dashCounter -= Time.deltaTime;
                if (dashCounter <= 0)
                {
                    activeMoveSpeed = moveSpeed;
                    dashCoolCounter = dashCoolDown;
                }
            }

            if (dashCoolCounter > 0)
            {
                dashCoolCounter -= Time.deltaTime;
            }
        }
        else
        {
            theRB.velocity = Vector2.zero;
            anim.SetBool("isMoving", false);
        }
    }
}
