var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();
// builder.Services.AddCors();
builder.Services.AddCors(options => 
    options.AddPolicy("CorsPolicy", 
        policy => policy.WithOrigins("http://localhost:4200")
            .AllowCredentials()
            .AllowAnyMethod()
            .AllowAnyHeader()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// app.UseCors(x => x.AllowCredentials().AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));
app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();



app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<API.Hubs.ChatHub>("/hub");
});
// app.MapHub<API.Hubs.ChatHub>("/hub");

app.Run();
