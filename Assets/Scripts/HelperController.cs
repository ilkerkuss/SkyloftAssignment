using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class HelperController : MonoBehaviour,IPlayerController
{
    public NavMeshAgent HelperNavMeshAgent;

    public ZoneController ParentZone;

    public CubeZoneController HelpZone;
    public GameObject HelperBoxInventory;
    public int PickAmount;
    public List<GameObject> HelperBoxList;

    public GameObject StorageObject;
    public Vector3 Destination;
    public bool HaveDestination;


    void Start()
    {
        HaveDestination = false;
        ParentZone = GetComponentInParent<ZoneController>();
    }


    void Update()
    {
        if (ParentZone.IsZoneActive)
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

                HelpZone.CollectableBoxes.RemoveAll(item => item = null); // clear null item from collectableboxlist which is stored in cubezone
            }

        }

    }
        


    private void MoveToZone(Vector3 DestinationPoint)
    {
        HelperNavMeshAgent.destination = DestinationPoint;
    }

    private void HelperPickCollectableBoxes(GameObject CollectBox)
    {
        //HelpZone.CollectableBoxes.Remove(CollectBox);
        CollectBox.GetComponentInParent<CubeZoneController>().CollectableBoxes.Remove(CollectBox);

        CollectBox.transform.parent = HelperBoxInventory.transform;
        CollectBox.tag = "Untagged";
        //CollectBox.transform.localPosition = Vector3.up * (CollectBox.transform.localScale.y) * (HelperBoxInventory.transform.childCount - 1);
        CollectBox.transform.DOLocalJump((Vector3.up * (CollectBox.transform.localScale.y) * (HelperBoxInventory.transform.childCount - 1)), 2, 1, 1f);
        CollectBox.transform.localRotation = Quaternion.Euler(Vector3.zero);

        HelperBoxList.Add(CollectBox);

    }

    public GameObject GetLastBox()
    {
        if (HelperBoxList.Count == 0)
        {
            return null;
        }

        var item = HelperBoxList[HelperBoxList.Count - 1];
        HelperBoxList.Remove(item);

        return item;
    }


    private void OnTriggerEnter(Collider other)
    {

        if (HelperBoxInventory.transform.childCount < PickAmount && other.transform.CompareTag("Collectable") && other.GetComponentInParent<CubeZoneController>())
        {
            HelperPickCollectableBoxes(other.gameObject);   
        }
    }

   
}
