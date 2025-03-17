using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
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

    public Vector3Int FindFirstAvailableSlotAndReturnGridPosition()
    {

        List<GameObject> slotList = RetrieveAllSlots();
        Grid grid = FindFirstObjectByType<Grid>();

        foreach (GameObject slot in slotList)
        {
            if (!isSlotOccupied(slot))
            {
                Vector3 worldPosition = slot.transform.position;
                Vector3Int gridPosition = grid.WorldToCell(worldPosition);
                setSlotStatus(slot, true);
                return gridPosition;
            }
        }

        return Vector3Int.zero;
    }
}
