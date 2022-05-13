using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static bool isInputEnable = true;
    public GameObject selectionWeel;
    public PlayerAttack playerAttack;
    public SwitchCharacter switchCharacter;
    public GameObject pauseCanva;

    private bool isPause = false;


    private void Awake()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            selectionWeel.SetActive(true);
            switchCharacter.activeCharacter.GetComponent<PlayerAttack>().canAttack = false;
            Time.timeScale = 0.1f;
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            selectionWeel.SetActive(false);
            switchCharacter.activeCharacter.GetComponent<PlayerAttack>().canAttack = true;
            Time.timeScale = 1f;
        }

        if (isPause == false && Input.GetKeyDown(KeyCode.Escape))
        {
            Pause(true);
        }
        else if (isPause == true && Input.GetKeyDown(KeyCode.Escape))
        {
            Pause(false);
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
                GameManager.isInputEnable = false;
                isPause = true;

            }
            else
            {
                Time.timeScale = 1;
                pauseCanva.SetActive(false);
                GameManager.isInputEnable = true;
                isPause = false;
        }
        }




}
