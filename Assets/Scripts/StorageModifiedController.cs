using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageModifiedController : MonoBehaviour
{
    public int StorageCapacity = 40;

    public GameObject StorageNode;
    public List<GameObject> StorageNodeList;
    public int Direction = -1;
    

    public List<GameObject> StorageList;
    //public Transform LastStoredBoxTransform;

    public Transform ModifiedStorageInventory;

    public Renderer ModifiedStorageRenderer;
    public Vector3 FirstPositionOfStorage; //new Vector3(-3,.5f,2);
    public Vector3 LastPositionOfStorage;




    void Start()
    {
        FirstPositionOfStorage = new Vector3(-(ModifiedStorageRenderer.bounds.extents.x) + .5f, .5f, ModifiedStorageRenderer.bounds.extents.z + .5f);
        LastPositionOfStorage = FirstPositionOfStorage;

        CreateEmptyNodes(StorageCapacity);
    }


    void Update()
    {
        
    }


    private void CreateEmptyNodes(int StorageCapacity)
    {
        for (int i = 0; i < StorageCapacity; i++)
        {
            GameObject newStorageNode = Instantiate(StorageNode, ModifiedStorageInventory);
            
            AlignItem(newStorageNode);

            StorageNodeList.Add(newStorageNode);
        }
    }

    public void AlignItem(GameObject Item)
    {
        
        if (StorageNodeList.Count % 5 == 0 && StorageNodeList.Count != 0)
        {
            LastPositionOfStorage.x += 1;
            Direction = -Direction;
        }
        else
        {
            LastPositionOfStorage.z += Direction;
        }

        Item.transform.localPosition = LastPositionOfStorage;

    }

    private GameObject GetEmptyNode(List<GameObject> NodeList)
    {

        foreach (GameObject Node in NodeList)
        {
            if (Node.transform.childCount == 0)
            {
                return Node;
                
            }
        }

        return null;

    }

    private void OnTriggerStay(Collider other)
    {
        if (StorageList.Count < StorageCapacity)
        {
            var Box = other.GetComponent<IPlayerController>()?.GetLastBox();
            var emptyNode = GetEmptyNode(StorageNodeList);

            if (Box)
            {
                Box.tag = "Collectable";
                Box.transform.parent = emptyNode.transform;
                Box.transform.localPosition = Vector3.zero;
                Box.transform.rotation = Quaternion.Euler(Vector3.zero);
                StorageList.Add(Box);

                //AlignItem(Box);

            }
        }
    }
}
