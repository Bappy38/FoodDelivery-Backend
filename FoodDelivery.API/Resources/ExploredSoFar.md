## Structured Logging

- Implemented Structured Logging using Serilog Library.
- Used three Log Sinks Console, File, ElasticSearch.
- Added a middleware to generate request correlation id if not exist already so that can easily monitor request log accross micro-services. And returning that CorrelationId in the response of that request. Propagating that CorrelationId through ThreadExecution Context so that we can find that inside the LogEnricher.