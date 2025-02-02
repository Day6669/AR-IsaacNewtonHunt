using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImageManager))]
public class PlaceTrackedImages : MonoBehaviour
{
    private ARTrackedImageManager _trackedImagesManager;

    // Reference to the single prefab to spawn (Newton MiniGame Scene)
    public GameObject NewtonMiniGamePrefab;

    // Keep track of instantiated prefab instances
    private readonly Dictionary<string, GameObject> _instantiatedPrefabs = new Dictionary<string, GameObject>();

    void Awake()
    {
        // Cache a reference to the Tracked Image Manager component
        _trackedImagesManager = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        // Attach event handler when tracked images change
        _trackedImagesManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        // Remove event handler
        _trackedImagesManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    // Event Handler
    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        // Loop through all newly detected tracked images
        foreach (var trackedImage in eventArgs.added)
        {
            var imageName = trackedImage.referenceImage.name;
            if (!_instantiatedPrefabs.ContainsKey(imageName))
            {
                // Instantiate the Newton MiniGame prefab at the tracked image's position
                var newPrefab = Instantiate(NewtonMiniGamePrefab, trackedImage.transform.position, Quaternion.identity);
                // Adjust the prefab's rotation to face the camera correctly (rotate by 180 degrees)
                newPrefab.transform.LookAt(Camera.main.transform);
                newPrefab.transform.Rotate(0f, 180f, 0f); // Rotate it by 180 degrees to face the camera correctly

                _instantiatedPrefabs[imageName] = newPrefab;
            }
        }

        // For all prefabs that have been created so far, set them active or inactive
        foreach (var trackedImage in eventArgs.updated)
        {
            if (_instantiatedPrefabs.ContainsKey(trackedImage.referenceImage.name))
            {
                var prefab = _instantiatedPrefabs[trackedImage.referenceImage.name];
                prefab.transform.position = trackedImage.transform.position;
                prefab.transform.rotation = trackedImage.transform.rotation;

                // Adjust the prefab's rotation to face the camera correctly (rotate by 180 degrees)
                prefab.transform.LookAt(Camera.main.transform);
                prefab.transform.Rotate(0f, 180f, 0f); // Rotate it by 180 degrees to face the camera correctly

                prefab.SetActive(trackedImage.trackingState == TrackingState.Tracking);
            }
        }

        // Handle removed images (destroy prefabs when the image is no longer tracked)
        foreach (var trackedImage in eventArgs.removed)
        {
            if (_instantiatedPrefabs.ContainsKey(trackedImage.referenceImage.name))
            {
                Destroy(_instantiatedPrefabs[trackedImage.referenceImage.name]);
                _instantiatedPrefabs.Remove(trackedImage.referenceImage.name);
            }
        }
    }
}





