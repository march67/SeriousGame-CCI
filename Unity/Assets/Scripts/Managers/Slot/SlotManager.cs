using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
   private static SlotManager instance;
   private const string SlotTag = "Slot";

    public static SlotManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance != null && instance != this )
        {
            Debug.Log("SlotManager instance already exists");
            return;
        }

        instance = this;
    }

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

    public void setAllSlotStatusToAvailable()
    {
        List<GameObject> slots = RetrieveAllSlots();
        foreach (GameObject slot in slots)
        {
            setSlotStatus(slot, false);
        }
    }
    private List<GameObject> RetrieveAllSlots()
    {
        List<GameObject> slots = new List<GameObject>();
        GameObject[] slotObjects = GameObject.FindGameObjectsWithTag(SlotTag);
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
