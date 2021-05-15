using Api.Klinger.Extensions;
using AutoMapper;
using Business.Interfaces;
using Business.Notifications;
using Business.Services;
using Data.Context;
using Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Klinger.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services) 
        {
            services.AddScoped<DataContext>();            
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();            

            services.AddScoped<ISupplierService, SupplierService>();            
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<INotifier, Notifier>();

            services.AddScoped<IMapper, Mapper>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();
            

            return services;
        }
    }
}
