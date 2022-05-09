using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject playerCamera;
    public float timeOffset;
    public Vector3 posOffset;

    private Vector3 velocity;


    public void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, playerCamera.transform.position + posOffset, ref velocity, timeOffset);
    }
}
