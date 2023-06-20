// ******------------------------------------------------------******
// OrderGenerator.cs
//
// Author:
//       K.Sinan Acar <ksa@puzzledwizard.com>
//
// Copyright (c) 2019 PuzzledWizard
//
// ******------------------------------------------------------******

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace PW
{
    public class OrderGenerator : MonoBehaviour
    {
        //This limits generating orders constantly
        public int MaxConcurrentOrder=4;

        public int currentOrderCount = 0;

        public Sprite[] orderSprites;

        public TextMeshProUGUI orderText;
        public TextMeshProUGUI pointText;

        private Item[] orderName;

        private int clearNum = 0;

        public GameObject totalPrfab;

        public class Item
        {
            public string name;
            // 0은 핸드 트래킹 1은 컨트롤러
            public int type;
            public int orderNum;
            public int productNum;
            public int ID;

            public Item(int ID, string name, int type, int orderNum, int productNum)
            {
                this.ID = ID;
                this.name = name;
                this.type = type;
                this.orderNum = orderNum;
                this.productNum = productNum;

            }
        }

        [HideInInspector]
        public int[] orderedProducts;

        public Transform UIParentForOrders;

        public GameObject orderRepPrefab;//The general prefab for order represantation

        private bool isCurrentOrder = false;
        private int totalOrderNum = 8;

        private Item[] orderList;

        private void Start()
        {
            currentOrderCount = 0;
            pointText.text = currentOrderCount.ToString();
            orderList = new Item[totalOrderNum];
            orderName = new Item[26];
            // 이름, 컨트롤 타입, 남은 수량, 구현된 수량
            orderName[0] = new Item(1, "cheesecake chcolatte", 0, 0, 8);
            orderName[1] = new Item(2, "cheesecake lime", 0, 0, 8);
            orderName[2] = new Item(3, "cheesecake strawberry", 0, 0, 5);
            orderName[3] = new Item(4, "cookie", 0, 0, 7);
            orderName[4] = new Item(5, "croissant", 0, 0, 0);
            orderName[5] = new Item(6, "croissant toserve", 1, 0, 1);
            orderName[6] = new Item(7, "cup", 1, 0, 1);
            orderName[7] = new Item(8, "cupdcake cherry", 0, 0, 3);
            orderName[8] = new Item(9, "cupdcake oreo", 0, 0, 3);
            orderName[9] = new Item(10, "cupcake redvelvet", 0, 0, 2);
            orderName[10] = new Item(11, "doughnut blackNwhite", 0, 0, 2);
            orderName[11] = new Item(12, "doughnut pink", 0, 0, 1);
            orderName[12] = new Item(13, "doughnut white", 0, 0, 1);
            orderName[13] = new Item(14, "espresso cup", 1, 0, 1);
            orderName[14] = new Item(15, "frenchtoas cherry tomato", 1, 0, 1);
            orderName[15] = new Item(16, "frenchtoas egg", 1, 0, 1);
            orderName[16] = new Item(17, "frenchtoas olives", 1, 0, 1);
            orderName[17] = new Item(18, "frenchtoas orange", 1, 0, 1);
            orderName[18] = new Item(19, "frenchtoas strawberry", 1, 0, 1);
            orderName[19] = new Item(20, "macaronbox", 0, 0, 1);
            orderName[20] = new Item(21, "tea cup", 1, 0, 1);
            orderName[21] = new Item(22, "toast cheese", 1, 0, 1);
            orderName[22] = new Item(23, "cupcake chocolattechips", 0, 0, 2);
            orderName[23] = new Item(24, "orange juice", 0, 0, 4);
            orderName[24] = new Item(25,"lemonade", 0, 0, 4);
            orderName[25] = new Item(26, "cheesecake blueberry", 0, 0, 2);
        }

        private void Update()
        {
            if (!isCurrentOrder)
            {
                GenerateOrder();
            }
        }

        public void GenerateOrder()
        {
            bool dupleCheck = false;
            orderText.text = "";
            int randNum = -1;
            Debug.Log("Generating order");
            for(int i=0; i< totalOrderNum; i++)
            {

                do
                {
                    dupleCheck = false;
                    randNum = Random.Range(0, orderName.Length);

                    if ((randNum + 1)  == 5)
                    {
                        dupleCheck = true;
                    }
                    else
                    {
                        for (int j = 0; j < i; j++)
                        {
                            if (orderList[j].ID == (randNum + 1))
                            {
                                dupleCheck = true;
                                break;
                            }
                        }
                    }

                } while (dupleCheck);
                orderList[i] = new Item(orderName[randNum].ID, orderName[randNum].name, orderName[randNum].type, orderName[randNum].orderNum, orderName[randNum].productNum);
                if (orderList[i].type == 0)
                {
                    orderList[i].orderNum = Random.Range(1, orderList[i].productNum);
                    orderText.text += orderList[i].name + " x" + orderList[i].orderNum + "\n";
                }
                else
                {
                    orderList[i].orderNum = 1;
                    orderText.text += orderList[i].name + "\n";
                }
                    
            }
            isCurrentOrder = true;
        }

        public void UpdateOrderList(int ProductID)
        {
            clearNum = 0;
            orderText.text = "";
            for (int i = 0; i < totalOrderNum; i++)
            {
                if (orderList[i].ID == ProductID && orderList[i].orderNum != 0)
                {
                    orderList[i].orderNum--;
                }


                if (orderList[i].orderNum == 0)
                {
                    orderText.text += "=====Clear===== \n";
                    clearNum++;
                }
                else if (orderList[i].type == 0)
                {
                    orderText.text += orderList[i].name + " x" + orderList[i].orderNum + "\n";
                }
                else
                {
                    orderText.text += orderList[i].name + "\n";
                }

            }

            if(clearNum == totalOrderNum)
            {
                IncreasePoint();
                GenerateOrder();
                ResetObjectys();
            }
        }

        public void IncreasePoint()
        {
            currentOrderCount++;
            pointText.text = currentOrderCount.ToString();
        }

        public void ResetObjectys()
        {
            GameObject previousObject = GameObject.FindWithTag("Finish");

            if (previousObject != null)
            {
                DestroyImmediate(previousObject, true);
            }

            Instantiate(totalPrfab);
        }


        public Sprite GetSpriteForOrder(int orderID)
        {
            var spriteIndex = Array.IndexOf(orderedProducts, orderID);
            if (spriteIndex<0)
                return null;
            return orderSprites[spriteIndex];
        }
    }
}