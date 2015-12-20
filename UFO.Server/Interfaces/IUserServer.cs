namespace UFO.Server.Interfaces
{
    using System.Threading.Tasks;
    using UFO.Domain;

    public interface IUserServer : IBaseServer<User>
    {
        bool CheckLoginData(string username, string password);
        Task<bool> CheckLoginDataAsync(string username, string password);
    }
}
