﻿using System.Linq;
using Damselfly.Core.DbModels;
using Damselfly.Core.DbModels.Models;
using Damselfly.Core.Interfaces;
using Damselfly.Core.Models;
using Damselfly.Core.ScopedServices;
using Damselfly.Core.ScopedServices.Interfaces;
using Damselfly.Core.Services;
using Damselfly.ML.Face.Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Route = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Damselfly.Web.Server.Controllers;

// TODO: WASM: [Authorize]
[ApiController]
[Route("/api/config")]
public class ConfigController : ControllerBase
{
    private readonly IConfigService _configService;
    private readonly ISystemSettingsService _settingsService;

    private readonly ILogger<ConfigController> _logger;

    public ConfigController( IConfigService configService, SystemSettingsService settingsService, ILogger<ConfigController> logger)
    {
        _configService = configService;
        _settingsService = settingsService;
        _logger = logger;
    }

    [HttpPut("/api/config/{name}")]
    public void Set( string name, ConfigSetting setting )
    {
        _configService.Set(setting.Name, setting.Value);
    }

    [HttpGet("/api/config")]
    public async Task<List<ConfigSetting>> GetAll()
    {
        var settings = new List<ConfigSetting>();

        var allValues = await _configService.GetAllSettings();
        if (allValues != null)
            settings.AddRange(allValues);

        return settings;
    }
    [HttpGet("/api/config/{name}")]
    public ConfigSetting Get( string name )
    {
        var value = _configService.Get(name);
        return new ConfigSetting { Name = name, Value = value };
    }

    [HttpPost("/api/config/settings")]
    public async Task SetSysteemSettings(SystemConfigSettings settings)
    {
        await _settingsService.SaveSystemSettings(settings);
    }

    [HttpGet("/api/config/settings")]
    public async Task<SystemConfigSettings> GetSystemSettings()
    {
        return await _settingsService.GetSystemSettings();
    }
}

