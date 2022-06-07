using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableIndicator : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Animator>().enabled = true;
    }
}
