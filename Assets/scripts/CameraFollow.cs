using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float timeOffset;
    public Vector3 posOffset;

    private Vector3 velocity;

    public void LateUpdate()
    {
        if (SwitchCharacter.instance != null)
        {
            player = SwitchCharacter.instance.activeCharacter;
        }
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + posOffset, ref velocity, timeOffset);
    }
}
