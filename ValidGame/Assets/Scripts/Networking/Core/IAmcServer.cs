namespace AMC.Networking
{
    public interface IAmcServer:INetworkContext
    {
       int SocketPort { get; set; }
    }
}
