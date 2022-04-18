using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HelperController : MonoBehaviour
{
    public NavMeshAgent HelperNavMeshAgent;

    public CubeZoneController HelpZone;
    public int PickAmount;
    public GameObject HelperBoxInventory;
    public GameObject StorageObject;

    public Vector3 Destination;
    public bool HaveDestination;


    // Start is called before the first frame update
    void Start()
    {
        HaveDestination = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (HelpZone.CollectableBoxes.Count > 0)
        {
            if (HelperBoxInventory.transform.childCount < PickAmount)
            {
                HaveDestination = true;

                Destination = HelpZone.CollectableBoxes[0].transform.position;
                MoveToZone(Destination);

                //Debug.Log(Destination + "+" + HelpZone.CollectableBoxes[0].transform.position);
            }

            else
            {
                HaveDestination = false;
                Destination = StorageObject.transform.position;
                MoveToZone(Destination);
            }
        }
        
     


        

    }
        
    




    private void MoveToZone(Vector3 DestinationPoint)
    {
        HelperNavMeshAgent.destination = DestinationPoint;
    }

    private void PickUpBoxes(GameObject CollectBox)
    {
        HelpZone.CollectableBoxes.RemoveAt(0);

        CollectBox.transform.parent = HelperBoxInventory.transform;
        CollectBox.transform.localPosition = Vector3.up * (CollectBox.transform.localScale.y) * (HelperBoxInventory.transform.childCount - 1);
        CollectBox.transform.localRotation = Quaternion.Euler(Vector3.zero);

    }

    private void OnTriggerEnter(Collider other)
    {

        if (HelperBoxInventory.transform.childCount < PickAmount && other.transform.CompareTag("Collectable"))
        {
            PickUpBoxes(other.gameObject);   
        }
    }
}
