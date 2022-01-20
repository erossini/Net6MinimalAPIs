﻿namespace MinimalApis.Data;

public interface IClientRepository
{
    Task<IEnumerable<Client>> GetClientsAsync(int page = 1, int pageSize = 25);
    Task<Client?> GetClientAsync(int id);
    Task<bool> HasClientAsync(int id);

    Task<IEnumerable<Case>> GetClientCases(int id);

    void Add<T>(T entity) where T : class;
    void Delete<T>(T entity) where T : class;
    Task<bool> SaveAll();
    void Update(Client existingClient);
}
