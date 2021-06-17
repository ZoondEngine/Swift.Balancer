using System;

namespace Swift.Balancer.App.Interface
{
    public interface IService
    {
        string Name();
        Version Version();
    }
}