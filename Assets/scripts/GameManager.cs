using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isInputEnable = true;
    public int pimsAmount;
    public int moneyAmount;

    public GameObject selectionWeel;
    public PlayerAttack playerAttack;
    public SwitchCharacter switchCharacter;
    public GameObject pauseCanva;
    public GameObject deafeatCanva;

    private bool isPause = false;


    private void Awake()
    {
        Time.timeScale = 1;

        if (instance == null)
            instance = this;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab) && isInputEnable)
        {
            selectionWeel.SetActive(true);
            Time.timeScale = 0.1f;
            switchCharacter.activeCharacter.GetComponent<PlayerAttack>().canAttack = false;
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            selectionWeel.SetActive(false);
            Time.timeScale = 1f;
            switchCharacter.activeCharacter.GetComponent<PlayerAttack>().canAttack = true;
        }

        if (isPause == false && Input.GetKeyDown(KeyCode.Escape))
        {
            Pause(true);
        }
        else if (isPause == true && Input.GetKeyDown(KeyCode.Escape))
        {
            Pause(false);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            switchCharacter.activeCharacter.transform.position = new Vector3(48.78f, 22.374f, 1.158906f);
        }



    }

        public void LoadGame()
        {
            SceneManager.LoadScene("Game");
        }

        public void LoadHome()
        {
            SceneManager.LoadScene("HomeMenu");
        }

    public void QuitGame()
        {
            Application.Quit();
        }

        public void Pause(bool isPause)
        {
            if(isPause == true)
            {
                Time.timeScale = 0;
                pauseCanva.SetActive(true);
                isInputEnable = false;
                isPause = true;

            }
            else
            {
                Time.timeScale = 1;
                pauseCanva.SetActive(false);
                isInputEnable = true;
                isPause = false;
            }
        }


    public void setDefeat(bool isDead)
    {
        if(isDead == true)
        {
            Time.timeScale = 0;
            deafeatCanva.SetActive(true);
        }

        if (isDead == false)
        {
            Time.timeScale = 1;
            deafeatCanva.SetActive(false);
        }

    }

    public void Victory()
    {
        SceneManager.LoadScene("Win");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Victory();
        }
    }

}
