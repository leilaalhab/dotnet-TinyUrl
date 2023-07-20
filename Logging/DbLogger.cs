using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace dotnet_rpg.Logging
{
    public class DbLogger : ILogger
{

    private readonly DbLoggerProvider _dbLoggerProvider;
 

    public DbLogger([NotNull] DbLoggerProvider dbLoggerProvider)
    {
        _dbLoggerProvider = dbLoggerProvider;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        throw new NotImplementedException();
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel != LogLevel.None;
    }

    public async void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            // Don't log the entry if it's not enabled.
            return;
        }
 
        var threadId = Thread.CurrentThread.ManagedThreadId; // Get the current thread ID to use in the log file. 
 
        // Store record.
        using (var connection = new SqlConnection(_dbLoggerProvider.Options.ConnectionString))
        {
            try {
            connection.Open(); 
            } catch (Exception) {
            return;
            }

            
 

             var values = new JObject();
 
            if (_dbLoggerProvider?.Options?.LogFields?.Any() ?? false)
            {
                foreach (var logField in _dbLoggerProvider.Options.LogFields)
                {
                    switch (logField)
                    {
                        case "LogLevel":
                            if (!string.IsNullOrWhiteSpace(logLevel.ToString()))
                            {
                                values["LogLevel"] = logLevel.ToString();
                            }
                            break;
                        case "ThreadId":
                            values["ThreadId"] = threadId;
                            break;
                        case "EventId":
                            values["EventId"] = eventId.Id;
                            break;
                        case "EventName":
                            if (!string.IsNullOrWhiteSpace(eventId.Name))
                            {
                                values["EventName"] = eventId.Name;
                            }
                            break;
                        case "Message":
                            if (!string.IsNullOrWhiteSpace(formatter(state, exception)))
                            {
                                values["Message"] = formatter(state, exception);
                            }
                            break;
                        case "ExceptionMessage":
                            if (exception != null && !string.IsNullOrWhiteSpace(exception.Message))
                            {
                                values["ExceptionMessage"] = exception?.Message;
                            }
                            break;
                         case "ExceptionStackTrace":
                            if (exception != null && !string.IsNullOrWhiteSpace(exception.StackTrace))
                            {
                                values["ExceptionStackTrace"] = exception?.StackTrace;
                            }
                            break;
                        case "ExceptionSource":
                            if (exception != null && !string.IsNullOrWhiteSpace(exception.Source))
                            {
                                values["ExceptionSource"] = exception?.Source;
                            }
                            break;
                    }
                }
            }
 
 
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                
                switch (logLevel) {
                    case LogLevel.Error : { command.CommandText = string.Format("INSERT INTO {0} ([Values], [Created]) VALUES (@Values, @Created)", "Errors"); break; }
                    case LogLevel.Warning : { command.CommandText = string.Format("INSERT INTO {0} ([Values], [Created]) VALUES (@Values, @Created)", "Warnings"); break; }
                    case LogLevel.Information : { command.CommandText = string.Format("INSERT INTO {0} ([Values], [Created]) VALUES (@Values, @Created)", "Information"); break; }
                    case LogLevel.Critical : { command.CommandText = string.Format("INSERT INTO {0} ([Values], [Created]) VALUES (@Values, @Created)", "Critical"); break; }
                }
                



 
                command.Parameters.Add(new SqlParameter("@Values", JsonConvert.SerializeObject(values, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore,
                    Formatting = Formatting.None
                }).ToString()));
                command.Parameters.Add(new SqlParameter("@Created", DateTimeOffset.Now));
 
                command.ExecuteNonQuery();
            }
 
            connection.Close();
        }
    }
}
 
}
