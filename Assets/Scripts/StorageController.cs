using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StorageController : MonoBehaviour
{
    public int StorageCapacity=40;

    public List<GameObject> StorageList;
    public Transform LastStoredBoxTransform;

    public Transform StorageInventory;

    public Renderer StorageRenderer;
    public Vector3 FirstPositionOfStorage = new Vector3(); //new Vector3(-3,.5f,2);
    public Vector3 LastPositionOfStorage;
    public int Direction = -1;

    private WaitForSeconds Cooldown = new WaitForSeconds(0.25f);
    private bool IsReady = true;

    void Start()
    {
        FirstPositionOfStorage = new Vector3(-(StorageRenderer.bounds.extents.x)+.5f, .5f, StorageRenderer.bounds.extents.z+.5f);
        LastPositionOfStorage = FirstPositionOfStorage;
        
    }


    void Update()
    {
        StorageList.RemoveAll(item =>item=null);

        if (StorageList.Count == StorageCapacity)
        {
            LastPositionOfStorage = FirstPositionOfStorage;
        }
    }

    public IEnumerator AlignBox(GameObject Box)
    {
        IsReady = false;
        yield return Cooldown;
        IsReady = true;

        if (StorageList.Count % 5 == 0 && StorageList.Count!= 0)
        {
            LastPositionOfStorage.x += 1;
            Direction = -Direction;
        }
        else
        {
            LastPositionOfStorage.z += Direction;
        }
        /*
        Box.transform.DOJump(LastPositionOfStorage, 2, 1, 1).OnComplete(() => 
        {
            Box.transform.localPosition = LastPositionOfStorage;
            StorageList.Add(Box);
        }
        */
        Box.transform.localPosition = LastPositionOfStorage;
        StorageList.Add(Box);


    }

    

    private void OnTriggerStay(Collider other)
    {
        if (StorageList.Count < StorageCapacity)
        {
            if (!IsReady)
            {
                return;
            }
            var Box = other.GetComponent<IPlayerController>()?.GetLastBox();

            if (Box)
            {
                Box.tag = "Collectable";
                Box.transform.parent = StorageInventory;

                //Box.transform.DOJump(StorageInventory.position,2,1,1);
                Box.transform.rotation = Quaternion.Euler(Vector3.zero);

                StartCoroutine(AlignBox(Box));

            }

        }
        
        
    }
}
