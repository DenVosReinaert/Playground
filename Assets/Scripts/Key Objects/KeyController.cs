using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour, IInteractable
{
    public List<DoorController> connectedDoors;

    public void Interact()
    {
        foreach(DoorController door in connectedDoors)
        {
            door.CollectKey(this);
        }
            this.gameObject.SetActive(false);
    }
}
