var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("LocalDB");
builder.Services.AddDbContext<NoGravityDbContext>(x => x.UseSqlServer(connectionString));

builder.Services.AddTransient<ITicketsDataService, TicketsDataService>();

builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IStarportsRepository, StarportsRepository>();
builder.Services.AddScoped<ICarrierRepository, CarrierRepository>();
builder.Services.AddScoped<IPlanetsRepository, PlanetsRepository>();
builder.Services.AddScoped<IJourneysRepository, JourneysRepository>();
builder.Services.AddScoped<IJourneySegmentsRepository, JourneySegmentsRepository>();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
