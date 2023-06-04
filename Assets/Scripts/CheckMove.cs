using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class CheckMove : MonoBehaviour
{
    public Collider pointZone;

    public TextMeshProUGUI text;
    public TextMeshProUGUI buttonTime;

    private int point=0;
    private int checkNum=0;

    private bool update = false;
    private float checkTime = 0;

    private bool checkNull = false;

    // Start is called before the first frame update
    void Start()
    {
        text.text = point.ToString();
        buttonTime.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        if (update)
        {
            text.text = point.ToString();
            update = false;
        }

        if(checkNum >= 5)
        {
            checkTime += Time.deltaTime;
            buttonTime.text = string.Format("{0:N1}", checkTime);
        }

        if(checkNum == 0)
        {
            checkNull = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Point")
        {
            checkNum++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Point")
        {
            checkNum--;
        }
    }

    public void IncreasePoint()
    {
        if (checkNum == 5 && !checkNull)
        {
            point++;
            update = true;
            checkNull = true;
        }
    }

    public void ResetPoint()
    {
        point = 0;
    }

}
