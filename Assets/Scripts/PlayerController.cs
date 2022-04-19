using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerController : MonoBehaviour
{
    public static Action<int> DecreaseZoneAmount;
    public static Action AlignBoxesToStorage;
    public static Action<int> UpdateBoxText;


    public float CharacterSpeed;
    public VariableJoystick variableJoystick;
    public Rigidbody CharacterRB;

    public float CharacterRotationSpeed = 5;

    public GameObject BoxInventory;

    public List<GameObject> PickedBoxesList;


    public void FixedUpdate()
    {
        if (Input.GetButton("Fire1"))
        {
            MovePlayer();
        }
    }

    private void Update()
    {
        PickedBoxesList.RemoveAll(item => item == null);  // Remove null values from PickBoxesList after remove operation

    }


    private void MovePlayer() //Player Movement
    {
        Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        CharacterRB.velocity = direction * CharacterSpeed * Time.fixedDeltaTime;


        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), CharacterRotationSpeed * Time.fixedDeltaTime);
    }



    private void PlayerPickCollectableBoxes(GameObject Box)
    {
        if (Box.GetComponentInParent<CubeZoneController>())
        {
            Box.GetComponentInParent<CubeZoneController>().CollectableBoxes.Remove(Box);
        }
        else if (Box.GetComponentInParent<StorageController>())
        {
            Box.GetComponentInParent<StorageController>().StorageList.Remove(Box);
        }
       

        Box.transform.parent = BoxInventory.transform;
        Box.transform.DOLocalJump((Vector3.up * (Box.transform.localScale.y) * (BoxInventory.transform.childCount - 1)), 2, 1, 1f);
        Box.transform.localRotation = Quaternion.Euler(Vector3.zero);
        
        PickedBoxesList.Add(Box);

        UpdateBoxText(PickedBoxesList.Count);


    }

 
    


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            PlayerPickCollectableBoxes(other.gameObject);
            

        }
    }




    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ZoneOpener") && PickedBoxesList.Count > 0 && other.GetComponentInParent<ZoneController>().DependencyZone.IsZoneActive && other.GetComponentInParent<ZoneController>().ZoneOpenAmount!=0)
        {
            DecreaseZoneAmount((int)(other.GetComponentInParent<ZoneController>().ZoneSO.ID));

            var lastBox = PickedBoxesList[PickedBoxesList.Count - 1];
            lastBox.transform.DOJump(other.transform.position, 2, 1, 1);
            lastBox.tag = "Untagged";
            PickedBoxesList.Remove(lastBox);
            lastBox.transform.parent = null;
            UpdateBoxText(PickedBoxesList.Count);



            Destroy(lastBox, 1.1f);
        }
    }

/*
    public GameObject GetLastBox()
    {
        if (PickedBoxesList.Count == 0)
        {
            return null;
        }

        var item = PickedBoxesList[PickedBoxesList.Count - 1];
        PickedBoxesList.Remove(item);

        return item;
    }
*/
}
