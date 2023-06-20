// ******------------------------------------------------------******
// DrinkableProduct.cs
// Inherits from ProductGameObject
//
// Author:
//       K.Sinan Acar <ksa@puzzledwizard.com>
//
// Copyright (c) 2019 PuzzledWizard
//
// ******------------------------------------------------------******
using UnityEngine;
using System.Collections;
namespace PW
{

    public class DrinkableProduct : ProductGameObject
    {
        private void Start()
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }

        public override IEnumerator AnimateGoingToSlot()
        {
            yield return base.AnimateGoingToSlot();
            Destroy(gameObject);
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                if (OVRInput.Get(OVRInput.Button.One) || OVRInput.Get(OVRInput.Button.Three))
                {
                    base.CanGoPlayerSlot();
                }
            }
        }

    }
}
