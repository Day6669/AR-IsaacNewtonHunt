using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleDropper : MonoBehaviour
{
    public GameObject[] Apples;
    public GameObject GoldenApple;
    public GameObject Apple3; // Specific apple for missed reaction
    private int currentAppleIndex = 0;

    private AppleCollision appleCollision;
    private List<GameObject> activeApples = new List<GameObject>();
    private float checkInterval = 2.0f;

    private void Awake()
    {
        appleCollision = FindObjectOfType<AppleCollision>();
        if (appleCollision == null)
        {
            Debug.LogError("AppleCollision script not found in the scene!");
        }

        StartCoroutine(CheckForMisses());
    }

    public void DropNextApple()
    {
        Debug.Log("Attempting to drop the next apple...");

        if (currentAppleIndex < Apples.Length)
        {
            GameObject apple = Apples[currentAppleIndex];
            Rigidbody rb = apple.GetComponent<Rigidbody>();

            if (rb != null && !rb.useGravity)
            {
                rb.useGravity = true;

                // Adjust the apple's mass based on its scale (assuming uniform scaling)
                float scaleFactor = apple.transform.localScale.x; // Get scale factor
                rb.mass = Mathf.Max(0.1f, rb.mass * scaleFactor); // Adjust mass based on the scale factor (ensure it's not too small)

                // Optionally apply a smaller gravity force if needed
                // rb.AddForce(Vector3.down * 5f, ForceMode.Acceleration);  // Uncomment if you want custom gravity

                Debug.Log($"Dropping apple: {apple.name}");
                activeApples.Add(apple);
                currentAppleIndex++;
            }
        }
        else if (GoldenApple != null && GoldenApple.GetComponent<Rigidbody>() != null && !GoldenApple.GetComponent<Rigidbody>().useGravity)
        {
            Rigidbody rb = GoldenApple.GetComponent<Rigidbody>();
            if (rb != null && !rb.useGravity)
            {
                rb.useGravity = true;

                // Adjust the golden apple's mass based on scale
                float scaleFactor = GoldenApple.transform.localScale.x;
                rb.mass = Mathf.Max(0.1f, rb.mass * scaleFactor);

                Debug.Log("Dropping the golden apple!");
                activeApples.Add(GoldenApple);
            }
        }
    }

    private IEnumerator CheckForMisses()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkInterval);

            for (int i = activeApples.Count - 1; i >= 0; i--)
            {
                GameObject apple = activeApples[i];
                if (apple != null)
                {
                    Rigidbody rb = apple.GetComponent<Rigidbody>();
                    // Reduce the threshold for smaller apples
                    if (rb.velocity.magnitude < 0.05f)  // Lower threshold for slower-moving apples
                    {
                        Debug.Log($"Miss detected for apple: {apple.name}");
                        appleCollision.OnAppleMissed(apple); // Pass the apple object for specific handling
                        activeApples.RemoveAt(i);
                    }
                }
            }
        }
    }
}















