using System.Collections;
using UnityEngine;
using TMPro; // For TextMeshPro

public class TapToZoom : MonoBehaviour
{
    public Transform zoomTarget; // The position where the apple should zoom
    public float zoomSpeed = 2.0f; // Speed of the zoom
    private bool isZooming = false; // To prevent multiple actions at once

    private AppleCounter appleCounter; // Reference to the AppleCounter script
    public TextMeshProUGUI funFactText; // Reference to the fun fact UI text (TextMeshPro)

    // Reference to the different FunFacts (FunFact1, FunFact2, etc.)
    public GameObject FunFact1;
    public GameObject FunFact2;
    public GameObject FunFact3;
    public GameObject FunFact4;

    private static GameObject previousApple; // Keeps track of the last apple clicked

    private void Start()
    {
        // Initialize fun facts to be hidden initially
        FunFact1.SetActive(false);
        FunFact2.SetActive(false);
        FunFact3.SetActive(false);
        FunFact4.SetActive(false);

        // Find the AppleCounter attached to the Tree
        GameObject tree = GameObject.FindWithTag("Tree");
        if (tree != null)
        {
            appleCounter = tree.GetComponent<AppleCounter>();
        }

        if (appleCounter == null)
        {
            Debug.LogError("AppleCounter script not found on the tree!");
        }

        if (funFactText != null)
        {
            funFactText.text = ""; // Ensure the fun fact text is initially empty
            funFactText.gameObject.SetActive(false); // Hide the fun fact UI initially
        }
    }

    private void Update()
    {
        // Check for touch input (tap)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                // Cast a ray from the touch point
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    // Check if the touched object is this apple
                    if (hit.transform == transform && !isZooming)
                    {
                        // Destroy the previous apple when the next one is clicked
                        if (previousApple != null)
                        {
                            // Destroy(previousApple); // Destroy the previous apple
                            previousApple.transform.position = new Vector3(0, -100, 0); // Hide previous apple
                        }

                        // Set this apple as the previous apple for the next tap
                        previousApple = hit.transform.gameObject;

                        Debug.Log($"Tapped on {gameObject.name}");

                        // Disable gravity to prevent the apple from falling while it's moving
                        Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
                        if (rb != null)
                        {
                            rb.useGravity = false; // Disable gravity and keep it off
                        }

                        StartCoroutine(ZoomToTarget());

                        // Increment the apple counter
                        if (appleCounter != null)
                        {
                            appleCounter.CollectApple();
                        }

                        // Display the corresponding fun fact
                        ShowFunFact();
                    }
                }
            }
        }
    }

    private void ShowFunFact()
    {
        // Hide all fun facts initially
        FunFact1.SetActive(false);
        FunFact2.SetActive(false);
        FunFact3.SetActive(false);
        FunFact4.SetActive(false);

        // Based on which apple is tapped, display the corresponding fun fact
        if (gameObject.name == "Apple1")
        {
            FunFact1.SetActive(true);
        }
        else if (gameObject.name == "Apple2")
        {
            FunFact2.SetActive(true);
        }
        else if (gameObject.name == "Apple3")
        {
            FunFact3.SetActive(true);
        }
        else if (gameObject.name == "GoldenApple")
        {
            FunFact4.SetActive(true);
        }

        // Optionally, display the fun fact in the UI Text
        if (funFactText != null)
        {
            funFactText.gameObject.SetActive(true); // Show the UI text
            funFactText.text = $"Fun fact about {gameObject.name}!"; // Change to the appropriate fun fact
        }
    }

    private IEnumerator ZoomToTarget()
    {
        isZooming = true; // Prevent multiple zooms

        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;

        Vector3 targetPosition = zoomTarget.position;
        Quaternion targetRotation = zoomTarget.rotation;

        float elapsedTime = 0;

        // Move smoothly toward the target
        while (elapsedTime < 1.0f)
        {
            elapsedTime += Time.deltaTime * zoomSpeed;

            // Move smoothly toward the target
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime);
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime);

            yield return null;
        }

        Debug.Log($"{gameObject.name} reached the zoom target.");

        // Gravity remains off after the zoom process
        Rigidbody rb = transform.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Gravity is NOT re-enabled here, keeping it off
        }

        isZooming = false; // Allow next zoom
    }
}





















