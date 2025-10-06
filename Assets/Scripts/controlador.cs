using UnityEngine;
using System.Collections;

public class controlador : MonoBehaviour
{
    public Transform sourceTarget;
    public Transform destinationTarget;

    private bool isAtSourceTarget = true;
    public float movementDuration = 1.0f;

    public void OnButtonClick()
    {
        if (isAtSourceTarget)
        {
            MoveContent(sourceTarget, destinationTarget);
            isAtSourceTarget = false;
        }
        else
        {
            MoveContent(destinationTarget, sourceTarget);
            isAtSourceTarget = true;
        }
    }

    private void MoveContent(Transform fromTarget, Transform toTarget)
    {
        StopAllCoroutines();

        Transform[] children = new Transform[fromTarget.childCount];
        for (int i = 0; i < fromTarget.childCount; i++)
        {
            children[i] = fromTarget.GetChild(i);
        }

        foreach (Transform child in children)
        {
            Vector3 targetLocalPosition = Vector3.zero;
            StartCoroutine(AnimateMovement(child, toTarget, targetLocalPosition));
        }
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