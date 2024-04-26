using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;


namespace SNRInputManager
{
    public static class InputManager
    {
        private static ActInPutCtr _actGameInputController;
        public static ActInPutCtr ActController
        {
            get
            {
                if (_actGameInputController == null)
                {
                    _actGameInputController = new ActInPutCtr();
                }

                return _actGameInputController;
            }
        }

        static UniversalInputCtr _universalInputController;
        public static UniversalInputCtr UInputCtr
        {
            get
            {
                if (_universalInputController == null)
                {
                    _universalInputController = new UniversalInputCtr();
                }
                return _universalInputController;
            }
        }


        public static void EnableLayerBack(Action<InputAction.CallbackContext> pPerform)
        {
            UInputCtr.UI.LayerBack.Enable();
            if (pPerform != null)
            {
                UInputCtr.UI.LayerBack.performed += pPerform;
            }
        }

        public static void DisableLayerBack(Action<InputAction.CallbackContext> pPerform)
        {
            UInputCtr.UI.LayerBack.Disable();
            if (pPerform != null)
            {
                UInputCtr.UI.LayerBack.performed -= pPerform;
            }
        }



        public static void DisableAll()
        {
            ActController.UI.MenuBtnSwitch.Disable();
            ActController.UI.Submit.Disable();
            ActController.UI.Cancel.Disable();
            UInputCtr.UI.LayerBack.Disable();
        }





        public static void EnableActUI(bool disableOtherInput = false)
        {
            if (disableOtherInput)
            {
                DisableAll();
            }

            ActController.UI.MenuBtnSwitch.Enable();
            ActController.UI.Submit.Enable();
            ActController.UI.Cancel.Enable();
        }







    }

}


