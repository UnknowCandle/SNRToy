using SNRLogHelper;
using UnityEditor;

[InitializeOnLoad]
public class TemplateEditorUpdate
{
    static TemplateEditorUpdate()
    {
        SLog.Log("TemplateEditorUpdate init");
        EditorApplication.update += OnUpdate;
    }

    static void OnUpdate()
    {
        if(!EditorApplication.isPlaying){
            //do it in editor every frame
        }
    }

}
