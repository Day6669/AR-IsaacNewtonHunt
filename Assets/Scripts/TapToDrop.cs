using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TapToDrop : MonoBehaviour
{
    private List<ARRaycastHit> rayHits = new List<ARRaycastHit>();
    public ARRaycastManager rayman;
    public GameObject[] Apples; // Assign all regular apples here
    public GameObject GoldenApple; // Assign the golden apple in the Inspector

    public int score = 0;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Apples")
                    {
                        // Select a random apple from the array to instantiate
                        GameObject selectedApple = Apples[Random.Range(0, Apples.Length)];
                        Instantiate(selectedApple, hit.collider.transform.position + new Vector3(Random.Range(-1, 1), 1, Random.Range(-1, 1)), Quaternion.identity);
                        Destroy(hit.collider.gameObject);
                        score++;
                    }
                }
            }
        }
    }

    public string getScore()
    {
        return score.ToString();
    }
}
