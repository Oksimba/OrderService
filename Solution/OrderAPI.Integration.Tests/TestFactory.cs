﻿using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OrderAPI.Integration.Tests
{
    public class TestFactory<TProgram>: WebApplicationFactory<TProgram> where TProgram : class
    {
        public IConfiguration Configuration { get; private set; }


        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(config =>
            {
                Configuration = new ConfigurationBuilder()
                    //.AddJsonFile("appsettings.json")
                    .AddUserSecrets<TestFactory<TProgram>>()
                    .Build();

                config.AddConfiguration(Configuration);


            });

            builder.ConfigureServices(services =>
            {
                var dbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                typeof(DbContextOptions<OrderAPIDbContext>));

                services.Remove(dbContextDescriptor);

                services.AddDbContext<OrderAPIDbContext>(
                    options =>
                    options.UseSqlServer(
                        Configuration["ConnectionStrings:DefaultConnection"]));

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var ctx = scope.ServiceProvider.GetRequiredService<OrderAPIDbContext>();

                var tesDbHelper = new DbInitializeService(ctx);
                tesDbHelper.Initialize();
            });

            
        }
    }
}