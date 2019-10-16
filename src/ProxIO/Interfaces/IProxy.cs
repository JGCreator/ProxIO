namespace ProxIO.Interfaces
{
    public interface IProxy<out TClientType>
    {
        TClientType Client { get; }
    }
}