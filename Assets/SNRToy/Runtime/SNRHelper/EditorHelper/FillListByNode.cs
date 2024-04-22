# if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

//挂载到某节点上时 生成nodeNum数量的fillNode做为其子节点
public class FillListByNode : MonoBehaviour
{
    public int _fillNum;
    public GameObject _fillNode;

    //inspector界面数值有改变时会触发
    void OnValidate()
    {
        // 延迟一帧后执行,否则会卡在删除所有子对象
        EditorApplication.delayCall += () =>
        {
            DeleteAllChildren();
            GenerateFillNode();
        };

    }


    void GenerateFillNode()
    {
        for (int i = 0; i < _fillNum; ++i)
        {
            GameObject node = Instantiate(_fillNode, transform);
            node.name = i.ToString();
        }
    }


    void DeleteAllChildren()
    {
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }



}


#endif