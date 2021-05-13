using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public GameObject DestoryEffect;
    public bool canDrop;
    public GameObject[] itemDrops;
    public float dropPercentChance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "Player Bullet")
        {
            if (PlayerController.instance.dashCounter > 0 || other.tag == "Player Bullet")
            {
                AudioManager.instance.PlaySFX(1);

                Instantiate(DestoryEffect, transform.position, transform.rotation);
                Destroy(gameObject);

                if(canDrop)
                {
                    float dropChance = Random.Range(0f, 100f);
                    if(dropChance < dropPercentChance)
                    {
                        int randItem = Random.Range(0, itemDrops.Length);
                        Instantiate(itemDrops[randItem], transform.position, transform.rotation);
                    }
                }
            }
        }
    }
}
