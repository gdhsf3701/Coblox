/*using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Build;

public class Build_Start : IPostprocessBuild
{
    // 빌드 후 처리를 위한 우선 순위
    public int callbackOrder { get { return 0; } }

    // 빌드가 완료된 후에 호출될 함수
    public void OnPostprocessBuild(BuildTarget target, string path)
    {
        string sourceDir = Application.dataPath + "/Resources/Start";
        string targetDir = Path.GetDirectoryName(path) + "/Resources/Start";

        if (!Directory.Exists(targetDir))
            Directory.CreateDirectory(targetDir);

        foreach (string srcPath in Directory.GetFiles(sourceDir, "*.*", SearchOption.AllDirectories))
        {
            string dstPath = srcPath.Replace(sourceDir, targetDir);
            File.Copy(srcPath, dstPath, true);
        }
    }
}*/