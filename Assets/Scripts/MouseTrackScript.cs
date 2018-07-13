using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class MouseTrackScript : MonoBehaviour {

    public GameObject tracker;
    public PostProcessingProfile profile;
    private float processingAmount = 0.15f;

    private void Start()
    {
        tracker = Instantiate(tracker);
    }

    // Update is called once per frame
    void Update () {
        Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        tracker.transform.position = MousePos;

        if (Input.GetMouseButtonDown(1))
        {
            //profile.chromaticAberration.enabled = !profile.chromaticAberration.enabled;
            processingAmount = 1.0f;
        }

        ChromaticAberrationModel.Settings settings = profile.chromaticAberration.settings;
        processingAmount = Mathf.Lerp(processingAmount, 0.15f, Time.deltaTime * 10f);
        settings.intensity = processingAmount;
        profile.chromaticAberration.settings = settings;
    }
}
