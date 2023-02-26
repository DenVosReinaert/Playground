using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour, IInteractable
{
    public bool open;

    public List<KeyController> requiredKeys;
    List<bool> requiredKeysCollected;

    void Start()
    {
        if (requiredKeys.Count > 0)
            foreach (KeyController key in requiredKeys)
            {
                requiredKeysCollected.Add(false);
            }
    }

    public void Interact()
    {
        if (!open)
            switch (requiredKeys.Count)
            {
                case (> 0):
                    if (requiredKeysCollected.Contains(false))
                    {
                        Debug.Log("You don't have the necessary key(s) to open this door!");
                        return;
                    }
                    else
                        open = true;

                    break;


                case (<= 0):
                    open = true;

                    break;
            }

        if (open)
            this.gameObject.SetActive(false);
    }

    public void CollectKey(KeyController collectedKey)
    {
        if (requiredKeys.Count > 0)
        {
            for (int i = 0; i < requiredKeys.Count; i++)
            {
                if (requiredKeys[i] == collectedKey)
                {
                    requiredKeysCollected[i] = true;
                    return;
                }
            }
        }
        else
        {
            Debug.LogError("ERROR: This door does not require any keys but is trying to collect one.");
        }
    }
}
