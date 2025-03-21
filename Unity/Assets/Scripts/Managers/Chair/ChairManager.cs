using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChairManager : MonoBehaviour
{
    private static ChairManager instance;

    public static ChairManager GetInstance()
    {
        return instance;
    }

    private void Awake ()
    {
        if (instance != null && instance != this)
        {
            return;
        }

        instance = this;
    }

    public bool isChairOccupied(GameObject chairObject)
    {
        Chair chairComponent = chairObject.GetComponent<Chair>();
        return chairComponent.isOccupied;
    }

    public void setAllChairStatusToAvailable()
    {
        //GameObject chairInstance = new GameObject("Chair");
        //Chair chairComponent = chairInstance.AddComponent<Chair>();
        List<GameObject> chairs = RetrieveAllChairs();
        foreach (GameObject chair in chairs)
        {
           setChairStatus(chair, false);
        }
    }

    private List<GameObject> RetrieveAllChairs()
    {
        List<GameObject> chairs = new List<GameObject>();
        GameObject[] chairObjects = GameObject.FindGameObjectsWithTag("Chair");
        foreach (GameObject chair in chairObjects)
        {
            chairs.Add(chair);
        }

        return chairs;
    }

    private void setChairStatus(GameObject chairObject, bool status)
    {
        Chair chair = chairObject.GetComponent<Chair>();
        if (chair != null)
        {
            chair.isOccupied = status;
        }
    }

    public Vector3 FindFirstAvailableChairAndReturnWorldPosition()
    {
        List<GameObject> chairList = RetrieveAllChairs();
        foreach (GameObject chair in chairList)
        {
            if (!isChairOccupied(chair))
            {
                setChairStatus(chair, true);
                return chair.transform.position;
            }
        }
        return Vector3.zero;
    }
}
