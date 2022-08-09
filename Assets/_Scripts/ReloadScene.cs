using DG.Tweening;
using UnityEngine;

public class ReloadScene : MonoBehaviour
{
    [UnityEditor.Callbacks.DidReloadScripts]
    private static void OnScriptsReloaded()
    {
        var existing = GameObject.Find("[DOTween]");
        if (existing)
            DestroyImmediate(existing);
        DOTween.Init();
    }
}
