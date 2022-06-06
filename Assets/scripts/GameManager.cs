using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    [HideInInspector] public bool isInputEnable = true;
    [HideInInspector] public bool isGodMod;
    [HideInInspector] public int pimsAmount;
    [HideInInspector] public int moneyAmount;
    [HideInInspector] public Camera activeCamera;

    public TextMeshProUGUI pimsCounter;
    public TextMeshProUGUI coinCounter;
    public GameObject selectionWeel;
    [HideInInspector] public PlayerAttack playerAttack;
    public GameObject pauseCanva;
    public GameObject deafeatCanva;
    public GameObject tpBoss;
    [SerializeField] private GameObject miniMap;
    [SerializeField] private GameObject godMod;
    [HideInInspector] public bool canMove;

    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject humanBoss;
    [SerializeField] GameObject golemBoss;

    private bool isPause = false;


    private void Awake()
    {
        Time.timeScale = 1;

        mainCamera = Camera.main;

        if (instance == null)
            instance = this;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab) && GameManager.instance.isInputEnable)
        {
            selectionWeel.SetActive(true);
            Time.timeScale = 0.1f;
            SwitchCharacter.instance.activeCharacter.GetComponent<PlayerAttack>().canAttack = false;
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            selectionWeel.SetActive(false);
            Time.timeScale = 1f;
            SwitchCharacter.instance.activeCharacter.GetComponent<PlayerAttack>().canAttack = true;
        }

        if (isPause == false && Input.GetKeyDown(KeyCode.Escape))
        {
            Pause(true);
        }
        else if (isPause == true && Input.GetKeyDown(KeyCode.Escape))
        {
            Pause(false);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            moneyAmount += 10;
            pimsAmount += 10;
        }
        
        if (Input.GetKeyDown(KeyCode.G))
        {
            isGodMod = !isGodMod;
            godMod.SetActive(!godMod.activeSelf);
        }

        if(pimsCounter != null && coinCounter != null)
        {
            pimsCounter.text = pimsAmount.ToString();
            coinCounter.text = moneyAmount.ToString();
        }


        if (Input.GetKeyDown(KeyCode.M))
            SwitchCharacter.instance.activeCharacter.transform.position = tpBoss.transform.position;

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
            if(isPause)
            {
                GameManager.instance.isInputEnable = false;
                Time.timeScale = 0;
                pauseCanva.SetActive(true);
                isPause = true;

            }
            else
            {
                GameManager.instance.isInputEnable = true;
                Time.timeScale = 1;
                pauseCanva.SetActive(false);
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
            if (golemBoss != null)
                golemBoss.GetComponent<BossHealth>().ResetFight();
            if (humanBoss != null)
                humanBoss.GetComponent<BossHumanHealth>().ResetFight();
            activeCamera = mainCamera;
        }

    }

    public void Victory()
    {
        SceneManager.LoadScene("Win");
    }

    public void SetMiniMap(bool isActive)
    {
        miniMap.SetActive(isActive);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Victory();
        }
    }

}
