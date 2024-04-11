using SNRKWordDefine;
using UnityEditor;
using UnityEngine;

public class PPUDefSet : AssetPostprocessor
{
    void OnPostprocessTexture(Texture2D texture)
    {
        if (assetPath.EndsWith(".png") || assetPath.EndsWith(".jpg"))
        {
            int ppu = EditorPrefs.GetInt(KWord.PixelPerUnit,100);
            TextureImporter textureImporter = (TextureImporter)assetImporter;
            textureImporter.spritePixelsPerUnit = ppu; 
        }
    }
}
