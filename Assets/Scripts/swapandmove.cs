using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class swampandmove : MonoBehaviour
{
    public Transform target1;
    public Transform target2;
    public Transform target3;
    public float movementDuration = 1.0f;
    public List<GameObject> sequentialModels;

    private int currentModelIndex = 0;
    private Transform currentTarget;

    void Start()
    {
        foreach (GameObject model in sequentialModels)
        {
            model.SetActive(false);
        }

        if (sequentialModels.Count > 0)
        {
            currentTarget = target1;
            sequentialModels[currentModelIndex].SetActive(true);
            sequentialModels[currentModelIndex].transform.SetParent(target1, false);
            sequentialModels[currentModelIndex].transform.localPosition = Vector3.zero;
        }
        else
        {
            Debug.LogError("La lista de modelos está vacía.");
        }
    }

    public void OnButtonClick()
    {
        Transform sourceTarget = currentTarget;

        sequentialModels[currentModelIndex].SetActive(false);

        int nextModelIndex = (currentModelIndex + 1) % sequentialModels.Count;
        Transform destinationTarget;

        if (sourceTarget == target1)
        {
            destinationTarget = target2;
        }
        else if (sourceTarget == target2)
        {
            destinationTarget = target3;
        }
        else
        {
            destinationTarget = target1;
        }

        currentModelIndex = nextModelIndex;
        currentTarget = destinationTarget;

        GameObject objectToMove = sequentialModels[currentModelIndex];
        objectToMove.SetActive(true);

        objectToMove.transform.position = sourceTarget.position;

        MoveContent(objectToMove.transform, destinationTarget);
    }

    private void MoveContent(Transform childToMove, Transform toTarget)
    {
        StopAllCoroutines();

        Vector3 targetLocalPosition = Vector3.zero;
        StartCoroutine(AnimateMovement(childToMove, toTarget, targetLocalPosition));
    }

    private IEnumerator AnimateMovement(Transform objectToMove, Transform newParent, Vector3 targetLocalPosition)
    {
        float elapsedTime = 0f;

        Vector3 startWorldPosition = objectToMove.position;

        objectToMove.SetParent(newParent, true);

        Vector3 endWorldPosition = newParent.TransformPoint(targetLocalPosition);

        objectToMove.position = startWorldPosition;

        while (elapsedTime < movementDuration)
        {
            float t = elapsedTime / movementDuration;

            t = Mathf.SmoothStep(0f, 1f, t);

            objectToMove.position = Vector3.Lerp(startWorldPosition, endWorldPosition, t);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        objectToMove.position = endWorldPosition;
        objectToMove.localPosition = targetLocalPosition;
    }
}