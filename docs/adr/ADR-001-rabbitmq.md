# ADR-001: RabbitMQ as Messaging Broker

## Status

Accepted

## Context

The system requires asynchronous communication between NestJS and .NET Worker services.

The project also aims to provide practical experience with event-driven architectures and messaging technologies commonly used in the market.

## Decision

RabbitMQ will be used as the primary message broker during development.

## Consequences

Positive:

- Decoupled services
- Asynchronous processing
- Industry-relevant experience

Negative:

- Additional infrastructure
- Operational complexity

## Alternatives Considered

- Direct HTTP communication
- AWS SQS
- Azure Service Bus