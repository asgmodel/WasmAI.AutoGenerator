

using AutoBuilderApiCore;
using AutoGenerator;

using AutoGenerator.Data;
using AutoGenerator.Notifications.Config;
using AutoGenerator.Schedulers;
using LAHJAAPI.Data;
using LAHJAAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


using System.Reflection;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
     .AddEntityFrameworkStores<DataContext>()
     .AddDefaultTokenProviders();
/// <summary>
/// generate
///





builder.Services.
    AddAutoBuilderApiCore<DataContext,ApplicationUser>(new()
    {

        Arags = args,
        NameRootApi = "ApiCore",
        IsMapper = true,
        Assembly = Assembly.GetExecutingAssembly(),
        
        // DbConnectionString = builder.Configuration.GetConnectionString("DefaultConnection"),

    });
    //.AddAutoValidator()
    //.AddAutoConfigScheduler()
    //.AddAutoNotifier(new()
    //{

    //    MailConfiguration = new MailConfig()
    //    {
    //        SmtpUsername = "gamal333ge@gmail.com",
    //        SmtpPassword = "bxed hnwv vqlt ddwy",
    //        SmtpHost = "smtp.gmail.com",
    //        SmtpPort = 587,
    //        FromEmail = "gamal333ge@gmail.com",
    //        NameApp = "ASG" // ⁄Ì¯‰ «”„ «· ÿ»Ìﬁ Â‰« ﬂ„« Ì‰«”»ﬂ

    //    },




    //});




var app = builder.Build();
//app.UseSchedulerDashboard();

    //app.UseSchedulersCore(new OptionScheduler()
    //{
    //    Assembly = Assembly.GetExecutingAssembly(),



//});

// Configure the HTTP request pipeline.
  
  app.UseSwagger();
   app.UseSwaggerUI();
    app.MapSwagger();
 
    app.UseHttpsRedirection();
    

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
