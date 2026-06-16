#!/bin/bash

# Service Worker - Quick Setup Script

set -e

echo "🚀 Service Worker - Setup Rápido"
echo "=================================="

# Cores para output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

# Check Docker
if ! command -v docker &> /dev/null; then
    echo -e "${RED}❌ Docker não encontrado. Instale antes de continuar.${NC}"
    exit 1
fi

echo -e "${GREEN}✅ Docker encontrado${NC}"

# Start containers
echo -e "${YELLOW}📦 Iniciando containers (RabbitMQ + PostgreSQL)...${NC}"
docker-compose up -d

echo -e "${GREEN}✅ Containers iniciados${NC}"

# Wait for RabbitMQ
echo -e "${YELLOW}⏳ Aguardando RabbitMQ inicializar...${NC}"
sleep 5

# Check RabbitMQ health
if curl -s -u guest:guest http://localhost:15672/api/aliveness-test/%2F >/dev/null 2>&1; then
    echo -e "${GREEN}✅ RabbitMQ pronto (http://localhost:15672 - guest/guest)${NC}"
else
    echo -e "${RED}⚠️  RabbitMQ ainda inicializando, aguarde alguns segundos${NC}"
fi

# Build
echo -e "${YELLOW}🔨 Compilando solução...${NC}"
cd worker
dotnet build --verbosity minimal

echo -e "${GREEN}✅ Build sucesso!${NC}"

echo ""
echo -e "${GREEN}🎉 Setup completo!${NC}"
echo ""
echo "Para iniciar o worker:"
echo -e "  ${YELLOW}cd worker && dotnet run${NC}"
echo ""
echo "RabbitMQ Management:"
echo -e "  ${YELLOW}http://localhost:15672${NC}"
echo "  Usuário: guest"
echo "  Senha: guest"
echo ""
