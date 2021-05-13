using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    public static VictoryScreen instance;
    public float waitForKey = 2;
    public GameObject anyKeyText;
    public string mainMenu;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (waitForKey > 0)
        {
            waitForKey -= Time.deltaTime;
            if (waitForKey <= 0)
            {
                anyKeyText.SetActive(true);
            }
        }
        else
        {
            if(Input.anyKeyDown)
            {
                SceneManager.LoadScene(mainMenu);
            }
        }
    }
}
