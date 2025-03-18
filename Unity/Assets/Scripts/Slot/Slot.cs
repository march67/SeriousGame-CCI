using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public bool isOccupied = false;

    public bool isSlotOccupied(GameObject slotObject)
    {
        Slot slot = slotObject.GetComponent<Slot>();
        return slot.isOccupied;
    }

    public void setSlotStatus(GameObject slotObject, bool status)
    {
        Slot slot = slotObject.GetComponent<Slot>();
        if (slot != null)
        {
            slot.isOccupied = status;
        }
    }

    public static void setAllSlotStatusToAvailable()
    {
        GameObject slotInstance = new GameObject("Slot");
        Slot slotComponent = slotInstance.AddComponent<Slot>();
        List<GameObject> slots = slotComponent.RetrieveAllSlots();
        foreach (GameObject slot in slots)
        {
            slotComponent.setSlotStatus(slot, false);
        }
    }
    private List<GameObject> RetrieveAllSlots()
    {
        List<GameObject> slots = new List<GameObject>();
        GameObject[] slotObjects = GameObject.FindGameObjectsWithTag("Slot");
        foreach (GameObject slot in slotObjects)
        {
            slots.Add(slot);
        }

        return slots;
    }

    public Vector3 FindFirstAvailableSlotAndReturnWorldPosition()
    {
        List<GameObject> slotList = RetrieveAllSlots();
        foreach (GameObject slot in slotList)
        {
            if (!isSlotOccupied(slot))
            {
                setSlotStatus(slot, true);
                return slot.transform.position;
            }
        }
        return Vector3.zero;
    }
}
