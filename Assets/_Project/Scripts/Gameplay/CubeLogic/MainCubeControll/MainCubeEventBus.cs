using Assets._Project.Scripts.EventBusSystem;
using Assets._Project.Scripts.ServiceLocatorSystem;

namespace Assets._Project.Scripts.Gameplay.CubeLogic.MainCubeControll
{
    public interface IMainCubeEvent : IBusEvent { }
    public class MainCubeEventBus<T> : EventBus<T>, IService where T : IMainCubeEvent
    { 

    }

    public struct MainCubeMergedEvent : IMainCubeEvent { } 

    public struct MainCubeSettledEvent : IMainCubeEvent { }
}