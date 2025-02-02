using UnityEngine;
using TMPro;

public class AppleCounter : MonoBehaviour
{
    public TextMeshPro counterText; // Reference to the UI Text element
    private int applesCollected = 0; // Counter for collected apples
    public int totalApples = 4; // Total number of apples to collect
    public GameObject congratulationsPopup; // Optional: Popup to show when all apples are collected
    public GameObject trophy; // Reference to the Trophy GameObject

    private void Start()
    {
        // Ensure the Trophy is invisible at the start
        if (trophy != null)
        {
            trophy.SetActive(false);
        }
    }

    public void CollectApple()
    {
        applesCollected++;
        Debug.Log($"CollectApple called. Current count: {applesCollected}/{totalApples}");
        UpdateCounterUI();
        CheckTrophyDisplay();
    }

    private void UpdateCounterUI()
    {
        if (counterText != null)
        {
            counterText.text = $"Apples Collected: {applesCollected}/{totalApples}";

            // Change text color when all apples are collected
            if (applesCollected == totalApples)
            {
                counterText.color = Color.green; // Change text to green
                Debug.Log("All apples collected!");

                // Show a congratulations message or popup
                if (congratulationsPopup != null)
                {
                    congratulationsPopup.SetActive(true); // Show popup
                }
            }
        }
        else
        {
            Debug.LogError("Counter text UI is not assigned!");
        }
    }

    private void CheckTrophyDisplay()
    {
        // Make the trophy visible if all apples are collected
        if (applesCollected == totalApples && trophy != null)
        {
            trophy.SetActive(true);
            Debug.Log("Trophy displayed!");
        }
    }

    public int GetCollectedApples()
    {
        return applesCollected;
    }
}




