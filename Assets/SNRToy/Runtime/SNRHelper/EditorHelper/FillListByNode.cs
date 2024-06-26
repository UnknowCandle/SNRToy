#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

//挂载到某节点上时 生成nodeNum数量的fillNode做为其子节点
public class FillListByNode : MonoBehaviour
{
    public int _fillNum;
    public GameObject _fillNode;
    public bool _refresh = false;//不加这开关拖prefab时会卡死

    //inspector界面数值有改变时会触发
    void OnValidate()
    {
        if (_refresh)
        {
            _refresh = false;

            // 延迟一帧后执行,否则会卡在删除所有子对象
            EditorApplication.delayCall += () =>
            {
                if (this != null)//播放时因为延迟执行了可能报错,所以再次判断
                {
                    DeleteAllChildren();

                    // 判断 _fillNode 是否为 Prefab
                    if (PrefabUtility.IsPartOfPrefabAsset(_fillNode))
                    {
                        GenerateFillNodeFromPrefab();
                    }
                    else
                    {
                        GenerateFillNode();
                    }
                }
            };
        }
    }

    void GenerateFillNode()
    {
        for (int i = 0; i < _fillNum; ++i)
        {
            GameObject node = Instantiate(_fillNode, transform);
            node.name = i.ToString();
        }
    }

    void GenerateFillNodeFromPrefab()
    {
        for (int i = 0; i < _fillNum; ++i)
        {
            GameObject node = PrefabUtility.InstantiatePrefab(_fillNode, transform) as GameObject;
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
