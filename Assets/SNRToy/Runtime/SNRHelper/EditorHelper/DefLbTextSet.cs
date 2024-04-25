using UnityEngine;
using UnityEditor;
using TMPro;
using System.IO;
using SNRLogHelper;


public class DefLbTextSet : SNRBehaviour
{
    public class DefSetData
    {
        public string fontAssetPath = "Assets/AllToHot/Game/UnFighter/Asset/Font/NotoSerifCJKsc-Regular.asset";
        public int fontSize = 36;
        public FontStyles fontStyle = FontStyles.Normal;
        public bool wordWrapping = false;
        public TextOverflowModes overFlow = TextOverflowModes.Overflow;
        public TextAlignmentOptions aligment = TextAlignmentOptions.Center;

    }

    //subclass use own static DefSetData variable and override the method GetDefSetData
    static DefSetData s_defLbTextSet = null;

    public virtual DefSetData GetDefSetData()
    {
        if (s_defLbTextSet == null)
        {
            s_defLbTextSet = new();
        }

        return s_defLbTextSet;
    }

    public virtual bool HasDifferentWithSetting()
    {
        DefSetData data = GetDefSetData();

        string dataFontName = Path.GetFileNameWithoutExtension(data.fontAssetPath);

        TextMeshProUGUI lb = GetComponent<TextMeshProUGUI>();
        string useFontName = lb.font.name;
        useFontName = Path.GetFileName(useFontName);

        bool hasDiff = (dataFontName != useFontName) || (data.fontSize != lb.fontSize) ||
        (data.fontStyle != lb.fontStyle) || (data.wordWrapping != lb.enableWordWrapping)
        || (data.overFlow != lb.overflowMode) || (data.aligment != lb.alignment);

        return hasDiff;

    }

    public virtual void UpdateSetingByData()
    {
        DefSetData data = GetDefSetData();
        TextMeshProUGUI lb = GetComponent<TextMeshProUGUI>();

        if (lb != null)
        {
            TMP_FontAsset fontAsset = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>(data.fontAssetPath);
            if (fontAsset != null)
            {
                lb.font = fontAsset;
            }
            else
            {
                SLog.Warn("not find the font asset " + data.fontAssetPath);
            }

            lb.fontSize = data.fontSize;
            lb.fontStyle = data.fontStyle;
            lb.enableWordWrapping = data.wordWrapping;
            lb.overflowMode = data.overFlow;
            lb.alignment = data.aligment;
        }
        else
        {
            SLog.Warn("not find textMeshPro lb while this component exist");
        }
    }


    void Awake()
    {
        if (HasDifferentWithSetting())
        {
            UpdateSetingByData();
        }
    }

}


//自定义编辑器脚本，用于在代码中应用默认的 TextMeshPro 字体设置
[CustomEditor(typeof(DefLbTextSet))]
public class DefLbTextSetEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (!EditorApplication.isPlaying)
        {
            // 获取目标脚本的引用
            DefLbTextSet scInEditor = (DefLbTextSet)target;
            scInEditor.UpdateSetingByData();
        }

    }

}
