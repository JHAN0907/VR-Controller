using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using static MediaPlayerImage;

public class ButtonVR : MonoBehaviour
{
    public GameObject button;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    LayerMask presser;
    AudioSource sound;
    bool isPressed;

    public GameObject[] apples;

    private Vector3[] applesTransform;

    [SerializeField]
    private Transform resetTransform;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Camera playerHead;

    [SerializeField]
    private CheckMove checkMove;

    [SerializeField]
    private TextMeshProUGUI point;
    [SerializeField]
    private TextMeshProUGUI time;
    [SerializeField]
    private TextMeshProUGUI buttonTime;

    private float timeValue = 0;
    private bool timeTrigger = false;


    void Start()
    {
        sound = GetComponent<AudioSource>();
        isPressed = false;

        applesTransform = new Vector3[apples.Length];
        for(int i=0; i< apples.Length; i++)
        {
            applesTransform[i] = apples[i].transform.position;
        }
    }

    private void Update()
    {
        if (timeTrigger)
        {
            timeValue = timeValue + Time.deltaTime;
            time.text = string.Format("{0:N1}", timeValue);

            if (point.text == "5")
                timeTrigger = false;
        }

        if (OVRInput.GetUp(OVRInput.Button.One))
        {
            ResetApples();
        }

        if (OVRInput.GetUp(OVRInput.Button.Two))
        {
            ResetUser();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!isPressed)
        {
            button.transform.localPosition = new Vector3(0, 0.003f, 0);
            presser = other.gameObject.layer;
            onPress.Invoke();
            sound.Play();
            isPressed = true;

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == presser)
        {
            button.transform.localPosition = new Vector3(0, 0.015f, 0);
            onRelease.Invoke();
            isPressed = false;
        }

    }

    public void SpawnShphere()
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        sphere.transform.localPosition = new Vector3(0, 1, 2);
        sphere.AddComponent<Rigidbody>();
    }


    public void ResetApples()
    {
        for(int i=0; i< apples.Length; i++)
        {
            apples[i].transform.position = applesTransform[i];
        }
        checkMove.IncreasePoint();
    }

    public void ResetUser()
    {
        //float rotationAnbleY = resetTransform.rotation.eulerAngles.y - playerHead.transform.rotation.eulerAngles.y;
        //player.transform.Rotate(0, rotationAnbleY, 0);

        point.text = "0";
        time.text  = "0";
        buttonTime.text = "0";
        timeValue = 0;
        timeTrigger = true;
        checkMove.ResetPoint();
    }

    public void ResetPosition()
    {
        float rotationAnbleY = resetTransform.rotation.eulerAngles.y - playerHead.transform.rotation.eulerAngles.y;
        player.transform.Rotate(0, rotationAnbleY, 0);
    }
}
