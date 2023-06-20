// ******------------------------------------------------------******
// PanGameObject.cs
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
    /// <summary>
    /// This class is just an example of how you can inherit from cookingGameObject.
    /// This class doesn't have overrides of base methods, and it doesn't have methods itself.
    /// The pan object on the gameplay scene uses this script and
    /// it just works by inheriting from the base class.
    /// </summary>
    public class PanGameObject : CookingGameObject
    {

        private void ClickButtonEvent()
        {
            base.ClickButtonEvent();

        }
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                if (OVRInput.Get(OVRInput.Button.One) || OVRInput.Get(OVRInput.Button.Three))
                {
                    ClickButtonEvent();
                }
            }
        }

    }
}
