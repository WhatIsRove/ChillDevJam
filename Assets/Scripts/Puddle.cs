using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puddle : MonoBehaviour
{
    [SerializeField] private float reductionFactor = 0.7f;
    [SerializeField] private int currentMop;
    [SerializeField] private float lerpSpeed = 0.9f;
    private int maxMop = 2;
    

   

    public void ReducePuddle()
    {
        //// Get the center point of the GameObject's bounds
        //Vector3 center = GetBoundsCenter();

        //// Store the original scale
        //Vector3 originalScale = transform.localScale;

        // Reduce the scale of the GameObject by 20%
        if (currentMop < maxMop)
        {
            Vector3 targetScale = transform.localScale * reductionFactor;
            StartCoroutine(SmoothScale(targetScale));
            currentMop++;
        }
        else if (currentMop >= maxMop)
        {
            Destroy(this.gameObject);
        }

        //// Adjust the position to keep the center unchanged
        //Vector3 newPosition = center + (transform.position - center) * reductionFactor;
        //transform.position = newPosition;
    }

    // Function to calculate the center of the GameObject's bounds
    private Vector3 GetBoundsCenter()
    {
        Renderer renderer = GetComponent<Renderer>();
        Bounds bounds = renderer != null ? renderer.bounds : new Bounds(transform.position, Vector3.zero);
        return bounds.center;
    }

    IEnumerator SmoothScale(Vector3 targetScale)
    {
        Vector3 initialScale = transform.localScale;
        float elapsedTime = 0;

        while (elapsedTime < lerpSpeed)
        {
            float t = Mathf.Clamp01(elapsedTime / lerpSpeed); // Clamp the interpolation factor between 0 and 1
            transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
    }
}
