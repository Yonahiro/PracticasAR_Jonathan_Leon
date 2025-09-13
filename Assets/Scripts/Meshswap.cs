using System.Collections.Generic;
using UnityEngine;

public class ObjectSwapper : MonoBehaviour
{
    public List<GameObject> swappableObjects;

    private void Start()
    {
        ChangeObject();
    }

    public void OnButtonClick()
    {
        ChangeObject();
    }

    private void ChangeObject()
    {
        foreach (GameObject obj in swappableObjects)
        {
            obj.SetActive(false);
        }

        if (swappableObjects.Count > 0)
        {
            int randomIndex = Random.Range(0, swappableObjects.Count);
            swappableObjects[randomIndex].SetActive(true);
        }
        else
        {
            Debug.LogWarning("The list of swappable objects is empty.");
        }
    }
}