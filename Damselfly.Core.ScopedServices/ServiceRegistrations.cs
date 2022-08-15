﻿using System;
using Damselfly.Core.Interfaces;
using Damselfly.Core.ScopedServices.Interfaces;
using Damselfly.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Damselfly.Core.ScopedServices;

public static class ServiceRegistrations
{
    public static IServiceCollection AddDamselflyUIServices( this IServiceCollection services )
    {
        services.AddScoped<ViewDataService>();
        services.AddScoped<ClientDataService>();
        services.AddScoped<ClientBasketService>();
        services.AddScoped<ClientDownloadService>();
        services.AddScoped<ClientThemeService>();
        services.AddScoped<ClientUserService>();
        services.AddScoped<ClientWordpressService>();
        services.AddScoped<NavigationService>();
        services.AddScoped<SearchService>();
        services.AddScoped<ClientConfigService>();
        services.AddScoped<StatusService>();
        services.AddScoped<UserStatusService>();
        services.AddScoped<ClientFolderService>();

        services.AddScoped<ICachedDataService>(x => x.GetRequiredService<ClientDataService>());
        services.AddScoped<IConfigService>(x => x.GetRequiredService<ClientConfigService>());
        services.AddScoped<IFolderService>(x => x.GetRequiredService<ClientFolderService>());
        services.AddScoped<IStatusService>(x => x.GetRequiredService<UserStatusService>());
        services.AddScoped<IBasketService>(x => x.GetRequiredService<ClientBasketService>());
        services.AddScoped<IDownloadService>(x => x.GetRequiredService<ClientDownloadService>());

        services.AddScoped<SelectionService>();

        return services;
    }
}

