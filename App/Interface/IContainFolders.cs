namespace Swift.Balancer.App.Interface
{
    public interface IContainFolders
    {
        string SystemFolder();
        string ConfigFolder();
        string LogFolder();
        string PluginsFolder();
    }
}