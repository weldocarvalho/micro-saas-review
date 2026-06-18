# System Overview

## High-Level Architecture

User
 ↓
Next.js Frontend
 ↓
NestJS BFF
 ↓
RabbitMQ
 ↓
.NET Worker
 ↓
PostgreSQL

## Responsibilities

### Next.js

- Public website
- Authentication UI
- Patient dashboard

### NestJS

- API Gateway
- Authentication
- Validation
- Message publishing

### RabbitMQ

- Asynchronous communication

### Worker Service

- Business processing
- AI integrations
- Long-running operations

### PostgreSQL
