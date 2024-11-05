using System.Diagnostics;

public class InstallGitHooks
{
    [UnityEditor.InitializeOnLoadMethod]
    private static void Install()
    {
        Process.Start("git", "config --local include.path ../.gitconfig");
    }
}
