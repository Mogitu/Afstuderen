/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   
/// </summary>
namespace AMC.Networking
{
    public interface IAmcMatch
    {
        void StartMatch();
        void TerminateMatch();        
        void AddClient(IAmcClient client);
        void RelayMessage();
    }
}
