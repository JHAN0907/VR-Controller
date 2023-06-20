using PW;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckProduct : MonoBehaviour
{
    public OrderGenerator OrderControl; 


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay(Collider other)
    {
        ProductGameObject test = null;
        if (test = other.gameObject.GetComponent<ProductGameObject>())
        {
            Debug.Log(test.orderID);
            OrderControl.UpdateOrderList(test.orderID);
            Destroy(test.gameObject);
        }
    }
}
