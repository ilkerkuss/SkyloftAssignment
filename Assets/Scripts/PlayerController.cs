using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public VariableJoystick variableJoystick;
    public Rigidbody rb;

    public float turnSpeed = 5;

    public GameObject BoxInventory;

    public List<GameObject> PickedBoxesList;


    public void FixedUpdate()
    {
        if (Input.GetButton("Fire1"))
        {
            MovePlayer();
        }
    }


    private void MovePlayer()
    {
        Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        rb.velocity = direction * speed * Time.fixedDeltaTime;


        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.fixedDeltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Collectable"))
        {
            other.gameObject.transform.parent = BoxInventory.transform;
            other.gameObject.transform.localPosition = Vector3.up* (other.transform.localScale.y) * (BoxInventory.transform.childCount-1);
            other.transform.localRotation = Quaternion.Euler(Vector3.zero);
            PickedBoxesList.Add(other.gameObject);
            
        }
    }
}