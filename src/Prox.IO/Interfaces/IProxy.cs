namespace Prox.IO.Interfaces
{
    public interface IProxy<out TClientType>
    {
        TClientType Client { get; }
    }
}