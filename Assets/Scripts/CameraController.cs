using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    public Vector3 Offset;

    public Vector3 newPos;

    private void Start()
    {
        Offset = transform.position - Player.transform.position;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,Offset+Player.transform.position,2);
    }
}
