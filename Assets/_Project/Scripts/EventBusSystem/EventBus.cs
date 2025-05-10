namespace Assets._Project.Scripts.EventBusSystem
{
    public interface IBusEvent { }
    public abstract class EventBus<T> where T : IBusEvent
    {
        public delegate void Event(T evnt);

        public event Event OnEvent;

        public void Raise(T evnt)
        { 
            OnEvent?.Invoke(evnt); 
        }
    }
}