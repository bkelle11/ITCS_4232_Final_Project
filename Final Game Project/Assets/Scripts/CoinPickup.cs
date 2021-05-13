using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int coinWorth = 1;
    public float waitToCollect;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (waitToCollect > 0)
        {
            waitToCollect -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && waitToCollect <= 0)
        {
            LevelManager.instance.GetCoins(coinWorth);
            AudioManager.instance.PlaySFX(0);
            Destroy(gameObject);
        }
    }
}