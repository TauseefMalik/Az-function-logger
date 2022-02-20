# Az-function-logger
Azure HTTP trigger Function and Serilogger

## Problem
In most Server-less boilerplate template, generally a log context is provided which logs to default log service of the respective Cloud provider i.e. Azure Application metric or Microsoft.Extensions.Logging class.

However, they do not provide the ability to log messages with different log levels which is really necessary for large distributed micro-service based systems where-in you need to drill down to a particular warning or debug message.
Without a log-level, it becomes tedious to filter out the types of message that really interests us.


## How to use Serilog library in your Azure Functions

We will use Serilog library to perform logging in our HTTP trigger Azure function. The function simply prints the name passed in the query parameter as a response.

Switch to Monitor section in the function and check the App Insights Logs to view the logs in real-time. Notice how the logs are colored based on the log severity i.e. warning, info or error etc.

# Rerun and add a new screenshot
