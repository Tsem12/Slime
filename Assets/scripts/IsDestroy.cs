using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsDestroy : MonoBehaviour
{
    [SerializeField] private Elevator[] elevator;

    private void OnDisable()
    {
        if(elevator != null)
            foreach(Elevator e in elevator)
            {
                e.objectsToDestroy -= 1;
            }
    }
}
