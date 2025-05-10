using Assets._Project.Scripts.EventBusSystem;
using Assets._Project.Scripts.ServiceLocatorSystem;

namespace Assets._Project.Scripts.Gameplay.CubeLogic.MainCubeControll
{
    public interface IMainCubeEvent : IBusEvent { }
    public class MainCubeEventBus : EventBus<IMainCubeEvent>, IService { }

    public struct MainCubeMergedEvent : IMainCubeEvent { } 

    public struct MainCubeSettled : IMainCubeEvent { }
}