using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    public bool isOccupied = false;

    public bool isChairOccupied(GameObject chairObject)
    {
        Chair chair = chairObject.GetComponent<Chair>();
        return chair.isOccupied;
    }

    public void setChairStatus(GameObject chairObject, bool status)
    {
        Chair chair = chairObject.GetComponent<Chair>();
        if (chair != null)
        {
            chair.isOccupied = status;
        }
    }

    public static void setAllChairStatusToAvailable()
    {
        GameObject chairInstance = new GameObject("Chair");
        Chair chairComponent = chairInstance.AddComponent<Chair>();
        List<GameObject> chairs = chairComponent.RetrieveAllChairs();
        foreach (GameObject chair in chairs)
        {
            chairComponent.setChairStatus(chair, false);
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
