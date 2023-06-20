
// ******------------------------------------------------------******
// ProductGameObject.cs
//
// All product type gameplay objects inherits from this base class
//
// Author:
//       K.Sinan Acar <ksa@puzzledwizard.com>
//
// Copyright (c) 2020 PuzzledWizard
//
// ******------------------------------------------------------******
using UnityEngine;
using System.Collections;
namespace PW
{
    public class ProductGameObject : MonoBehaviour
    {
        const float totalTimeGoingToSlot = .4f;

        //Some products can be visually different while they served.
        //This is not available for ReadyToServe objects.
        public GameObject serveAsDifferentGameObject;

        //Product orderID
        public int orderID;

        //This is inherited on ReadyToServe, CookableProduct and HeatableProduct
        //DrinakbleProduct dont use that buy you can use it with a prefab, different than plate,
        //such as a takeaway package.
        public bool AddToPlateBeforeServed;

        //This is usually Vector.zero but someObjects may require that,
        //in our case unfortunaly cookies required that;
        public Vector3 plateOffset;

        public bool RegenerateProduct = false;

        private OrderGenerator orderGenerator;

        private void Start()
        {
            //Debug.Log("This is avaliable????? : " + GameObject.FindGameObjectsWithTag("OrderGenerator")[0].GetComponent<OrderGenerator>());
            //orderGenerator = GameObject.FindGameObjectsWithTag("OrderGenerator")[0].GetComponent<OrderGenerator>();
        }


        // 오브젝트를 눌렀을 때 움직이는 이벤트 발생하도록 하는 것
        // 해당 함수에 인벤토리에 아이템을 추가하는 함수 실행하도록 할 것
        // 또한 컨트롤러로 해당 이벤트를 발생시키도록 해야 한다. 
        public virtual IEnumerator AnimateGoingToSlot()
        {
            if (serveAsDifferentGameObject != null)
            {

                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }

                var go = Instantiate(serveAsDifferentGameObject, transform);
                go.transform.SetAsFirstSibling();
            }

            float curTime = totalTimeGoingToSlot;

            //slot is in the lower center of the camera.
            //which means 0.5f on x axis and 0f on Y axis of viewport
            //we find the world position according to the camera viewport
            //We convert the Vector2 to Vector3 by adding camera pos in Z
            Vector3 viewPositionOfSlots = new Vector3(0.5f, 0f, Camera.main.nearClipPlane + 2f);

            //If you want your objects to go to somewhere else in screen or world,
            //change centerPos to another Vector3;
            Vector3 centerPos = new Vector3(-1.646f, 0.878f, 0.447f);
            Vector3 totalDist = (centerPos - transform.position);

            while (curTime > 0)
            {
                float timePassed = Time.deltaTime;
                transform.position += timePassed * totalDist / totalTimeGoingToSlot;
                curTime -= timePassed;
                yield return null;
            }

            yield return null;

        }

        public virtual bool CanGoPlayerSlot()
        {

            //var PlayerSlots = FindObjectOfType<PlayerSlots>();
            //if (PlayerSlots.CanHoldItem(orderID))
            //{
            //    BasicGameEvents.RaiseOnProductAddedToSlot(orderID);
            //    return true;
            //}
            //else
            //    return false;

            orderGenerator = GameObject.FindGameObjectsWithTag("OrderGenerator")[0].GetComponent<OrderGenerator>();
            //StartCoroutine(MoveToPlace(new Vector3(-1.597f, 0.989f, 0.299f)));
            Debug.Log(orderID);
            Debug.Log(orderGenerator);
            orderGenerator.UpdateOrderList(orderID);
            Destroy(gameObject);
            return true;
        }

        /// <summary>
        /// This is used on heatable and cookable objects
        /// For moving the object to microwave or stove
        /// But giving a targetPos you can use it any place
        /// </summary>
        /// <param name="targetPos"></param>
        /// <returns></returns>
        public virtual IEnumerator MoveToPlace(Vector3 targetPos)
        {

            float totalTime = 1f;
            float curTime = totalTime;
            var totalDist = (targetPos - transform.position);
            while (curTime > 0)
            {
                var timePassed = Time.deltaTime;
                transform.position += timePassed * totalDist / totalTime;
                curTime -= timePassed;
                yield return null;
            }

            transform.position = targetPos;
            yield return null;

        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                if (OVRInput.Get(OVRInput.Button.One) || OVRInput.Get(OVRInput.Button.Three))
                {
                    CanGoPlayerSlot();
                }
            }
        }
    } 
}
