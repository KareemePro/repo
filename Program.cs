using FoodDelivery.Data;
using FoodDelivery.Models.DominModels;
using FoodDelivery.Services.Addresses;
using FoodDelivery.Services.Auth;
using FoodDelivery.Services.Meals;
using FoodDelivery.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));


builder.Services.AddDefaultIdentity<User>(options => 
{
    options.SignIn.RequireConfirmedAccount = true;
}).AddRoles<IdentityRole>().AddEntityFrameworkStores<DBContext>();


builder.Services.AddIdentityCore<Customer>().AddRoles<IdentityRole>().AddEntityFrameworkStores<DBContext>();

builder.Services.AddIdentityCore<Chief>().AddRoles<IdentityRole>().AddEntityFrameworkStores<DBContext>();

builder.Services.AddDbContext<DBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IMealService, MealService>();

builder.Services.AddScoped<IAddressSerivce, AddressSerivce>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(o =>
    {
        o.RequireHttpsMetadata = false;
        o.SaveToken = false;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
            ClockSkew = TimeSpan.Zero
        };
    });
builder.Services.AddCors();

builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAuthorization();

app.UseCors(x => x
.SetIsOriginAllowed(origin => true)
.AllowAnyMethod()
.AllowAnyHeader()
.AllowCredentials());


app.MapControllers();

app.Run();
