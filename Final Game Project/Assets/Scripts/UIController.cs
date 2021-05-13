using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;    
    public Slider healthSlider;
    public Text healthText;
    public Text coinText;
    public GameObject deathScreen;
    public Image levelTransition;
    public float transitionTime;
    private bool transitionToBlack;
    private bool transitionFromBlack;
    public string newGameScene;
    public string mainMenu;
    public GameObject pauseMenu;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        transitionFromBlack = true;
        transitionToBlack = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(transitionFromBlack)
        {
            levelTransition.color = new Color(levelTransition.color.r,
                                              levelTransition.color.g, 
                                              levelTransition.color.b, 
                                              Mathf.MoveTowards(levelTransition.color.a, 0f, transitionTime * Time.deltaTime));
            if(levelTransition.color.a == 0)
            {
                transitionFromBlack = false;
            }
        }

        if(transitionToBlack)
        {
            levelTransition.color = new Color(levelTransition.color.r,
                                              levelTransition.color.g,
                                              levelTransition.color.b,
                                              Mathf.MoveTowards(levelTransition.color.a, 1f, transitionTime * Time.deltaTime));
            if (levelTransition.color.a == 0)
            {
                transitionToBlack = false;
            }
        }
    }

    public void StartToBlack()
    {
        transitionToBlack = true;
        transitionFromBlack = false;
    }

    public void NewGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(newGameScene);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenu);
    }

    public void Resume()
    {
        LevelManager.instance.PauseUnpause();
    }
}
