using Application;
using Persistence;
using Persistence.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);

builder.Services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyHeader()
                       .AllowAnyMethod()
                       .SetIsOriginAllowed((host) => true)
                       .AllowCredentials()
            ));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
        await ResponseMessageSeeder.SeedExcepionMessages(context);
    }
    catch (Exception ex)
    {
        throw;
    }
}


app.UseCors("CorsPolicy");

app.UseExceptionHandler("/api/Error");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseRouting();


app.Run();
