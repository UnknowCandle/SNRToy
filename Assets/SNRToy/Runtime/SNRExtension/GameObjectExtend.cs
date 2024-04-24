using SNRLogHelper;
using UnityEngine;


namespace SNRGameObjectExtend
{
    public static class GameObjectExtend
    {
        //if pLayer==1 ,return the parent gameobj
        public static GameObject GetParent(this GameObject value, int pLayer = 1)
        {
            Transform curTrans = value.transform;

            for (int i = 0; i < pLayer; ++i)
            {
                curTrans = curTrans.parent;
                if (curTrans == null)
                {
                    SLog.Err($"not find the parent at layer {i + 1} for {value}");
                    return null;
                }
            }

            return curTrans.gameObject;
        }


    }

}



