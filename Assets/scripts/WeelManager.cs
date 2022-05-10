using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeelManager : MonoBehaviour
{
    [Header("Lock")]
    public GameObject flyLock;
    public GameObject golemLock;
    public GameObject humanLock;

    [Header("UnLock")]
    public GameObject flyUnLock;
    public GameObject golemUnLock;
    public GameObject humanUnLock;

    public void UnlockCharacter(string type)
    {

        switch (type)
        {
            case "fly":
                flyLock.SetActive(false);
                flyUnLock.SetActive(true);
                break;
            case "golem":
                golemLock.SetActive(false);
                golemUnLock.SetActive(true);
                break;
            case "human":
                humanLock.SetActive(false);
                humanUnLock.SetActive(true);
                break;
        }
    }



}
