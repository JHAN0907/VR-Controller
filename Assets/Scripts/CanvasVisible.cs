using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasVisible : MonoBehaviour
{
    public GameObject pointer;
    public GameObject camera;
    public GameObject canvas;

    private float value;

    public float standard = -0.7f;

    // Update is called once per frame
    void Update()
    {
        value = Quaternion.Dot(pointer.transform.rotation, camera.transform.rotation);

        if (value <= standard && OVRInput.IsControllerConnected(OVRInput.Controller.Hands))
        {
            canvas.SetActive(true);

        }
        else
        {
            canvas.SetActive(false);
        }

    }
}
