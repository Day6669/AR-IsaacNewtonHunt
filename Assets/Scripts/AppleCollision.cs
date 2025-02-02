using UnityEngine;
using TMPro;

public class AppleCollision : MonoBehaviour
{
    // Assign these in the Inspector
    public TextMeshPro defaultTextUI;
    public TextMeshPro appleHitTextUI1;
    public TextMeshPro appleHitTextUI2;
    public TextMeshPro goldenAppleTextUI;
    public TextMeshPro apple3MissedTextUI;

    private string[] reactions = {
        "Ouch! Maybe try that again gentler this time please?",
        "Could it be something pulling it down? Maybe... gravity?"
    };
    private string noHitReaction = "YIKES!!! That was close!";
    private string goldenAppleReaction = "Wow! The golden apple! This must be a true discovery!";
    private string defaultText = "What happens if you tap the tree?";
    private int reactionIndex = 0;

    private void Start()
    {
        ClearAllTexts();
        DisplayDefaultText();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collision detected with {collision.gameObject.name}");
        if (collision.gameObject.CompareTag("GoldenApple"))
        {
            DisplayGoldenAppleReaction();
        }
        else if (collision.gameObject.CompareTag("Apples") && collision.gameObject.name != "Apple3")
        {
            DisplayReaction();
            reactionIndex = (reactionIndex + 1) % reactions.Length;
        }
    }

    public void OnAppleMissed(GameObject apple)
    {
        if (apple.name == "Apple3")
        {
            DisplayApple3MissedReaction();
        }
    }

    private void DisplayReaction()
    {
        ClearAllTexts();
        if (reactionIndex == 0)
        {
            appleHitTextUI1.text = reactions[reactionIndex];
            appleHitTextUI1.gameObject.SetActive(true);
        }
        else if (reactionIndex == 1)
        {
            appleHitTextUI2.text = reactions[reactionIndex];
            appleHitTextUI2.gameObject.SetActive(true);
        }
    }

    private void DisplayGoldenAppleReaction()
    {
        ClearAllTexts();
        goldenAppleTextUI.text = goldenAppleReaction;
        goldenAppleTextUI.gameObject.SetActive(true);
    }

    private void DisplayApple3MissedReaction()
    {
        ClearAllTexts();
        apple3MissedTextUI.text = noHitReaction;
        apple3MissedTextUI.gameObject.SetActive(true);
    }

    private void DisplayDefaultText()
    {
        defaultTextUI.text = defaultText;
        defaultTextUI.gameObject.SetActive(true);
    }

    private void ClearAllTexts()
    {
        defaultTextUI.gameObject.SetActive(false);
        appleHitTextUI1.gameObject.SetActive(false);
        appleHitTextUI2.gameObject.SetActive(false);
        goldenAppleTextUI.gameObject.SetActive(false);
        apple3MissedTextUI.gameObject.SetActive(false);
    }
}










