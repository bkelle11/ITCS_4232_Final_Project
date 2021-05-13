using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public float loadWait = 4;
    public string nextLevel;
    public bool isPaused;
    public int coinsAmount;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        coinsAmount = CharacterTracker.instance.curCoins;

        Time.timeScale = 1f;
        UIController.instance.coinText.text = "x" + coinsAmount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public IEnumerator LevelEnd()
    {
        PlayerController.instance.canMove = false;
        UIController.instance.StartToBlack();
        yield return new WaitForSeconds(loadWait);
        CharacterTracker.instance.curCoins = coinsAmount;
        CharacterTracker.instance.curHealth = PlayerHealthController.instance.currentHealth;
        SceneManager.LoadScene(nextLevel);
    }
    public void PauseUnpause()
    {
        if(!isPaused)
        {
            UIController.instance.pauseMenu.SetActive(true);
            isPaused = true;
            Time.timeScale = 0f;
        }
        else
        {
            UIController.instance.pauseMenu.SetActive(false);
            isPaused = false;
            Time.timeScale = 1f;
        }
    }

    public void GetCoins(int amount)
    {
        coinsAmount += amount;
        UIController.instance.coinText.text = "x" + coinsAmount.ToString();
    }
}
