    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddDbContext<ClientContext>(opt => opt.UseInMemoryDatabase("Clients"));
    builder.Services
      .AddTransient<IClientRepository,
                    ClientRepository>();
    builder.Services
      .AddAutoMapper(Assembly.GetEntryAssembly());

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = builder.Environment.ApplicationName, Version = "v1" });
    });

    var app = builder.Build();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("v1/swagger.json", $"{builder.Environment.ApplicationName} v1");
    });

    app.MapFallback(() => Results.Redirect("/swagger"));

    // Get a shared logger object
    var loggerFactory =
      app.Services.GetService<ILoggerFactory>();
    var logger =
      loggerFactory?.CreateLogger<Program>();

    if (logger == null)
    {
        throw new InvalidOperationException(
          "Logger not found");
    }

    // Get the Automapper, we can share this too
    var mapper = app.Services.GetService<IMapper>();
    if (mapper == null)
    {
        throw new InvalidOperationException(
          "Mapper not found");
    }

app.MapGet("/clients",
    async (IClientRepository repo) =>
    {
        var results = await repo.GetClientsAsync();
        return mapper.Map<
        IEnumerable<ClientModel>
        >(results);
    });

app.MapGet("/clients/{id:int}",
  async (int id, IClientRepository repo) =>
  {
      try
      {
          var client =
            await repo.GetClientAsync(id);
          if (client == null)
          {
              return Results.NotFound();
          }
          return Results.Ok(
            mapper.Map<ClientModel>(client));
      }
      catch (Exception ex)
      {
          logger.LogError(
            "Failed while reading: {message}",
            ex.Message);
      }
      return Results.BadRequest("Failed");
  });

app.MapPost("/clients",
  async (ClientModel model,
         IClientRepository repo) =>
  {
      try
      {
          var newClient = mapper.Map<Client>(model);
          repo.Add(newClient);
          if (await repo.SaveAll())
          {
              return Results.Created(
                $"/clients/{newClient.Id}",
                mapper.Map<ClientModel>(newClient));
          }
      }
      catch (Exception ex)
      {
          logger.LogError(
            "Failed while creating client: {ex}",
            ex);
      }
      return Results.BadRequest(
        "Failed to create client");
  });

app.MapPut("/clients/{id}",
  async (int id,
         ClientModel model,
         IClientRepository repo) =>
  {
      try
      {
          var oldClient =
            await repo.GetClientAsync(id);
          if (oldClient == null)
          {
              return Results.NotFound();
          }

          mapper.Map(model, oldClient);
          if (await repo.SaveAll())
          {
              return Results.Ok(
                mapper.Map<ClientModel>(oldClient));
          }
      }
      catch (Exception ex)
      {
          logger.LogError(
            "Failed while updating client: {ex}",
            ex);
      }
      return Results.BadRequest(
        "Failed to update client");
  });

app.MapDelete("/clients/{id}",
  async (int id,
         IClientRepository repo) =>
  {
      try
      {
          var client =
            await repo.GetClientAsync(id);
          if (client == null)
          {
              return Results.NotFound();
          }
          repo.Delete(client);
          if (await repo.SaveAll())
          {
              return Results.Ok();
          }
      }
      catch (Exception ex)
      {
          logger.LogError(
            "Failed while deleting client: {ex}",
            ex);
      }
      return Results.BadRequest(
        "Failed to deleting client");
  });

app.MapGet("/clients/{id}/cases",
  async (int id, IClientRepository repo) =>
  {
      var cases = await repo.GetClientCases(id);
      return Results.Ok(cases);
  });

app.Run();