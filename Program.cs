using Courses_API.Models;
using Courses_API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
		options.UseSqlServer("name=DefaultConnection"));

builder.Services.AddIdentityCore<UserAsp>()
	.AddEntityFrameworkStores<ApplicationDbContext>()
	.AddDefaultTokenProviders();

builder.Services.AddScoped<UserManager<UserAsp>>();
builder.Services.AddScoped<SignInManager<UserAsp>>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication().AddJwtBearer(options =>
{
	options.MapInboundClaims = false;
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = false,
		ValidateAudience = false,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwtkey"]!)),
		ClockSkew = TimeSpan.Zero
	};
});

builder.Services.AddAuthorization(option =>
{
	option.AddPolicy("isadmin", policy => policy.RequireClaim("isadmin"));
});

var app = builder.Build();

app.MapControllers();

app.Run();