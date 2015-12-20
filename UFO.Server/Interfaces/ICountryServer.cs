namespace UFO.Server.Interfaces
{
    using System.Collections.Generic;
    using UFO.Domain;

    public interface ICountryServer : IBaseServer<Country>
    {
    }
    public interface ICountryServerAsync : IBaseServerAsync<Country>
    {
    }
}
