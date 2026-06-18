# AI Development Guide

Before implementing anything:

1. Read project-context.md.
2. Read relevant architecture documentation.
3. Check ADRs.
4. Check backlog.

## Architecture Rules

- Domain must not depend on Infrastructure.
- Application orchestrates use cases.
- CQRS via MediatR.
- RabbitMQ for asynchronous communication.
- Business rules belong to Domain/Application.

## Coding Rules

- Prefer explicit naming.
- Avoid premature abstractions.
- Favor readability over cleverness.
- Keep methods small.
- Follow SOLID principles when appropriate.

## Documentation Rules

Whenever a significant decision is made:

- Update ADR.
- Update architecture docs.
- Update backlog.