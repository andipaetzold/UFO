namespace UFO.Server.Interfaces
{
    using System.Threading.Tasks;
    using UFO.Domain;

    public interface IUserServer : IBaseServer<User>
    {
        bool CheckLoginData(string username, string password);
    }
    public interface IUserServerAsync : IBaseServerAsync<User>
    {
        Task<bool> CheckLoginDataAsync(string username, string password);
    }
}
