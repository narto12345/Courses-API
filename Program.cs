using Courses_API.Models;
using Courses_API.Services;
using Courses_API.Swagger;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDataProtection();

string[]? allowOrigins = builder.Configuration.GetSection("allowOrigins").Get<string[]>();

builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(corsOptions =>
	{
		corsOptions.WithOrigins(allowOrigins!).AllowAnyMethod().AllowAnyHeader()
		.WithExposedHeaders("total-records");
	});
});

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IHashService, HashService>();

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

builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
	{
		Title = "Courses API",
		Description = "API RESTful - Courses",
		Contact = new Microsoft.OpenApi.Models.OpenApiContact
		{
			Name = "Nicolas Sosa",
			Email = "nicosan12@hotmail.com"
		}
	});

	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
		BearerFormat = "JWT",
		In = ParameterLocation.Header
	});

	options.OperationFilter<AuthorizationFilter>();

	//options.AddSecurityRequirement(new OpenApiSecurityRequirement
	//{
	//	{
	//		new OpenApiSecurityScheme
	//		{
	//			Reference = new OpenApiReference
	//			{
	//				Type = ReferenceType.SecurityScheme,
	//				Id = "Bearer"
	//			}
	//		},
	//		new string[] { }
	//	}
	//});
});

//builder.Services.AddTransient<IFileStorage, FileStorageAzure>();
builder.Services.AddTransient<IFileStorage, FileStorageLocal>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.Use(async (context, next) =>
{
	context.Response.Headers.Append("my-header", "value");
	await next();
});

app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles();

app.UseCors();

app.MapControllers();

app.Run();