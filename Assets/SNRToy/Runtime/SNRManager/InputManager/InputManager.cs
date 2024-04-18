using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


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

        public static void DisableAll()
        {
            ActController.UI.MenuBtnSwitch.Disable();
            ActController.UI.Submit.Disable();
            ActController.UI.Cancel.Disable();
        }





        public static void EnableActUI(bool disableOther = true)
        {
            if (disableOther)
            {
                DisableAll();
            }

            ActController.UI.MenuBtnSwitch.Enable();
            ActController.UI.Submit.Enable();
            ActController.UI.Cancel.Enable();
        }







    }

}


