using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace dotnet_rpg.Logging
{
    [ProviderAlias("Database")]
public class DbLoggerProvider : ILoggerProvider
{
    public readonly DbLoggerOptions Options;
 
    public DbLoggerProvider(IOptions<DbLoggerOptions> _options)
    {
        Options = _options.Value; // Stores all the options.
    }
 
 
    public ILogger CreateLogger(string categoryName)
    {
        return new DbLogger(this);
    }
 
    public void Dispose()
    {
    }
}
}