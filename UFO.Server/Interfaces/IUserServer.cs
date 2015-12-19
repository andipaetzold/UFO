namespace UFO.Server.Interfaces
{
    using UFO.Domain;

    public interface IUserServer : IBaseServer<User>
    {
        bool CheckLoginData(string username, string password);
    }
}
